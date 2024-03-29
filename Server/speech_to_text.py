import logging
import numpy as np
import speech_recognition as sr
# import torch
import zmq

from config import Config
from faster_whisper import WhisperModel
from pipeline_thread import PipelineThread
from queue import Queue
from time import sleep
from typing import override

# rename
class STTThread(PipelineThread):
    def __init__(self):
        super().__init__(send_addr=Config.ZMQ_ADDR_STT)
    
        # TODO: do something like this
        # self.record_timeout = Config.WHISPER_RECORD_TIMEOUT
        # self.phrase_timeout = Config.WHISPER_PHRASE_TIMEOUT

        # TODO: make some attributes not attributes and/or private
        
        # Thread safe Queue for passing data from the threaded recording callback.
        self._data_queue = Queue()
        # We use SpeechRecognizer to record our audio because it has a nice feature where it can detect when speech ends.
        self.recorder = sr.Recognizer()
        self.recorder.energy_threshold = Config.SR_ENERGY_THRESHOLD
        self.recorder.pause_threshold = Config.SR_PAUSE_THRESHOLD
        # Definitely do this, dynamic energy compensation lowers the energy threshold dramatically to a point where the SpeechRecognizer never stops recording.
        self.recorder.dynamic_energy_threshold = False

        self.source = sr.Microphone(sample_rate=16_000)

        with self.source:
            self.recorder.adjust_for_ambient_noise(self.source)

        def record_callback(_, audio: sr.AudioData) -> None:
            """
            Threaded callback function to receive audio data when recordings finish.
            audio: An AudioData containing the recorded bytes.
            """
            # Grab the raw bytes and push it into the thread safe queue.
            data: bytes = audio.get_raw_data()
            self._data_queue.put(data)

        # Create a background thread that will pass us raw audio bytes.
        # We could do this manually but SpeechRecognizer provides a nice helper.
        self.recorder.listen_in_background(self.source, record_callback) # self.record_timeout

        # configurable
        self.audio_model = WhisperModel(Config.WHISPER_MODEL, device=Config.WHISPER_DEVICE, compute_type=Config.WHISPER_COMPUTE_TYPE)

        # TODO: log
        print("Model loaded.")
    
    # use events
    @override
    def run(self) -> None:
        super().run()

        while not self.stop_event.is_set():
            # Pull raw recorded audio from the queue.
            if self._data_queue.empty():
                # Infinite loops are bad for processors, must sleep.
                sleep(0.25)
                continue
            
            # Combine audio data from queue
            audio_data = b"".join(self._data_queue.queue)
            self._data_queue.queue.clear()

            # Convert in-ram buffer to something the model can use directly without needing a temp file.
            # Convert data from 16 bit wide integers to floating point with a width of 32 bits.
            # Clamp the audio stream frequency to a PCM wavelength compatible default of 32768hz max.
            audio_np = np.frombuffer(audio_data, dtype=np.int16).astype(np.float32) / 32_768.0

            # Read the transcription.
            segments, _ = self.audio_model.transcribe(audio_np)
            text = " ".join([segment.text.strip() for segment in segments])

            # TODO: logging.debug()
            print(text)

            # TODO: log
            # FIXME: do this in a non-blocking manner
            try:
                self.push_socket.send_string(text, zmq.DONTWAIT)
            except zmq.Again:
                pass

# TODO: Log transcription with debug level in finally block 

if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)

    with STTThread() as stt_thread:
        try:
            stt_thread.run()
        except KeyboardInterrupt:
            stt_thread.stop_event.set()