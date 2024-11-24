using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

// TODO: python script context menu
namespace Wanko.Editor
{
    [InitializeOnLoad]
    public static class PythonEmbedSetup
    {
        static PythonEmbedSetup()
        {
            if (Directory.GetDirectories(Application.streamingAssetsPath, "python-*-embed-*").Any())
                return;

            using Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Application.streamingAssetsPath, @"pywanko\requirements-install.bat"),
                    WorkingDirectory = Application.streamingAssetsPath
                }
            };

            process.Start();
        }
    }
}