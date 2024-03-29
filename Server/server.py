import logging

from time import sleep

# TODO: Redo or rename
from natural_language_generation import NLGThread
from speech_to_text import STTThread

if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)
    
    with (
        STTThread() as stt_thread,
        NLGThread() as nlg_thread
    ):
        try:
            stt_thread.start()
            nlg_thread.start()
            
            # TODO: Add comment
            while True:
                sleep(1)
        except KeyboardInterrupt:
            stt_thread.stop_event.set()
            nlg_thread.stop_event.set()
        finally:
            stt_thread.join()
            nlg_thread.join()