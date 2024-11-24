from characterai import aiocai

from .config import CharacterAIConfig

CLIENT = aiocai.Client(CharacterAIConfig.CHAR_TOKEN)