using System;
using UnityEngine;

namespace Wanko.Runtime.Utilities.Wrappers
{
    [Serializable]
    public struct Drag
    {
        [field: Range(.01f, 50f)]
        [field: SerializeField]
        public float Speed { get; set; }
    }
}