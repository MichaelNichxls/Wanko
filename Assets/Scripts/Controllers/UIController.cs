//#undef UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if !UNITY_EDITOR
using Wanko.Native;
using static Wanko.Native.User32.SetWindowLongFlags;
using static Wanko.Native.User32.WindowLongIndexFlags;
#endif

namespace Wanko.Controllers
{
    [DisallowMultipleComponent]
    public sealed class UIController : Selectable, IDragHandler, IScrollHandler
    {
        private RectTransform _rectTransform;
        // Rename
        private Vector2 _position, _offset;
        private Vector3 _scale;
#if !UNITY_EDITOR
        private User32.SetWindowLongFlags _dwNewLong = WS_EX_LAYERED | WS_EX_TRANSPARENT;
#endif
        [field: Header("Drag")]
        [field: SerializeField]
        public float DragSpeed { get; private set; } = 10f;

        [field: Header("Scale")]
        [field: SerializeField]
        public float ScaleMin { get; private set; } = .5f;
        [field: SerializeField]
        public float ScaleMax { get; private set; } = 4f;
        [field: SerializeField]
        public float ScaleFactor { get; private set; } = .025f;
        [field: SerializeField]
        public float ScaleSpeed { get; private set; } = 10f;

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            _position += eventData.delta;
        }

        void IScrollHandler.OnScroll(PointerEventData eventData)
        {
            _scale += eventData.scrollDelta.y * ScaleFactor * Vector3.one;
            _scale = Vector3.Min(_scale, ScaleMax * Vector3.one);
            _scale = Vector3.Max(_scale, ScaleMin * Vector3.one);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
#if !UNITY_EDITOR
            _dwNewLong = WS_EX_LAYERED;
#endif
        }

        // FIXME: Graphics aren't exactly synced with dragging, as dragging is lerped
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            DoStateTransition(SelectionState.Normal, false);
#if !UNITY_EDITOR
            _dwNewLong = WS_EX_LAYERED | WS_EX_TRANSPARENT;
#endif
        }

        protected override void Awake()
        {
            base.Awake();
            _rectTransform = transform as RectTransform;
        }

        protected override void Start()
        {
            base.Start();
            _offset = _rectTransform.anchoredPosition;
            _scale = _rectTransform.localScale;
        }

        private void Update()
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(_rectTransform.anchoredPosition, _position + _offset, Time.deltaTime * DragSpeed);
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