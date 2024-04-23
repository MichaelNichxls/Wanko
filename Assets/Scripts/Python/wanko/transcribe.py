import logging
import numpy as np

from faster_whisper import WhisperModel
from typing import BinaryIO, Union

from .config import Config

# inherit Whisper?
class Transcriber:
    def __init__(self) -> None:
        self._model = WhisperModel(
            model_size_or_path=Config.WHISPER__MODEL_SIZE_OR_PATH,
            device=Config.WHISPER__DEVICE,
            device_index=Config.WHISPER__DEVICE_INDEX,
            compute_type=Config.WHISPER__COMPUTE_TYPE,
            cpu_threads=Config.WHISPER__CPU_THREADS,
            download_root=Config.WHISPER__DOWNLOAD_ROOT,
            local_files_only=Config.WHISPER__LOCAL_FILES_ONLY
        )
        logging.info("Transcriber initialized")

    def transcribe_bytes(self, audio: bytes) -> str:
        audio_np = np.frombuffer(audio, dtype=np.int16).astype(np.float32) / 32_768.0
        return self.transcribe(audio_np)
    
    def transcribe(self, audio: Union[str, BinaryIO, np.ndarray]) -> str:
        segments, _ = self._model.transcribe(audio)
        transcription = " ".join([segment.text.strip() for segment in segments])
        return transcription