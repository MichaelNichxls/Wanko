using UnityEngine;
using UnityEngine.EventSystems;
using Wanko.Runtime.Managers;

namespace Wanko.Runtime.Controllers
{
    [DisallowMultipleComponent]
    public sealed class UIController : MonoBehaviour, IDragHandler, IScrollHandler, IWindowClickthroughHandler
    {
        private RectTransform _rectTransform;
        private Vector3 _position, _offset, _scale;

        [field: SerializeField]
        public float DragSpeed { get; private set; } = 10f;
        [field: SerializeField]
        public float ScaleMin { get; private set; } = .5f;
        [field: SerializeField]
        public float ScaleMax { get; private set; } = 4f;
        [field: SerializeField]
        public float ScaleFactor { get; private set; } = .1f;
        [field: SerializeField]
        public float ScaleSpeed { get; private set; } = 10f;

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            _position += (Vector3)eventData.delta;
        }

        void IScrollHandler.OnScroll(PointerEventData eventData)
        {
            _scale += eventData.scrollDelta.y / 6f * ScaleFactor * Vector3.one;
            _scale = Vector3.Min(_scale, ScaleMax * Vector3.one);
            _scale = Vector3.Max(_scale, ScaleMin * Vector3.one);
        }

        bool IWindowClickthroughHandler.SetClickthrough(Vector3 position) =>
            !EventSystem.current.IsPointerOverGameObject();

        private void Awake() =>
            _rectTransform = transform as RectTransform;

        private void Start()
        {
            _position = _rectTransform.position;
            _scale = _rectTransform.localScale;
        }

        private void Update()
        {
            _rectTransform.position = Vector3.Lerp(_rectTransform.position, _position + _offset, Time.deltaTime * DragSpeed);
            _rectTransform.localScale = Vector3.Lerp(_rectTransform.localScale, _scale, Time.deltaTime * ScaleSpeed);
        }
    }
}