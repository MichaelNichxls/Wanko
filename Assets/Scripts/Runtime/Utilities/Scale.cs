using System;
using UnityEngine;

namespace Wanko.Runtime.Utilities
{
    [Serializable]
    public struct Scale
    {
        [field: Min(.01f)]
        [field: SerializeField]
        public float Min { get; set; }

        [field: Min(.01f)]
        [field: SerializeField]
        public float Max { get; set; }

        [field: Range(.01f, 5f)]
        [field: SerializeField]
        public float Factor { get; set; }

        [field: Range(.01f, 50f)]
        [field: SerializeField]
        public float Speed { get; set; }
    }
}