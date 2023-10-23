using Live2D.Cubism.Framework.Raycasting;
using UnityEngine;

namespace Wanko
{
    // TODO: Rename
    [RequireComponent(typeof(CubismRaycaster))]
    public class CubismRaycasterScalable : MonoBehaviour // IPointerEnterHandler
    {
        private CubismRaycaster _raycaster;

        [field: SerializeField]
        public float ScaleMin { get; private set; } = 0.1f;
        [field: SerializeField]
        public float ScaleMax { get; private set; } = 2f;
        [field: SerializeField]
        public float ScaleSpeed { get; private set; } = 1f;

        private void Start() =>
            _raycaster = GetComponent<CubismRaycaster>();

        // TODO: Optimize and refactor; make helpers
        // TODO: OnMouseOverCubismRaycaster()
        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = new CubismRaycastHit[1];

            if (_raycaster.Raycast(ray, hit) <= 0)
                return;

            transform.localScale += Input.GetAxis("Mouse ScrollWheel") * ScaleSpeed * Vector3.one;
            transform.localScale = new Vector3(
                Mathf.Clamp(transform.localScale.x, ScaleMin, ScaleMax),
                Mathf.Clamp(transform.localScale.y, ScaleMin, ScaleMax),
                Mathf.Clamp(transform.localScale.z, ScaleMin, ScaleMax));
        }
    }
}