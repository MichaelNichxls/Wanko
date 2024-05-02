from time import sleep

from . import *
from .utils import LOGGING

generator = Generator()
recognizer = Recognizer()
transcriber = Transcriber()
LOGGING.info("Wanko initialized; Listening for audio input")

try:
    while True:
        if recognizer.audio_data_queue.empty():
            sleep(0.25)
            continue

        audio_data = recognizer.dequeue_audio_data()
        transcription = transcriber.transcribe_bytes(audio_data)
        LOGGING.debug("User: %s", transcription)

        response = generator.send_message(transcription)
        name, reply = response.src_char.participant.name, response.replies[0].text
        LOGGING.info("%s: %s", name, reply)

except KeyboardInterrupt:
    LOGGING.info("Session ended")
    pass