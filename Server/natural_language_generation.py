import logging
import zmq

from config import Config
# TODO: as
from characterai import PyCAI, errors
from dataclasses import dataclass, field
from threading import Event
from typing import Optional, List
# from time import sleep

@dataclass(kw_only=True)
class Account:
    name: str
    avatar_type: str
    onboarding_complete: bool
    avatar_file_name: str
    mobile_onboarding_complete: int

@dataclass(kw_only=True)
class User:
    username: str
    id: int
    first_name: str
    account: Optional[Account]
    is_staff: bool

    def __post_init__(self):
        self.account = Account(**self.account) if self.account is not None else None

@dataclass(kw_only=True)
class Participant:
    user: Optional[User] = None
    is_human: Optional[bool] = None
    name: str
    num_interactions: Optional[int] = None

    def __post_init__(self):
        self.user = User(**self.user) if self.user is not None else None

# frozen
@dataclass(kw_only=True)
class Chat:
    title: str
    # union
    # default factory needed?
    participants: List[Participant] = field(default_factory=list)
    external_id: str
    created: str
    last_interaction: str
    type: str
    description: str

    # TODO: Type check
    def __post_init__(self):
        self.participants = [Participant(**participant) for participant in self.participants]

@dataclass(kw_only=True)
class Reply:
    text: str
    uuid: str
    id: int

# rename
@dataclass(kw_only=True)
class SrcChar:
    participant: Participant
    avatar_file_name: str

    def __post_init__(self):
        self.participant = Participant(**self.participant)

# rename
@dataclass(kw_only=True)
class Data:
    replies: List[Reply] = field(default_factory=list)
    src_char: SrcChar
    is_final_chunk: bool
    last_user_msg_id: int
    last_user_msg_uuid: str

    def __post_init__(self):
        self.replies = [Reply(**reply) for reply in self.replies]
        self.src_char = SrcChar(**self.src_char)

# TODO: Rename to generate_text()?
# TODO: from zmq import Socket idfk
def natural_language_generation(*, publisher: zmq.Socket, subscriber: zmq.Socket, stop_event: Optional[Event] = None) -> None:
    client = PyCAI(Config.CAI_TOKEN)
    # TODO: Log

    chat = Chat(**client.chat.get_chat(Config.CAI_CHAR))
    # TODO: Log

    tgt = [participant.user.username for participant in chat.participants if not participant.is_human][0]

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
            data = Data(**client.chat.send_message(chat.external_id, tgt, message))

            # inline
            name = data.src_char.participant.name
            text = data.replies[0].text

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