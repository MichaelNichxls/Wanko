import numpy as np

from faster_whisper import WhisperModel

from .config import WhisperConfig

class Transcriber:
    def __init__(self) -> None:
        self.model = WhisperModel(
            model_size_or_path=WhisperConfig.MODEL_SIZE_OR_PATH,
            device=WhisperConfig.DEVICE,
            device_index=WhisperConfig.DEVICE_INDEX,
            compute_type=WhisperConfig.COMPUTE_TYPE,
            cpu_threads=WhisperConfig.CPU_THREADS,
            download_root=WhisperConfig.DOWNLOAD_ROOT,
            local_files_only=WhisperConfig.LOCAL_FILES_ONLY
        )

    def transcribe_bytes(self, audio: bytes) -> str:
        audio_np = np.frombuffer(audio, dtype=np.int16).astype(np.float32) / 32_768.0
        segments, _ = self.model.transcribe(audio_np)
        transcription = " ".join([segment.text.strip() for segment in segments])

        return transcription