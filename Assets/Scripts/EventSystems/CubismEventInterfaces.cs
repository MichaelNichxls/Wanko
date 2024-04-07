namespace Wanko.EventSystems
{
    public interface ICubismEventSystemHandler
    {
    }

    public interface ICubismPointerMoveHandler : ICubismEventSystemHandler
    {
        void OnPointerMove(CubismPointerEventData eventData);
    }

    public interface ICubismPointerEnterHandler : ICubismEventSystemHandler
    {
        void OnPointerEnter(CubismPointerEventData eventData);
    }

    public interface ICubismPointerExitHandler : ICubismEventSystemHandler
    {
        void OnPointerExit(CubismPointerEventData eventData);
    }

    public interface ICubismPointerDownHandler : ICubismEventSystemHandler
    {
        void OnPointerDown(CubismPointerEventData eventData);
    }

    public interface ICubismPointerUpHandler : ICubismEventSystemHandler
    {
        void OnPointerUp(CubismPointerEventData eventData);
    }

    public interface ICubismPointerClickHandler : ICubismEventSystemHandler
    {
        void OnPointerClick(CubismPointerEventData eventData);
    }

    public interface ICubismBeginDragHandler : ICubismEventSystemHandler
    {
        void OnBeginDrag(CubismPointerEventData eventData);
    }

    public interface ICubismDragHandler : ICubismEventSystemHandler
    {
        void OnDrag(CubismPointerEventData eventData);
    }

    public interface ICubismEndDragHandler : ICubismEventSystemHandler
    {
        void OnEndDrag(CubismPointerEventData eventData);
    }

    public interface ICubismScrollHandler : ICubismEventSystemHandler
    {
        void OnScroll(CubismPointerEventData eventData);
    }
}