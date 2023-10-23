using UnityEngine;
using UnityEngine.EventSystems;

namespace Wanko
{
    [RequireComponent(typeof(RectTransform))]
    public class UIDraggable : MonoBehaviour, IDragHandler
    {
        private RectTransform _rectTransform;
        [SerializeField]
        private Canvas _canvas;

        private void Start() =>
            _rectTransform = GetComponent<RectTransform>();

        public void OnDrag(PointerEventData eventData) =>
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }
}