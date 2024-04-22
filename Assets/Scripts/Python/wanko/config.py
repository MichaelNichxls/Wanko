import os

from dataclasses import dataclass
from dotenv import load_dotenv

from pprint import pprint

load_dotenv(".env.public")

# TODO: add asserts
@dataclass(frozen=True, init=False)
class Config:
    """Character.AI"""
    CAI_TOKEN: str              = os.getenv("CAI_TOKEN")
    CAI_CHAR: str               = os.getenv("CAI_CHAR")
    """OpenAI Whisper"""
    WHISPER_MODEL: str          = os.getenv("WHISPER_MODEL")
    WHISPER_DEVICE: str         = os.getenv("WHISPER_DEVICE")
    WHISPER_COMPUTE_TYPE: str   = os.getenv("WHISPER_COMPUTE_TYPE")
    """Speech Recognition"""
    SR_ENERGY_THRESHOLD: int    = int(os.getenv("SR_ENERGY_THRESHOLD"))
    SR_PAUSE_THRESHOLD: float   = float(os.getenv("SR_PAUSE_THRESHOLD"))

# Config(**getenv_values)

# TODO: logging.debug()
pprint(Config())