using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Wanko.Utilities.Serializable
{
    [Serializable]
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Consistency")]
    public struct DummyTransform
    {
        [field: SerializeField]
        public Vector3 position { get; set; }
        [field: SerializeField]
        public Quaternion rotation { get; set; }
        [field: SerializeField]
        public Vector3 localScale { get; set; }

        public DummyTransform(Transform transform)
        {
            position = transform.position;
            rotation = transform.rotation;
            localScale = transform.localScale;
        }

        public static explicit operator DummyTransform(Transform transform) =>
            new(transform);
    }
}