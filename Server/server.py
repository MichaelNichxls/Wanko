import logging
import zmq

from threading import Thread, Event
from time import sleep

# TODO: Redo or rename
from config import Config
from speech_to_text import speech_to_text
from natural_language_generation import natural_language_generation

# TODO: Do this better
Config.load_config("config.json")
config = Config.get_config()

_URL_PUB = "{}://{}:{}".format(config["zmq"]["protocol"], "*", config["zmq"]["port"])
_URL_SUB = "{}://{}:{}".format(config["zmq"]["protocol"], config["zmq"]["host"], config["zmq"]["port"])

if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)
    
    with (
        zmq.Context() as context,
        context.socket(zmq.PUB) as publisher,
        context.socket(zmq.SUB) as subscriber
    ):
        publisher.bind(_URL_PUB)
        logging.info("Binded to socket at \"%s\"", _URL_PUB)
        
        subscriber.connect(_URL_SUB)
        logging.info("Connected to socket at \"%s\"", _URL_SUB)

        stop_event = Event()

        # TODO: Probably don't need kwargs
        stt_thread = Thread(target=speech_to_text, kwargs={"publisher": publisher, "stop_event": stop_event})
        nlg_thread = Thread(target=natural_language_generation, kwargs={"publisher": publisher, "subscriber": subscriber, "stop_event": stop_event})

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