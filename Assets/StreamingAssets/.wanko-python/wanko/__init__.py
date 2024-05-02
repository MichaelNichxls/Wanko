import logging

from environs import Env

_ENV = Env()
logging.basicConfig(level=_ENV.log_level("LOG_LEVEL", logging.INFO))

from .generation import Generator
from .recognition import Recognizer
from .transcribe import Transcriber
from .version import __version__

__all__ = [
    "Generator",
    "Recognizer",
    "Transcriber",
    "__version__"
]