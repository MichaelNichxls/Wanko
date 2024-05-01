using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Wanko.Runtime.UI
{
    [DisallowMultipleComponent]
    public sealed class GraphicController : Selectable
    {
        // FIXME: Graphics aren't exactly synced with dragging, as dragging is lerped
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            DoStateTransition(SelectionState.Normal, false);
        }
    }
}