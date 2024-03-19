# TODO: Rename config.py

import os

from dataclasses import dataclass, field
from dotenv import load_dotenv

load_dotenv(".env.ex")

@dataclass(frozen=True, init=False)
class Config:
    CAI_TOKEN: str                  = os.getenv("CAI_TOKEN")
    CAI_CHAR: str                   = os.getenv("CAI_CHAR")
    ZMQ_PROTOCOL: str               = os.getenv("ZMQ_PROTOCOL")
    ZMQ_HOST: str                   = os.getenv("ZMQ_HOST")
    ZMQ_PORT: int                   = int(os.getenv("ZMQ_PORT") or 0)
    ZMQ_ADDRESS_BIND: str           = os.getenv("ZMQ_ADDRESS_BIND")
    ZMQ_ADDRESS_CONNECT: str        = os.getenv("ZMQ_ADDRESS_CONNECT")
    ZMQ_TOPIC_TTS: str              = os.getenv("ZMQ_TOPIC_TTS")
    ZMQ_TOPIC_STT: str              = os.getenv("ZMQ_TOPIC_STT")
    ZMQ_TOPIC_NLG: str              = os.getenv("ZMQ_TOPIC_NLG")
    WHISPER_MODEL: str              = os.getenv("WHISPER_MODEL")
    WHISPER_ENERGY_THRESHOLD: int   = int(os.getenv("WHISPER_ENERGY_THRESHOLD") or 0)
    WHISPER_RECORD_TIMEOUT: float   = float(os.getenv("WHISPER_RECORD_TIMEOUT") or 0.0)
    WHISPER_PHRASE_TIMEOUT: float   = float(os.getenv("WHISPER_PHRASE_TIMEOUT") or 0.0)