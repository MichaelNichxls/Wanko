import logging
import numpy as np

from faster_whisper import WhisperModel
# from typing import BinaryIO, Union

from .config import Config

class Transcriber:
    def __init__(self) -> None:
        # TODO: make more configurable and/or private
        self._model = WhisperModel(
            Config.WHISPER_MODEL,
            device=Config.WHISPER_DEVICE,
            compute_type=Config.WHISPER_COMPUTE_TYPE
        )
        logging.info("Transcriber initialized")

    # transcribe_bytes
    def transcribe(self, audio: bytes) -> str:
        audio_np = np.frombuffer(audio, dtype=np.int16).astype(np.float32) / 32_768.0
        
        segments, _ = self._model.transcribe(audio_np)
        text = " ".join([segment.text.strip() for segment in segments])

        # TODO: Log transcription with debug level in finally block
        # TODO: send via event
        return text

if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)