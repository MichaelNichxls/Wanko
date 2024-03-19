import logging
import zmq

from config import Config
# TODO: as
from characterai import PyCAI, errors
from threading import Event
from typing import Optional
# from time import sleep

# TODO: Rename to generate_text()?
# TODO: from zmq import Socket idfk
def natural_language_generation(*, publisher: zmq.Socket, subscriber: zmq.Socket, stop_event: Optional[Event] = None) -> None:
    client = PyCAI(Config.CAI_TOKEN)
    # TODO: Log

    chat = client.chat.get_chat(Config.CAI_CHAR)
    # TODO: Log

    # TODO: Dataclass
    participants = chat["participants"]
    is_human = participants[0]["is_human"]
    tgt = participants[int(is_human)]["user"]["username"]

    # TODO: Redo logging
    subscriber.subscribe(Config.ZMQ_TOPIC_STT)
    logging.info("Subscribed to \"%s\" topic", Config.ZMQ_TOPIC_STT)

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
            publisher.send_string(Config.ZMQ_TOPIC_NLG, zmq.SNDMORE)
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
        publisher.bind(Config.ZMQ_ADDRESS_BIND)
        logging.info("Binded to socket at \"%s\"", Config.ZMQ_ADDRESS_BIND)

        subscriber.connect(Config.ZMQ_ADDRESS_CONNECT)
        logging.info("Connected to socket at \"%s\"", Config.ZMQ_ADDRESS_CONNECT)

        natural_language_generation(publisher=publisher, subscriber=subscriber)