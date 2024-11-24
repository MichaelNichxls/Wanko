import asyncio

from asyncio import CancelledError

from . import *
from .config import CharacterAIConfig
from .recognition import audio_data_queue
from .transcribe import transcribe_bytes
from .utils import LOGGING

async def main() -> None:
    try:
        me = await CLIENT.get_me()

        async with await CLIENT.connect() as chat:
            new, answer = await chat.new_chat(CharacterAIConfig.CHAR, me.id)
            LOGGING.info("%s: %s", answer.name, answer.text)
            
            while True:
                if audio_data_queue.empty():
                    await asyncio.sleep(0.2)
                    continue

                audio_data = audio_data_queue.get()
                transcription = transcribe_bytes(audio_data)
                LOGGING.debug("User: %s", transcription)

                message = await chat.send_message(CharacterAIConfig.CHAR, new.chat_id, transcription)
                LOGGING.info("%s: %s", message.name, message.text)
                
    # FIXME: close websocket connection properly
    except KeyboardInterrupt:
        LOGGING.info("Session ended")
        pass

if __name__ == "__main__":
    try:
        asyncio.run(main())
    except CancelledError:
        pass