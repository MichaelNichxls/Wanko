from dataclasses import dataclass
from environs import Env
from pprint import pformat
from typing import Optional

from .utils import LOGGING

# TODO: get streamingassets path from unity
_ENV = Env()
_ENV.read_env(".env.public")

# TODO: add validation
@dataclass(frozen=True, init=False)
class CharacterAIConfig:
    with _ENV.prefixed("CAI__"):
        CHAR_TOKEN: str = _ENV.str("CHAR_TOKEN")
        CHAR: str       = _ENV.str("CHAR")

@dataclass(frozen=True, init=False)
class WhisperConfig:
    with _ENV.prefixed("WHISPER__"):
        MODEL_SIZE_OR_PATH: str         = _ENV.str("MODEL_SIZE_OR_PATH")
        DEVICE: str                     = _ENV.str("DEVICE")
        DEVICE_INDEX: int               = _ENV.int("DEVICE_INDEX")
        COMPUTE_TYPE: str               = _ENV.str("COMPUTE_TYPE")
        CPU_THREADS: int                = _ENV.int("CPU_THREADS")
        DOWNLOAD_ROOT: Optional[str]    = _ENV.str("DOWNLOAD_ROOT", None)
        LOCAL_FILES_ONLY: bool          = _ENV.bool("LOCAL_FILES_ONLY")

@dataclass(frozen=True, init=False)
class SpeechRecognitionConfig:
    with _ENV.prefixed("SR__"):

        @dataclass(frozen=True, init=False)
        class Recognizer:
            with _ENV.prefixed("REC__"):
                ENERGY_THRESHOLD: int                       = _ENV.int("ENERGY_THRESHOLD")
                DYNAMIC_ENERGY_THRESHOLD: bool              = _ENV.bool("DYNAMIC_ENERGY_THRESHOLD")
                DYNAMIC_ENERGY_ADJUSTMENT_DAMPING: float    = _ENV.float("DYNAMIC_ENERGY_ADJUSTMENT_DAMPING")
                DYNAMIC_ENERGY_RATIO: float                 = _ENV.float("DYNAMIC_ENERGY_RATIO")
                PAUSE_THRESHOLD: float                      = _ENV.float("PAUSE_THRESHOLD")
                OPERATION_TIMEOUT: Optional[float]          = _ENV.float("OPERATION_TIMEOUT", None)
                PHRASE_THRESHOLD: float                     = _ENV.float("PHRASE_THRESHOLD")
                NON_SPEAKING_DURATION: float                = _ENV.float("NON_SPEAKING_DURATION")
                ADJUST_FOR_AMBIENT_NOISE: bool              = _ENV.bool("ADJUST_FOR_AMBIENT_NOISE")

        @dataclass(frozen=True, init=False)
        class Microphone:
            with _ENV.prefixed("MIC__"):
                DEVICE_INDEX: Optional[int] = _ENV.int("DEVICE_INDEX", None)
                SAMPLE_RATE: Optional[int]  = _ENV.int("SAMPLE_RATE", None)
                CHUNK_SIZE: int             = _ENV.int("CHUNK_SIZE")

LOGGING.debug(pformat(_ENV.dump()))