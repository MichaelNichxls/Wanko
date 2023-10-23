using UnityEngine;

namespace Wanko
{
    [RequireComponent(typeof(RectTransform))]
    public class UIScalable : MonoBehaviour
    {
        private RectTransform _rectTransform;

        [field: SerializeField]
        public float ScaleMin { get; private set; } = 0.1f;
        [field: SerializeField]
        public float ScaleMax { get; private set; } = 2f;
        [field: SerializeField]
        public float ScaleSpeed { get; private set; } = 1f;

        private void Start() =>
            _rectTransform = GetComponent<RectTransform>();

        // TODO: Implement
    }
}