import logging
import os
import zmq

# TODO: as
from characterai import PyCAI, errors
from config import Config
from dotenv import load_dotenv
from threading import Event
from typing import Optional
# from time import sleep

load_dotenv()

# TODO: Do this better, like an __init__ file
Config.load_config("config.json")
config = Config.get_config()

_URL_PUB = "{}://{}:{}".format(config["zmq"]["protocol"], "*", config["zmq"]["port"])
_URL_SUB = "{}://{}:{}".format(config["zmq"]["protocol"], config["zmq"]["host"], config["zmq"]["port"])

_TOPIC_STT = config["zmq"]["topics"]["speech_to_text"]
_TOPIC_NLG = config["zmq"]["topics"]["natural_language_generation"]

# TODO: "char_token", or whatever
# FIXME: Other way around
_TOKEN = config["cai"]["token"] or os.getenv("CAI_TOKEN")
_CHAR = config["cai"]["char"]

# TODO: Rename to generate_text()?
# TODO: from zmq import Socket idfk
def natural_language_generation(*, publisher: zmq.Socket, subscriber: zmq.Socket, stop_event: Optional[Event] = None) -> None:
    client = PyCAI(_TOKEN)
    # TODO: Log

    chat = client.chat.get_chat(_CHAR)
    # TODO: Log

    # TODO: Dataclass
    participants = chat["participants"]
    is_human = participants[0]["is_human"]
    tgt = participants[int(is_human)]["user"]["username"]

    # TODO: Redo logging
    subscriber.subscribe(_TOPIC_STT)
    logging.info("Subscribed to \"%s\" topic", _TOPIC_STT)

    poller = zmq.Poller()
    poller.register(subscriber, zmq.POLLIN)

    # TODO: Subclass; consistency
    # BUG: "The process tried to write to a nonexistent pipe." on KeyboardInterrupt
    while stop_event is None or not stop_event.is_set():
        try:
            # TODO: Tweak timeout
            sockets = dict(poller.poll(timeout=1_000))
            
            if subscriber not in sockets or sockets[subscriber] != zmq.POLLIN:
                continue

            # TODO: Comment
            message = subscriber.recv_multipart()[1].decode("utf-8")
            data = client.chat.send_message(chat["external_id"], tgt, message)

            name = data["src_char"]["participant"]["name"]
            text = data["replies"][0]["text"]

            print(f"{name}: {text}")

            # TODO: Log
            publisher.send_string(_TOPIC_NLG, zmq.SNDMORE)
            publisher.send_string(text)
        # FIXME
        # TODO: sleep()
        except errors.ServerError as e:
            logging.exception(e.__class__.__name__)
        except KeyboardInterrupt as e:
            logging.info("Program interrupted by user")
            break

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

        natural_language_generation(publisher=publisher, subscriber=subscriber)