using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Wanko.Runtime.Utilities.Serializable
{
    [Serializable]
    public struct DummyTransform
    {
        [field: SerializeField]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Consistency")]
        public Vector3 position { get; set; }
        [field: SerializeField]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Consistency")]
        public Quaternion rotation { get; set; }
        [field: SerializeField]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Consistency")]
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