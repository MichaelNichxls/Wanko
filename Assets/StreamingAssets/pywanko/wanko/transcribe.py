import numpy as np

from faster_whisper import WhisperModel

from .config import WhisperConfig, TranscribeConfig

MODEL = WhisperModel(
    WhisperConfig.MODEL_SIZE_OR_PATH,
    WhisperConfig.DEVICE,
    WhisperConfig.DEVICE_INDEX,
    WhisperConfig.COMPUTE_TYPE,
    WhisperConfig.CPU_THREADS,
    WhisperConfig.NUM_WORKERS,
    WhisperConfig.DOWNLOAD_ROOT,
    WhisperConfig.LOCAL_FILES_ONLY
)

def transcribe_bytes(audio: bytes) -> str:
    audio_np = np.frombuffer(audio, dtype=np.int16).astype(np.float32) / 32_768.0
    segments, _ = MODEL.transcribe(
        audio_np,
        TranscribeConfig.LANGUAGE,
        TranscribeConfig.TASK,
        TranscribeConfig.BEAM_SIZE,
        TranscribeConfig.BEST_OF,
        TranscribeConfig.PATIENCE,
        TranscribeConfig.LENGTH_PENALTY,
        TranscribeConfig.REPETITION_PENALTY,
        TranscribeConfig.NO_REPEAT_NGRAM_SIZE,
        [0.0, 0.2, 0.4, 0.6, 0.8, 1.0],
        TranscribeConfig.COMPRESSION_RATIO_THRESHOLD,
        TranscribeConfig.LOG_PROB_THRESHOLD,
        TranscribeConfig.NO_SPEECH_THRESHOLD,
        TranscribeConfig.CONDITION_ON_PREVIOUS_TEXT,
        TranscribeConfig.PROMPT_RESET_ON_TEMPERATURE,
        TranscribeConfig.INITIAL_PROMPT,
        TranscribeConfig.PREFIX,
        TranscribeConfig.SUPPRESS_BLANK,
        [-1],
        TranscribeConfig.WITHOUT_TIMESTAMPS,
        TranscribeConfig.MAX_INITIAL_TIMESTAMP,
        TranscribeConfig.WORD_TIMESTAMPS,
        TranscribeConfig.PREPEND_PUNCTUATIONS,
        TranscribeConfig.APPEND_PUNCTUATIONS,
        TranscribeConfig.VAD_FILTER,
        None,
        TranscribeConfig.MAX_NEW_TOKENS,
        TranscribeConfig.CHUNK_LENGTH,
        TranscribeConfig.CLIP_TIMESTAMPS,
        TranscribeConfig.HALLUCINATION_SILENCE_THRESHOLD
    )

    return " ".join([segment.text.strip() for segment in segments])