<!-- markdownlint-disable MD033 -->

# Wanko

Initial pre-release

## Prerequisites

- Windows 10 or 11
- Python, specifically [Python 3.11.5](https://www.python.org/downloads/release/python-3115/)
  - Other Python versions have not been tested and may not work
- Optionally, if you have CUDA support, then you may install from one of the following for better performance:
  - [NVIDIA CUDA](https://developer.nvidia.com/cuda-downloads)
  - [NVIDIA cuDNN](https://developer.nvidia.com/cudnn) v7 or above
  - [Compiler](https://gist.github.com/ax3l/9489132) compatible with CUDA

## Setup

- Run `wanko-installer-1.0.0-alpha.2.exe` and go through the installation process
  - ***Warning: You WILL get security warnings, as the executable is not digitally signed. I do NOT plan on purchasing a code signing certificate, either. For full disclosure, this application is NOT a virus, and runs solely on user trust. Continue at your own discretion.***
- Unzip `Server.zip`
- In the root folder, open `config.json` and paste in your [Character.AI](https://beta.character.ai/) token into the `token` field
  - To get your token:
    1. Go to [Character.AI](https://beta.character.ai/), and register or login to an account
    2. Open DevTools
    3. Go to Storage → Local storage → `char_token`
    4. Copy `value`
  - ***Warning: Do NOT share your token, as it is needed for authorization and operation of requests from your account.***

```json
{
    "cai": {
        "token": "",
        "char": "D_ZmzC4orhpu6S7UXmW-PIS0X5qFm1qnzukot7v6vWI"
    }
}
```

- While in the `Server` directory, open a terminal and run the following command to install dependencies:

```bash
pip install -r requirements.txt
```

- Start the server:

```bash
python server.py
```

- Run `Wanko.exe`

## Remarks

- You can stop the server with <kbd>Ctrl</kbd> + <kbd>C</kbd>
- `Wanko.exe` does not currently have a button to exit the program

## Issues

As Wanko is currently in alpha, there will be bugs and possibly performance issues. Please create a Github Issue if any are found.
