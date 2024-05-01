using Python.Runtime;
using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using PythonRuntime = Python.Runtime.Runtime;

namespace Wanko.Runtime
{
    [DisallowMultipleComponent]
    internal sealed class PythonEnvironmentSetup : MonoBehaviour
    {
        public static readonly string WankoPythonHome;
        public static readonly string PythonHome;
        public static readonly string PythonDLL;
        public static readonly string PythonPath;

        static PythonEnvironmentSetup()
        {
            WankoPythonHome = Path.Combine(Application.streamingAssetsPath, ".wanko-python");
            PythonHome = Path.Combine(Application.streamingAssetsPath, ".python-3.12.2-embed-amd64");
            PythonDLL = Path.Combine(PythonHome, "python312.dll");
            PythonPath = string.Join(
                ';',
                new[]
                {
                    WankoPythonHome,
                    PythonHome,
                    Path.Combine(PythonHome, "Lib"),
                    Path.Combine(PythonHome, @"Lib\site-packages")
                });
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void OnSubsystemRegistration()
        {
            // TODO: Make into separate script
            using Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = $"\"{Path.Combine(PythonHome, "python.exe")}\"",
                    Arguments = $"-m pip install -r \"{Path.Combine(WankoPythonHome, "requirements.txt")}\""
                }
            };

            process.Start();
            process.WaitForExit();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
            Environment.SetEnvironmentVariable("PYTHONHOME", PythonHome, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("PYTHONPATH", PythonPath, EnvironmentVariableTarget.Process);

            PythonRuntime.PythonDLL = PythonDLL;
            PythonEngine.PythonHome = PythonHome;
            PythonEngine.PythonPath = PythonPath;

            PythonEngine.Initialize();
        }
    }
}