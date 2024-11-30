using System;
using UnityEngine;
using Wanko.Utilities.Attributes;

namespace Wanko.Utilities.Serializable
{
    [Serializable]
    public struct ScaleOptions
    {
        [field: SerializeField]
        [field: MinMaxSlider(.01f, 50f)]
        public Vector2 Range { get; set; }
        [field: SerializeField]
        [field: Range(.01f, 5f)]
        public float Factor { get; set; }
        [field: SerializeField]
        [field: Range(.01f, 50f)]
        public float LerpFactor { get; set; }
    }
}