using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Raycasting;
using UnityEngine;

namespace Wanko
{
    // TODO: Rename
    [RequireComponent(typeof(CubismRaycaster))]
    public class CubismRaycasterDraggable : MonoBehaviour // IPointer/IDrag
    {
        private CubismRaycaster _raycaster;

        // TODO: Rename
        private bool _isDragging;
        private Vector3 _offset;

        private void Start() =>
            _raycaster = GetComponent<CubismRaycaster>();

        // TODO: OnMouseOverCubismRaycaster()
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hit = new CubismRaycastHit[1];

                if (_raycaster.Raycast(ray, hit) > 0)
                {
                    _isDragging = true;

                    CubismDrawable drawable = hit[0].Drawable;
                    _offset = drawable.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else if (Input.GetMouseButtonUp(0))
                _isDragging = false;

            if (_isDragging)
                transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offset;
        }
    }
}