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
    public sealed class UIController
        : MonoBehaviour
        , IPointerEnterHandler, IPointerExitHandler
        , IPointerDownHandler, IPointerUpHandler
        , IDragHandler
        , IScrollHandler
    {
        private RectTransform _rectTransform;
        // Rename
        private Vector2 _position, _offset;
        private Vector3 _scale;
#if !UNITY_EDITOR
        private User32.SetWindowLongFlags _dwNewLong = WS_EX_LAYERED | WS_EX_TRANSPARENT;
#endif
        [field: Header("Graphic")]
        [field: SerializeField]
        public Graphic TargetGraphic { get; private set; }
        [field: SerializeField]
        public Color NormalColor { get; private set; } = new(1f, 1f, 1f, 1f);
        [field: SerializeField]
        public Color HighlightedColor { get; private set; } = new(245f / byte.MaxValue, 245f / byte.MaxValue, 245f / byte.MaxValue, 1f);
        [field: SerializeField]
        public Color PressedColor { get; private set; } = new(200f / byte.MaxValue, 200f / byte.MaxValue, 200f / byte.MaxValue, 1f);
        [field: SerializeField]
        public float FadeDuration { get; private set; } = 0.1f;
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

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
#if !UNITY_EDITOR
            _dwNewLong = WS_EX_LAYERED;
#endif
            TargetGraphic.CrossFadeColor(HighlightedColor, FadeDuration, true, true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
#if !UNITY_EDITOR
            _dwNewLong = WS_EX_LAYERED | WS_EX_TRANSPARENT;
#endif
            TargetGraphic.CrossFadeColor(NormalColor, FadeDuration, true, true);
        }

        // FIXME: "breaks" if you drag too fast
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button is PointerEventData.InputButton.Left or PointerEventData.InputButton.Right)
                return;

            TargetGraphic.CrossFadeColor(PressedColor, FadeDuration, true, true);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData) =>
            TargetGraphic.CrossFadeColor(HighlightedColor, FadeDuration, true, true);

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (eventData.button is PointerEventData.InputButton.Left or PointerEventData.InputButton.Right)
                return;

            _position += eventData.delta;
        }

        void IScrollHandler.OnScroll(PointerEventData eventData)
        {
            _scale += eventData.scrollDelta.y * ScaleFactor * Vector3.one;
            _scale = Vector3.Min(_scale, ScaleMax * Vector3.one);
            _scale = Vector3.Max(_scale, ScaleMin * Vector3.one);
        }

        private void Reset() =>
            TargetGraphic = GetComponent<Graphic>();

        private void Awake() =>
            _rectTransform = transform as RectTransform;

        private void Start()
        {
            _offset = _rectTransform.anchoredPosition;
            _scale = _rectTransform.localScale;

            TargetGraphic.CrossFadeColor(NormalColor, 0f, true, true);
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