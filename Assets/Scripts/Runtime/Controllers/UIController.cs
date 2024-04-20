using UnityEngine;
using UnityEngine.EventSystems;
using Wanko.Runtime.Managers;
using Wanko.Runtime.Utilities;

namespace Wanko.Runtime.Controllers
{
    [DisallowMultipleComponent]
    public sealed class UIController : MonoBehaviour, IDragHandler, IScrollHandler, IWindowClickthroughHandler
    {
        private RectTransform _rectTransform;
        private DummyTransform _target;

        [field: SerializeField]
        public Drag Drag { get; private set; }
        [field: SerializeField]
        public Scale Scale { get; private set; }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            _target.Position += (Vector3)eventData.delta;
        }

        void IScrollHandler.OnScroll(PointerEventData eventData)
        {
            _target.Scale += eventData.scrollDelta.y / 6f * Scale.Factor * Vector3.one;
            _target.Scale = Vector3.Min(_target.Scale, Scale.Max * Vector3.one);
            _target.Scale = Vector3.Max(_target.Scale, Scale.Min * Vector3.one);
        }

        bool IWindowClickthroughHandler.SetClickthrough(Vector3 position) =>
            !EventSystem.current.IsPointerOverGameObject();

        private void Reset()
        {
            Drag = new() { Speed = 10f };
            Scale = new() { Min = .5f, Max = 4f, Factor = .1f, Speed = 10f };
        }

        private void Awake() =>
            _rectTransform = transform as RectTransform;

        private void Start() =>
            _target = (DummyTransform)_rectTransform;

        private void Update()
        {
            _rectTransform.position = Vector3.Lerp(_rectTransform.position, _target.Position, Time.deltaTime * Drag.Speed);
            _rectTransform.localScale = Vector3.Lerp(_rectTransform.localScale, _target.Scale, Time.deltaTime * Scale.Speed);
        }
    }
}