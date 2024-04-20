using System;
using UnityEngine;

namespace Wanko.Runtime.Utilities
{
    [Serializable]
    public struct DummyTransform
    {
        [field: SerializeField]
        public Vector3 Position { get; set; }

        [field: SerializeField]
        public Quaternion Rotation { get; set; }

        [field: SerializeField]
        public Vector3 Scale { get; set; }

        public DummyTransform(Transform transform)
        {
            Position = transform.position;
            Rotation = transform.rotation;
            Scale = transform.localScale;
        }

        public static explicit operator DummyTransform(Transform transform) =>
            new(transform);
    }
}