import logging

from characterai import PyCAI
from dataclasses import dataclass, field
from typing import List, Optional

from .config import Config

@dataclass(kw_only=True)
class Account:
    name: str
    avatar_type: str
    avatar_file_name: str
    onboarding_complete: bool
    mobile_onboarding_complete: int

@dataclass(kw_only=True)
class User:
    id: int
    username: str
    first_name: str
    is_staff: bool
    account: Optional[Account]

    def __post_init__(self) -> None:
        self.account = Account(**self.account) if self.account is not None else None

@dataclass(kw_only=True)
class Participant:
    name: str
    user: Optional[User] = None
    is_human: Optional[bool] = None
    num_interactions: Optional[int] = None

    def __post_init__(self) -> None:
        self.user = User(**self.user) if self.user is not None else None

@dataclass(kw_only=True)
class Chat:
    participants: List[Participant] = field(default_factory=list)
    type: str
    title: str
    description: str
    last_interaction: str
    external_id: str
    created: str

    def __post_init__(self) -> None:
        self.participants = [Participant(**participant) for participant in self.participants]

@dataclass(kw_only=True)
class Reply:
    id: int
    uuid: str
    text: str

@dataclass(kw_only=True)
class Character:
    participant: Participant
    avatar_file_name: str

    def __post_init__(self) -> None:
        self.participant = Participant(**self.participant)

@dataclass(kw_only=True)
class Response:
    replies: List[Reply] = field(default_factory=list)
    src_char: Character
    is_final_chunk: bool
    last_user_msg_id: int
    last_user_msg_uuid: str

    def __post_init__(self) -> None:
        self.replies = [Reply(**reply) for reply in self.replies]
        self.src_char = Character(**self.src_char)

class Generator:
    def __init__(self) -> None:
        self._client = PyCAI(Config.CAI_TOKEN)
        self._chat = Chat(**self._client.chat.get_chat(Config.CAI_CHAR))
        self._chat_tgt = [participant.user.username for participant in self._chat.participants if not participant.is_human][0]
    
    def send_message(self, message: str) -> Response:
        return Response(**self._client.chat.send_message(self._chat.external_id, self._chat_tgt, message))
        # name, text = response.src_char.participant.name, response.replies[0].text

if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)