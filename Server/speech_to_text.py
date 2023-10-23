# import argparse
import io
import logging
import speech_recognition as sr
import torch
import zmq

from config import Config
from datetime import datetime, timedelta
from faster_whisper import WhisperModel
from queue import Queue
from threading import Event
from typing import Optional
from tempfile import NamedTemporaryFile
from time import sleep

# TODO: Do this better
Config.load_config("config.json")
config = Config.get_config()

_URL_PUB = "{}://{}:{}".format(config["zmq"]["protocol"], "*", config["zmq"]["port"])

_TOPIC_STT = config["zmq"]["topics"]["speech_to_text"]

_MODEL = config["whisper"]["model"]
_ENERGY_THRESHOLD = config["whisper"]["energy_threshold"]
_RECORD_TIMEOUT = config["whisper"]["record_timeout"]
_PHRASE_TIMEOUT = config["whisper"]["phrase_timeout"]

# TODO: Rename
def speech_to_text(*, publisher: zmq.Socket, stop_event: Optional[Event] = None) -> None:
    # parser = argparse.ArgumentParser()
    # TODO: Update help args
    # TODO: Update args in general
    # parser.add_argument("--model", default="base", help="Model to use.", choices=["tiny", "base", "small", "medium", "large"])
    # parser.add_argument("--non_english", action="store_true", help="Don't use the english model.")
    # parser.add_argument("--energy_threshold", default=1_000, help="Energy level for mic to detect.", type=int)
    # parser.add_argument("--record_timeout", default=2, help="How real-time the recording is in seconds.", type=float)
    # parser.add_argument("--phrase_timeout", default=3, help="How much empty space between recordings before the phrase is considered a new line in the transcription.", type=float)

    # args = parser.parse_args()

    # The last time a recording was retrieved from the queue.
    phrase_time = None
    # Current raw audio bytes.
    last_sample = bytes()
    # Thread safe Queue for passing data from the threaded recording callback.
    data_queue = Queue()
    # We use SpeechRecognizer to record our audio because it has a nice feature where it can detect when speech ends.
    recorder = sr.Recognizer()
    # recorder.energy_threshold = args.energy_threshold
    recorder.energy_threshold = _ENERGY_THRESHOLD
    # Definitely do this, dynamic energy compensation lowers the energy threshold dramatically to a point where the SpeechRecognizer never stops recording.
    recorder.dynamic_energy_threshold = False

    # HACK
    should_send = False

    # Load / Download model
    # model = args.model

    # if args.model != "large" and not args.non_english:
    #     model += ".en"

    # TODO: Move to config; Rename
    # TODO: Benchmark and optimize
    audio_model = WhisperModel(_MODEL, compute_type="int8_float16" if torch.cuda.is_available() else "int8")
    logging.info("Loaded \"%s\" Whisper model", _MODEL)

    # record_timeout = args.record_timeout
    # phrase_timeout = args.phrase_timeout

    temp_file = NamedTemporaryFile().name
    transcription = [""]

    source = sr.Microphone(sample_rate=16000)

    with source:
        recorder.adjust_for_ambient_noise(source)

    def record_callback(_, audio: sr.AudioData) -> None:
        """
        Threaded callback function to receive audio data when recordings finish.
        audio: An AudioData containing the recorded bytes.
        """
        # Grab the raw bytes and push it into the thread safe queue.
        data = audio.get_raw_data()
        data_queue.put(data)

    # Create a background thread that will pass us raw audio bytes.
    # We could do this manually but SpeechRecognizer provides a nice helper.
    recorder.listen_in_background(source, record_callback, phrase_time_limit=_RECORD_TIMEOUT)

    # TODO: Utilize faster-whisper's segmenting
    try:
        # TODO: Subclass
        while stop_event is None or not stop_event.is_set():
            # Infinite loops are bad for processors, must sleep.
            sleep(0.2)

            # TODO: Move
            now = datetime.utcnow()
            phrase_complete = False

            # If enough time has passed between recordings, consider the phrase complete.
            # Clear the current working audio buffer to start over with the new data.
            if phrase_time and now - phrase_time > timedelta(seconds=_PHRASE_TIMEOUT):
                last_sample = bytes()
                phrase_complete = True

                # HACK
                if should_send:
                    should_send = False

                    publisher.send_string(_TOPIC_STT, zmq.SNDMORE)
                    publisher.send_string(transcription[-1])

                    # TODO: logging.debug()?
                    logging.info("Phrase sent via socket")
            
            # Pull raw recorded audio from the queue.
            if data_queue.empty():
                continue

            # HACK
            should_send = True

            # This is the last time we received new audio data from the queue.
            phrase_time = now

            # Concatenate our current audio data with the latest audio data.
            while not data_queue.empty():
                data = data_queue.get()
                last_sample += data

            # Use AudioData to convert the raw data to wav data.
            audio_data = sr.AudioData(last_sample, source.SAMPLE_RATE, source.SAMPLE_WIDTH)
            wav_data = io.BytesIO(audio_data.get_wav_data())

            # Write wav data to the temporary file as bytes.
            with open(temp_file, "w+b") as f:
                f.write(wav_data.read())

            # Read the transcription.
            segments, _ = audio_model.transcribe(temp_file, beam_size=5)
            # NOTE: LIST COMPREHENSIONS FOR THE WIN HAHAHA
            # TODO: Do this where applicable
            text = " ".join(segment.text.strip() for segment in segments)

            # If we detected a pause between recordings, add a new item to our transcription.
            # Otherwise edit the existing one.
            if phrase_complete:
                transcription.append(text)
            else:
                transcription[-1] = text
            
            # TODO: Format in terminal
            print(text)

            # TODO: Log transcription with debug level in finally block 
    except KeyboardInterrupt:
        # TODO: Add everywhere or move
        logging.info("Program interrupted by user")
    finally:
        logging.debug("Transcription:\n%s", "\n".join(transcription))

if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)

    with (
        zmq.Context() as context,
        context.socket(zmq.PUB) as publisher
    ):
        publisher.bind(_URL_PUB)
        logging.info("Binded to socket at \"%s\"", _URL_PUB)

        speech_to_text(publisher=publisher)