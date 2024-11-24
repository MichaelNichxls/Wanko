import speech_recognition as sr

from queue import Queue

from .config import RecognizerConfig, MicrophoneConfig

RECOGNIZER = sr.Recognizer()
RECOGNIZER.energy_threshold                  = RecognizerConfig.ENERGY_THRESHOLD
RECOGNIZER.dynamic_energy_threshold          = RecognizerConfig.DYNAMIC_ENERGY_THRESHOLD
RECOGNIZER.dynamic_energy_adjustment_damping = RecognizerConfig.DYNAMIC_ENERGY_ADJUSTMENT_DAMPING
RECOGNIZER.dynamic_energy_ratio              = RecognizerConfig.DYNAMIC_ENERGY_RATIO
RECOGNIZER.pause_threshold                   = RecognizerConfig.PAUSE_THRESHOLD
RECOGNIZER.operation_timeout                 = RecognizerConfig.OPERATION_TIMEOUT
RECOGNIZER.phrase_threshold                  = RecognizerConfig.PHRASE_THRESHOLD
RECOGNIZER.non_speaking_duration             = RecognizerConfig.NON_SPEAKING_DURATION

MICROPHONE = sr.Microphone(
    MicrophoneConfig.DEVICE_INDEX,
    MicrophoneConfig.SAMPLE_RATE,
    MicrophoneConfig.CHUNK_SIZE
)

if RecognizerConfig.ADJUST_FOR_AMBIENT_NOISE:
    with MICROPHONE:
        RECOGNIZER.adjust_for_ambient_noise(MICROPHONE)

audio_data_queue = Queue[bytes]()

def _callback(_, audio: sr.AudioData) -> None:
    """
    Threaded callback function to receive audio data when recordings finish.
    audio: An AudioData containing the recorded bytes.
    """
    audio_data: bytes = audio.get_raw_data()
    audio_data_queue.put(audio_data)

RECOGNIZER.listen_in_background(MICROPHONE, _callback)