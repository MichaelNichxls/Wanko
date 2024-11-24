using Python.Runtime;
using System;
using System.IO;
using UnityEngine;
using PythonRuntime = Python.Runtime.Runtime;

namespace Wanko.Runtime
{
    // PythonManager?
    public static class PythonEnvironmentSetup
    {
        public static readonly string PyWankoHome;
        public static readonly string PythonHome;
        public static readonly string PythonDLL;
        public static readonly string PythonPath;

        static PythonEnvironmentSetup()
        {
            PyWankoHome = Path.Combine(Application.streamingAssetsPath, "pywanko");
            PythonHome = Directory.GetDirectories(Application.streamingAssetsPath, "python-*-embed-*")[0];
            PythonDLL = Path.Combine(PythonHome, "python312.dll");
            PythonPath = string.Join(
                ';',
                new[]
                {
                    PyWankoHome,
                    PythonHome,
                    Path.Combine(PythonHome, "Lib"),
                    Path.Combine(PythonHome, @"Lib\site-packages")
                });
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

            Application.quitting += PythonEngine.Shutdown;
        }
    }
}