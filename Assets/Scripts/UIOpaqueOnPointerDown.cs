using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Wanko
{
    [RequireComponent(typeof(Image))]
    public class UIOpaqueOnPointerDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
        }

        public void OnPointerDown(PointerEventData eventData) =>
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1);

        public void OnPointerUp(PointerEventData eventData) =>
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
    }
}