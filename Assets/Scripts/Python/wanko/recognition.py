import logging
import speech_recognition as sr

from queue import Queue

from .config import Config

class Recognizer:
    def __init__(self) -> None:
        # TODO: make prop
        self._data_queue = Queue()
        # TODO: make more configurable
        self._recorder = sr.Recognizer()
        self._recorder.dynamic_energy_threshold = False
        self._recorder.energy_threshold = Config.SR_ENERGY_THRESHOLD
        self._recorder.pause_threshold = Config.SR_PAUSE_THRESHOLD

        self._source = sr.Microphone(sample_rate=16_000)

        with self._source:
            self._recorder.adjust_for_ambient_noise(self._source)

        def record_callback(_, audio: sr.AudioData) -> None:
            """
            Threaded callback function to receive audio data when recordings finish.
            audio: An AudioData containing the recorded bytes.
            """
            data: bytes = audio.get_raw_data()
            self._data_queue.put(data)

        self._recorder.listen_in_background(self._source, record_callback)
        logging.info("Recognizer initialized")
    
    # def listen_in_background
        # audio_data = b"".join(data)

        # transcription = self.transcribe(self, audio_data)
        # print(transcription)

if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)