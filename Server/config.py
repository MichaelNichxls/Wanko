import json
import logging

from typing import Union, List, Dict

# TODO: Singleton or DI
class Config:
    # TODO: Rename
    _config_data = None

    @classmethod
    def load_config(cls, config_file: str) -> None:
        if cls._config_data is not None:
            return
        
        with open(config_file, 'r') as f:
            cls._config_data = json.load(f)
    
    # TODO: Make type alias
    @classmethod
    def get_config(cls) -> Union[str, int, float, List, Dict]:
        if cls._config_data is None:
            logging.warning("Config data has not been loaded")
        
        return cls._config_data