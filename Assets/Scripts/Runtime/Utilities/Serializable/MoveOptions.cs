using System;
using UnityEngine;

namespace Wanko.Runtime.Utilities.Serializable
{
    [Serializable]
    public struct MoveOptions
    {
        // TODO: change range and normalize
        [field: SerializeField]
        [field: Range(.01f, 50f)]
        public float Speed { get; set; }
    }
}