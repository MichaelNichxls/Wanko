using UnityEngine;

namespace Wanko.EventSystems
{
    // TODO: Fully implement
    public sealed class CubismPointerEventData // : BaseEventData
    {
        public enum InputButton
        {
            Left = 0,
            Right = 1,
            Middle = 2
        }

        private readonly CubismEventSystem _eventSystem;

        public Vector3 Delta { get; set; }
        public Vector2 ScrollDelta { get; set; }
        public InputButton Button { get; set; }

        public CubismPointerEventData(CubismEventSystem eventSystem)
            // : base(eventSystem)
        {
            _eventSystem = eventSystem;
        }
    }
}