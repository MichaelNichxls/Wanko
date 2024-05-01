from characterai import PyCAI
from dataclasses import dataclass, field
from typing import List, Optional

from .config import CharacterAIConfig as CAIConfig

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
        self.client = PyCAI(CAIConfig.CHAR_TOKEN)
        self.chat = Chat(**self.client.chat.get_chat(CAIConfig.CHAR))
    
    @property
    def _tgt(self) -> str:
        return [participant.user.username for participant in self.chat.participants if not participant.is_human][0]
    
    def send_message(self, message: str) -> Response:
        return Response(**self.client.chat.send_message(self.chat.external_id, self._tgt, message))