using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Json;
using System;
using System.IO;
using UnityEngine;

namespace Wanko
{
    // TODO: Rename
    public class InitializeModel : MonoBehaviour
    {
        // TODO
        // [field: SerializeField]
        // public string Model { get; private set; }

        private void Start()
        {
            string path = Path.Combine(Application.streamingAssetsPath, "Live2DModels/wanko_vts/wanko.model3.json");
            CubismModel3Json model3Json = CubismModel3Json.LoadAtPath(path, BuiltinLoadAssetAtPath);
            CubismModel model = model3Json.ToModel();
        }

        /// <summary>
        /// Load asset.
        /// </summary>
        /// <param name="assetType">Asset type.</param>
        /// <param name="absolutePath">Path to asset.</param>
        /// <returns>The asset on succes; <see langword="null"> otherwise.</returns>
        public static object BuiltinLoadAssetAtPath(Type assetType, string absolutePath)
        {
            switch (assetType)
            {
                case { } when assetType == typeof(byte[]):
                    return File.ReadAllBytes(absolutePath);

                case { } when assetType == typeof(string):
                    return File.ReadAllText(absolutePath);

                case { } when assetType == typeof(Texture2D):
                    Texture2D texture = new(1, 1);
                    texture.LoadImage(File.ReadAllBytes(absolutePath));

                    return texture;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}