# Character.AI
CAI_TOKEN=
CAI_CHAR=D_ZmzC4orhpu6S7UXmW-PIS0X5qFm1qnzukot7v6vWI

# ZeroMQ
ZMQ_PROTOCOL=tcp
ZMQ_HOST=localhost
ZMQ_PORT=9265
ZMQ_ADDRESS_BIND=${ZMQ_PROTOCOL}://*:${ZMQ_PORT}
ZMQ_ADDRESS_CONNECT=${ZMQ_PROTOCOL}://${ZMQ_HOST}:${ZMQ_PORT}
# Speech-To-Text
ZMQ_TOPIC_STT=Wanko.STT
# Text-To-Speech
ZMQ_TOPIC_TTS=Wanko.TTS
# Natural Language Generation
ZMQ_TOPIC_NLG=Wanko.NLG

# OpenAI Whisper
WHISPER_MODEL=base.en
WHISPER_ENERGY_THRESHOLD=1000
WHISPER_RECORD_TIMEOUT=2.0
WHISPER_PHRASE_TIMEOUT=2.0