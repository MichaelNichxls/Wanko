using Python.Runtime;
using TMPro;
using UnityEngine;
using PythonRuntime = Python.Runtime.Runtime;

namespace Wanko.Runtime // meow this is being stupid with the namespaces
{
    // rename to PythonManager
    [DisallowMultipleComponent]
    public sealed class SpeechRecognition : MonoBehaviour
    {
        [field: SerializeField]
        public TMP_Text Text { get; private set; }

        private void Awake()
        {
            //string venvPath = @"path\to\env";

            //string path = Environment.GetEnvironmentVariable("PATH").TrimEnd(';');
            //path = string.IsNullOrEmpty(path) ? venvPath : path + ";" + venvPath;
            //Environment.SetEnvironmentVariable("PATH", path, EnvironmentVariableTarget.Process);
            //Environment.SetEnvironmentVariable("PYTHONHOME", venvPath, EnvironmentVariableTarget.Process);

            PythonRuntime.PythonDLL = @"python312.dll"; // can also be absolute path
            
            PythonEngine.Initialize();
            //PythonEngine.BeginAllowThreads();
        }

        private void Start()
        {
            using var _ = Py.GIL();
            using PyObject platform = PyModule.Import("platform");

            Text.text = platform.InvokeMethod("python_version").As<string>();
        }
    }
}