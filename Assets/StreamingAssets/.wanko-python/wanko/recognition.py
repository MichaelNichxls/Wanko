import speech_recognition as sr

from queue import Queue

from .config import RecognizerConfig, MicrophoneConfig

class Recognizer:
    def __init__(self) -> None:
        self.audio_data_queue = Queue()

        self.recorder = sr.Recognizer()
        self.recorder.energy_threshold                  = RecognizerConfig.ENERGY_THRESHOLD
        self.recorder.dynamic_energy_threshold          = RecognizerConfig.DYNAMIC_ENERGY_THRESHOLD
        self.recorder.dynamic_energy_adjustment_damping = RecognizerConfig.DYNAMIC_ENERGY_ADJUSTMENT_DAMPING
        self.recorder.dynamic_energy_ratio              = RecognizerConfig.DYNAMIC_ENERGY_RATIO
        self.recorder.pause_threshold                   = RecognizerConfig.PAUSE_THRESHOLD
        self.recorder.operation_timeout                 = RecognizerConfig.OPERATION_TIMEOUT
        self.recorder.phrase_threshold                  = RecognizerConfig.PHRASE_THRESHOLD
        self.recorder.non_speaking_duration             = RecognizerConfig.NON_SPEAKING_DURATION

        self.source = sr.Microphone(
            MicrophoneConfig.DEVICE_INDEX,
            MicrophoneConfig.SAMPLE_RATE,
            MicrophoneConfig.CHUNK_SIZE
        )

        if RecognizerConfig.ADJUST_FOR_AMBIENT_NOISE:
            with self.source:
                self.recorder.adjust_for_ambient_noise(self.source)
        
        def record_callback(_, audio: sr.AudioData) -> None:
            """
            Threaded callback function to receive audio data when recordings finish.
            audio: An AudioData containing the recorded bytes.
            """
            audio_data: bytes = audio.get_raw_data()
            self.audio_data_queue.put(audio_data)
        
        self.recorder.listen_in_background(self.source, record_callback)
    
    def dequeue_audio_data(self) -> bytes:
        audio_data = b"".join(self.audio_data_queue.queue)
        self.audio_data_queue.queue.clear()

        return audio_data