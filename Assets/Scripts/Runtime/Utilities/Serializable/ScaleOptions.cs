using System;
using UnityEngine;

namespace Wanko.Runtime.Utilities.Serializable
{
    [Serializable]
    public struct ScaleOptions
    {
        // TODO: change range and normalize
        [field: SerializeField]
        [field: Min(.01f)]
        public float Min { get; set; }
        [field: SerializeField]
        [field: Min(.01f)]
        public float Max { get; set; }
        [field: SerializeField]
        [field: Range(.01f, 5f)]
        public float Factor { get; set; }
        [field: SerializeField]
        [field: Range(.01f, 50f)]
        public float Speed { get; set; }
    }
}