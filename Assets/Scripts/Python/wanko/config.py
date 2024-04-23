import logging

from dataclasses import dataclass
from environs import Env
from pprint import pformat
from typing import Optional

_ENV = Env()
_ENV.read_env(".env.public")

@dataclass(frozen=True, init=False)
class Config:
    with _ENV.prefixed("CAI__"):
        CAI__CHAR_TOKEN: str                                    = _ENV.str("CHAR_TOKEN")
        CAI__CHAR: str                                          = _ENV.str("CHAR")

    with _ENV.prefixed("WHISPER__"):
        WHISPER__MODEL_SIZE_OR_PATH: str                        = _ENV.str("MODEL_SIZE_OR_PATH")
        WHISPER__DEVICE: str                                    = _ENV.str("DEVICE")
        WHISPER__DEVICE_INDEX: int                              = _ENV.int("DEVICE_INDEX")
        WHISPER__COMPUTE_TYPE: str                              = _ENV.str("COMPUTE_TYPE")
        WHISPER__CPU_THREADS: int                               = _ENV.int("CPU_THREADS")
        WHISPER__DOWNLOAD_ROOT: Optional[str]                   = _ENV.str("DOWNLOAD_ROOT", None)
        WHISPER__LOCAL_FILES_ONLY: bool                         = _ENV.bool("LOCAL_FILES_ONLY")
    
    with _ENV.prefixed("SR__"):
        with _ENV.prefixed("REC__"):
            SR__REC__ENERGY_THRESHOLD: int                      = _ENV.int("ENERGY_THRESHOLD")
            SR__REC__DYNAMIC_ENERGY_THRESHOLD: bool             = _ENV.bool("DYNAMIC_ENERGY_THRESHOLD")
            SR__REC__DYNAMIC_ENERGY_ADJUSTMENT_DAMPING: float   = _ENV.float("DYNAMIC_ENERGY_ADJUSTMENT_DAMPING")
            SR__REC__DYNAMIC_ENERGY_RATIO: float                = _ENV.float("DYNAMIC_ENERGY_RATIO")
            SR__REC__PAUSE_THRESHOLD: float                     = _ENV.float("PAUSE_THRESHOLD")
            SR__REC__OPERATION_TIMEOUT: Optional[float]         = _ENV.float("OPERATION_TIMEOUT", None)
            SR__REC__PHRASE_THRESHOLD: float                    = _ENV.float("PHRASE_THRESHOLD")
            SR__REC__NON_SPEAKING_DURATION: float               = _ENV.float("NON_SPEAKING_DURATION")
            SR__REC__ADJUST_FOR_AMBIENT_NOISE: bool             = _ENV.bool("ADJUST_FOR_AMBIENT_NOISE")

        with _ENV.prefixed("MIC__"):
            SR__MIC__DEVICE_INDEX: Optional[int]                = _ENV.int("DEVICE_INDEX", None)
            SR__MIC__SAMPLE_RATE: Optional[int]                 = _ENV.int("SAMPLE_RATE", None)
            SR__MIC__CHUNK_SIZE: int                            = _ENV.int("CHUNK_SIZE")
    
    with _ENV.prefixed("LOGGING__"):
        LOGGING__LEVEL: logging                                 = _ENV.log_level("LEVEL")

logging.basicConfig(level=Config.LOGGING__LEVEL)
logging.info(pformat(Config()))