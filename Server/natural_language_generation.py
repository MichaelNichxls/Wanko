import logging
import zmq

from characterai import PyCAI
from config import Config
from dataclasses import dataclass, field
from pipeline_thread import PipelineThread
from typing import List, Optional, override

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
    
    # TODO: __repr__

# name alias
class NLGThread(PipelineThread):
    def __init__(self):
        # FIXME:
        super().__init__(recv_addr=Config.ZMQ_ADDR_STT, send_addr=Config.ZMQ_ADDR_NLG)

        # TODO: Log
        self.cai_client = PyCAI(Config.CAI_TOKEN)
        self.cai_chat = Chat(**self.cai_client.chat.get_chat(Config.CAI_CHAR))
        # make prop or method
        self._cai_chat_tgt = [participant.user.username for participant in self.cai_chat.participants if not participant.is_human][0]
    
    # use events
    @override
    def run(self) -> None:
        super().run()

        # TODO: refactor further
        while not self.stop_event.is_set():
            # TODO: Tweak timeout; optimize; possibly do without a poller
            polled_sockets = dict(self.poller.poll(timeout=500))
            
            if polled_sockets.get(self.pull_socket) != zmq.POLLIN:
                continue

            # TODO: Comment
            # FIXME
            # TODO: zmq.DONTWAIT
            message = self.pull_socket.recv_string()
            data = Data(**self.cai_client.chat.send_message(self.cai_chat.external_id, self._cai_chat_tgt, message))

            name, text = data.src_char.participant.name, data.replies[0].text

            # TODO: logging.debug
            print(f"{name}: {text}")

            # TODO: Log
            # FIXME: do this in a non-blocking manner
            try:
                self.push_socket.send_string(text, zmq.DONTWAIT)
            except zmq.Again:
                pass

            # Where does this go again
            # except errors.ServerError as e:
            #     logging.exception(e.__class__.__name__)
                
# TODO: from zmq import Socket

if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)

    with NLGThread() as nlg_thread:
        try:
            nlg_thread.run()
        except KeyboardInterrupt:
            nlg_thread.stop_event.set()