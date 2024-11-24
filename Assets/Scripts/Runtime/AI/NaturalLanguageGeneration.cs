using Python.Runtime;
using System.IO;
using UnityEngine;

namespace Wanko.Runtime.AI
{
    [DisallowMultipleComponent]
    public sealed class NaturalLanguageGeneration : MonoBehaviour
    {
        private void Start()
        {
            using var _ = Py.GIL();

            using PyObject environs = PyModule.Import("environs");
            using PyObject env = environs.GetAttr("Env");
            using PyObject ENV = env.Invoke();

            ENV.InvokeMethod("read_env", Path.Combine(Application.streamingAssetsPath, @"pywanko\.env").ToPython());
            ENV.InvokeMethod("read_env", Path.Combine(Application.streamingAssetsPath, @"pywanko\.env.public").ToPython());

            // TODO: Implement, lol
        }
    }
}