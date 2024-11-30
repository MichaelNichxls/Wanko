using System;
using UnityEngine;

namespace Wanko.Utilities.Serializable
{
    [Serializable]
    public struct MoveOptions
    {
        [field: SerializeField]
        [field: Range(.01f, 50f)]
        public float LerpFactor { get; set; }
    }
}