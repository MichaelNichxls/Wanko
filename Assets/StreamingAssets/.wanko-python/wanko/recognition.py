import speech_recognition as sr

from queue import Queue

from .config import SpeechRecognitionConfig as SRConfig

class Recognizer:
    def __init__(self) -> None:
        self.audio_data_queue = Queue()

        self.recorder = sr.Recognizer()
        self.recorder.energy_threshold = SRConfig.Recognizer.ENERGY_THRESHOLD
        self.recorder.dynamic_energy_threshold = SRConfig.Recognizer.DYNAMIC_ENERGY_THRESHOLD
        self.recorder.dynamic_energy_adjustment_damping = SRConfig.Recognizer.DYNAMIC_ENERGY_ADJUSTMENT_DAMPING
        self.recorder.dynamic_energy_ratio = SRConfig.Recognizer.DYNAMIC_ENERGY_RATIO
        self.recorder.pause_threshold = SRConfig.Recognizer.PAUSE_THRESHOLD
        self.recorder.operation_timeout = SRConfig.Recognizer.OPERATION_TIMEOUT
        self.recorder.phrase_threshold = SRConfig.Recognizer.PHRASE_THRESHOLD
        self.recorder.non_speaking_duration = SRConfig.Recognizer.NON_SPEAKING_DURATION

        self.source = sr.Microphone(
            device_index=SRConfig.Microphone.DEVICE_INDEX,
            sample_rate=SRConfig.Microphone.SAMPLE_RATE,
            chunk_size=SRConfig.Microphone.CHUNK_SIZE
        )

        if SRConfig.Recognizer.ADJUST_FOR_AMBIENT_NOISE:
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