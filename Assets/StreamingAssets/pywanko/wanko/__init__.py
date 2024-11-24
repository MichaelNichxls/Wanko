import logging

from environs import Env

_ENV = Env()
logging.basicConfig(level=_ENV.log_level("LOG_LEVEL", logging.INFO))

from .generation import CLIENT
from .recognition import RECOGNIZER, MICROPHONE
from .transcribe import MODEL
from .version import __version__

__all__ = [
    "CLIENT",
    "RECOGNIZER",
    "MICROPHONE",
    "MODEL",
    "__version__"
]