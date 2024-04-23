import logging
import speech_recognition as sr

from typing import Callable

from .config import Config
from .transcribe import Transcriber

class Recognizer:
    def __init__(self) -> None:
        self._recorder = sr.Recognizer()
        self._recorder.energy_threshold = Config.SR__REC__ENERGY_THRESHOLD
        self._recorder.dynamic_energy_threshold = Config.SR__REC__DYNAMIC_ENERGY_THRESHOLD
        self._recorder.dynamic_energy_adjustment_damping = Config.SR__REC__DYNAMIC_ENERGY_ADJUSTMENT_DAMPING
        self._recorder.dynamic_energy_ratio = Config.SR__REC__DYNAMIC_ENERGY_RATIO
        self._recorder.pause_threshold = Config.SR__REC__PAUSE_THRESHOLD
        self._recorder.operation_timeout = Config.SR__REC__OPERATION_TIMEOUT
        self._recorder.phrase_threshold = Config.SR__REC__PHRASE_THRESHOLD
        self._recorder.non_speaking_duration = Config.SR__REC__NON_SPEAKING_DURATION

        self._source = sr.Microphone(
            device_index=Config.SR__MIC__DEVICE_INDEX,
            sample_rate=Config.SR__MIC__SAMPLE_RATE,
            chunk_size=Config.SR__MIC__CHUNK_SIZE
        )

        if Config.SR__REC__ADJUST_FOR_AMBIENT_NOISE:
            with self._source:
                self._recorder.adjust_for_ambient_noise(self._source)
        
        logging.info("Recognizer initialized")
    
    def listen_in_background(self, callback: Callable[[sr.AudioSource, sr.AudioData], None]) -> None:
        self._recorder.listen_in_background(self._source, callback)
    
    def transcribe_in_background(self, transcriber: Transcriber) -> None:
        def record_callback(_, audio: sr.AudioData) -> None:
            """
            Threaded callback function to receive audio data when recordings finish.
            audio: An AudioData containing the recorded bytes.
            """
            data: bytes = audio.get_raw_data()
            transcription = transcriber.transcribe_bytes(data)
            logging.info(transcription)
        
        self.listen_in_background(self, record_callback)