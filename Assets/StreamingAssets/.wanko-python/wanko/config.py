from dataclasses import dataclass, field
from environs import Env, EnvError
from pprint import pformat
from typing import List, Optional

from .utils import LOGGING

# TODO: get streamingassets path from unity
_ENV = Env()
_ENV.read_env(".env.public")

# TODO: add validation
@dataclass(frozen=True, init=False)
class CharacterAIConfig:
    with _ENV.prefixed("CHARACTERAI__"):
        CHAR_TOKEN: str = _ENV.str("CHAR_TOKEN")
        CHAR: str       = _ENV.str("CHAR", "D_ZmzC4orhpu6S7UXmW-PIS0X5qFm1qnzukot7v6vWI")

@dataclass(frozen=True, init=False)
class WhisperConfig:
    with _ENV.prefixed("WHISPER__"):
        MODEL_SIZE_OR_PATH: str         = _ENV.str("MODEL_SIZE_OR_PATH", "base")
        DEVICE: str                     = _ENV.str("DEVICE", "auto")
        DEVICE_INDEX: int               = _ENV.int("DEVICE_INDEX", 0)
        COMPUTE_TYPE: str               = _ENV.str("COMPUTE_TYPE", "default")
        CPU_THREADS: int                = _ENV.int("CPU_THREADS", 0)
        NUM_WORKERS: int                = _ENV.int("NUM_WORKERS", 1)
        DOWNLOAD_ROOT: Optional[str]    = _ENV.str("DOWNLOAD_ROOT", None)
        LOCAL_FILES_ONLY: bool          = _ENV.bool("LOCAL_FILES_ONLY", False)

# FIXME
@dataclass(frozen=True, init=False)
class TranscribeConfig:
    with _ENV.prefixed("TRANSCRIBE__"):
        LANGUAGE: Optional[str]                             = _ENV.str("LANGUAGE", None)
        TASK: str                                           = _ENV.str("TASK", "transcribe")
        BEAM_SIZE: int                                      = _ENV.int("BEAM_SIZE", 5)
        BEST_OF: int                                        = _ENV.int("BEST_OF", 5)
        PATIENCE: float                                     = _ENV.float("PATIENCE", 1.0)
        LENGTH_PENALTY: float                               = _ENV.float("LENGTH_PENALTY", 1.0)
        REPETITION_PENALTY: float                           = _ENV.float("REPETITION_PENALTY", 1.0)
        NO_REPEAT_NGRAM_SIZE: int                           = _ENV.int("NO_REPEAT_NGRAM_SIZE", 0)
        # TEMPERATURE: List[float]                            = field(default_factory=lambda: [0.0, 0.2, 0.4, 0.6, 0.8, 1.0])
        COMPRESSION_RATIO_THRESHOLD: float                  = _ENV.float("COMPRESSION_RATIO_THRESHOLD", 2.4)
        LOG_PROB_THRESHOLD: float                           = _ENV.float("LOG_PROB_THRESHOLD", -1.0)
        NO_SPEECH_THRESHOLD: float                          = _ENV.float("NO_SPEECH_THRESHOLD", 0.6)
        CONDITION_ON_PREVIOUS_TEXT: bool                    = _ENV.bool("CONDITION_ON_PREVIOUS_TEXT", True)
        PROMPT_RESET_ON_TEMPERATURE: float                  = _ENV.float("PROMPT_RESET_ON_TEMPERATURE", 0.5)
        INITIAL_PROMPT: Optional[str]                       = _ENV.str("INITIAL_PROMPT", None)
        PREFIX: Optional[str]                               = _ENV.str("PREFIX", None)
        SUPPRESS_BLANK: bool                                = _ENV.bool("SUPPRESS_BLANK", True)
        # SUPPRESS_TOKENS: List[int]                          = field(default_factory=lambda: [-1])
        WITHOUT_TIMESTAMPS: bool                            = _ENV.bool("WITHOUT_TIMESTAMPS", False)
        MAX_INITIAL_TIMESTAMP: float                        = _ENV.float("MAX_INITIAL_TIMESTAMP", 1.0)
        WORD_TIMESTAMPS: bool                               = _ENV.bool("WORD_TIMESTAMPS", False)
        PREPEND_PUNCTUATIONS: str                           = _ENV.str("PREPEND_PUNCTUATIONS", "\"'“¿([{-")
        APPEND_PUNCTUATIONS: str                            = _ENV.str("APPEND_PUNCTUATIONS", "\"'.。,，!！?？:：”)]}、")
        VAD_FILTER: bool                                    = _ENV.bool("VAD_FILTER", False)
        # VAD_PARAMETERS: Optional[dict]                      = field(default_factory=dict)
        MAX_NEW_TOKENS: Optional[int]                       = _ENV.int("MAX_NEW_TOKENS", None)
        CHUNK_LENGTH: Optional[int]                         = _ENV.int("CHUNK_LENGTH", None)
        CLIP_TIMESTAMPS: str                                = _ENV.str("CLIP_TIMESTAMPS", "0")
        HALLUCINATION_SILENCE_THRESHOLD: Optional[float]    = _ENV.float("HALLUCINATION_SILENCE_THRESHOLD", None)

@dataclass(frozen=True, init=False)
class RecognizerConfig:
    with _ENV.prefixed("RECOGNIZER__"):
        ENERGY_THRESHOLD: int                       = _ENV.int("ENERGY_THRESHOLD", 300)
        DYNAMIC_ENERGY_THRESHOLD: bool              = _ENV.bool("DYNAMIC_ENERGY_THRESHOLD", True)
        DYNAMIC_ENERGY_ADJUSTMENT_DAMPING: float    = _ENV.float("DYNAMIC_ENERGY_ADJUSTMENT_DAMPING", 0.15)
        DYNAMIC_ENERGY_RATIO: float                 = _ENV.float("DYNAMIC_ENERGY_RATIO", 1.5)
        PAUSE_THRESHOLD: float                      = _ENV.float("PAUSE_THRESHOLD", 0.8)
        OPERATION_TIMEOUT: Optional[float]          = _ENV.float("OPERATION_TIMEOUT", None)
        PHRASE_THRESHOLD: float                     = _ENV.float("PHRASE_THRESHOLD", 0.3)
        NON_SPEAKING_DURATION: float                = _ENV.float("NON_SPEAKING_DURATION", 0.5)
        ADJUST_FOR_AMBIENT_NOISE: bool              = _ENV.bool("ADJUST_FOR_AMBIENT_NOISE", False)

@dataclass(frozen=True, init=False)
class MicrophoneConfig:
    with _ENV.prefixed("MICROPHONE__"):
        DEVICE_INDEX: Optional[int] = _ENV.int("DEVICE_INDEX", None)
        SAMPLE_RATE: Optional[int]  = _ENV.int("SAMPLE_RATE", None)
        CHUNK_SIZE: int             = _ENV.int("CHUNK_SIZE", 1_024)

LOGGING.debug(pformat(_ENV.dump()))