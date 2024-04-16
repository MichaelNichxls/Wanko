using System;
using UnityEngine;
using UnityEngine.EventSystems;
#if !UNITY_EDITOR
using Wanko.Native;
using static Wanko.Native.User32.SetWindowLongFlags;
using static Wanko.Native.User32.WindowLongIndexFlags;
#endif

namespace Wanko.Controllers
{
    [DisallowMultipleComponent]
    public sealed class UIController : MonoBehaviour, IDragHandler, IScrollHandler
#if !UNITY_EDITOR
        , IPointerEnterHandler, IPointerExitHandler
#endif
    {
        private RectTransform _rectTransform;
        private Vector3 _position, _offset, _scale;
#if !UNITY_EDITOR
        private User32.SetWindowLongFlags _dwNewLong = WS_EX_LAYERED | WS_EX_TRANSPARENT;
#endif
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
#if !UNITY_EDITOR
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) =>
            _dwNewLong = WS_EX_LAYERED;
            
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData) =>
            _dwNewLong = WS_EX_LAYERED | WS_EX_TRANSPARENT;
#endif
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

        public unsafe void SetWindowLong(IntPtr hWnd)
        {
#if !UNITY_EDITOR
            if ((_dwNewLong & WS_EX_TRANSPARENT) != 0)
                return;

            User32.SetWindowLongPtr(hWnd, GWL_EXSTYLE, (void*)(int)_dwNewLong);
#endif
        }
    }
}