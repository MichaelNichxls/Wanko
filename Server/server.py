import logging
import zmq

from config import Config
from threading import Thread, Event
from time import sleep

# TODO: Redo or rename
from speech_to_text import speech_to_text as stt
from natural_language_generation import natural_language_generation as nlg

if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)
    
    with (
        zmq.Context() as context,
        context.socket(zmq.PUB) as publisher,
        context.socket(zmq.SUB) as subscriber
    ):
        publisher.bind(Config.ZMQ_ADDRESS_BIND)
        logging.info("Binded to socket at \"%s\"", Config.ZMQ_ADDRESS_BIND)
        
        subscriber.connect(Config.ZMQ_ADDRESS_CONNECT)
        logging.info("Connected to socket at \"%s\"", Config.ZMQ_ADDRESS_CONNECT)

        stop_event = Event()

        # TODO: Probably don't need kwargs
        stt_thread = Thread(target=stt, kwargs={"publisher": publisher, "stop_event": stop_event})
        nlg_thread = Thread(target=nlg, kwargs={"publisher": publisher, "subscriber": subscriber, "stop_event": stop_event})

        try:
            stt_thread.start()
            nlg_thread.start()
            
            # TODO: Add comment
            while True:
                sleep(1)
        except KeyboardInterrupt:
            logging.info("Program interrupted by user")
            stop_event.set()
        finally:
            stt_thread.join()
            nlg_thread.join()