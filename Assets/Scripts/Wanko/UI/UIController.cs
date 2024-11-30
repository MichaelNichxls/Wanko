using UnityEngine;
using UnityEngine.EventSystems;
using Wanko.Utilities.Serializable;
using Wanko.Window;
using static UnityEngine.EventSystems.PointerEventData;

namespace Wanko.UI
{
    [DisallowMultipleComponent]
    public sealed class UIController : MonoBehaviour, IDragHandler, IScrollHandler, IWindowTransparentHandler
    {
        private RectTransform _rectTransform;
        private DummyTransform _target;

        [field: SerializeField]
        public MoveOptions Move { get; private set; } = new() { LerpFactor = 10f };
        [field: SerializeField]
        public ScaleOptions Scale { get; private set; } = new() { Range = new Vector2(.5f, 4f), Factor = .1f, LerpFactor = 10f };

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (eventData.button != InputButton.Left)
                return;

            _target.position += (Vector3)eventData.delta;
        }

        void IScrollHandler.OnScroll(PointerEventData eventData)
        {
            _target.localScale += eventData.scrollDelta.y / 6f * Scale.Factor * Vector3.one;
            _target.localScale = Vector3.Min(_target.localScale, Scale.Range.y * Vector3.one);
            _target.localScale = Vector3.Max(_target.localScale, Scale.Range.x * Vector3.one);
        }

        bool IWindowTransparentHandler.SetTransparent(Vector2 position) =>
            !EventSystem.current.IsPointerOverGameObject();

        private void Awake() =>
            _rectTransform = transform as RectTransform;

        private void Start() =>
            _target = (DummyTransform)_rectTransform;

        private void Update()
        {
            _rectTransform.position = Vector3.Lerp(_rectTransform.position, _target.position, Time.deltaTime * Move.LerpFactor);
            _rectTransform.localScale = Vector3.Lerp(_rectTransform.localScale, _target.localScale, Time.deltaTime * Scale.LerpFactor);
        }
    }
}