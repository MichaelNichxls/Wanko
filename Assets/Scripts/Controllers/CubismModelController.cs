using Live2D.Cubism.Framework.Raycasting;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Wanko.InputActions;
#if !UNITY_EDITOR
using Wanko.Native;
using static Wanko.Native.User32.SetWindowLongFlags;
using static Wanko.Native.User32.WindowLongIndexFlags;
#endif

namespace Wanko.Controllers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CubismRaycaster))]
    public sealed class CubismModelController : MonoBehaviour, CubismModelInputActions.ICubismModelActions
    {
        private CubismRaycaster _raycaster;
        private CubismRaycastHit[] _raycastHits;
        private CubismModelInputActions _inputActions;
        private Vector3 _position, _offset, _scale;
#if !UNITY_EDITOR
        private User32.SetWindowLongFlags _dwNewLong = WS_EX_LAYERED | WS_EX_TRANSPARENT;
#endif
        [field: SerializeField]
        public float DragSpeed { get; private set; } = 10f;
        [field: SerializeField]
        public float ScaleMin { get; private set; } = 1f;
        [field: SerializeField]
        public float ScaleMax { get; private set; } = 20f;
        [field: SerializeField]
        public float ScaleFactor { get; private set; } = .5f;
        [field: SerializeField]
        public float ScaleSpeed { get; private set; } = 10f;

        void CubismModelInputActions.ICubismModelActions.OnPosition(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed)
                return;

            Array.Clear(_raycastHits, 0, _raycastHits.Length);
            _raycaster.Raycast(Camera.main.ScreenPointToRay(context.ReadValue<Vector2>()), _raycastHits);
#if !UNITY_EDITOR
            _dwNewLong = _raycastHits.Any(x => !x.Equals(default(CubismRaycastHit)))
                ? WS_EX_LAYERED
                : WS_EX_LAYERED | WS_EX_TRANSPARENT;
#endif
        }

        void CubismModelInputActions.ICubismModelActions.OnClick(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed || !_raycastHits.Any(x => !x.Equals(default(CubismRaycastHit))))
                return;

            _position = Camera.main.ScreenToWorldPoint(_inputActions.CubismModel.Position.ReadValue<Vector2>());
            _offset = transform.position - _position;

            StartCoroutine(DragCoroutine(context));
        }

        void CubismModelInputActions.ICubismModelActions.OnScroll(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Performed || !_raycastHits.Any(x => !x.Equals(default(CubismRaycastHit))))
                return;

            _scale += context.ReadValue<float>() / 120f * ScaleFactor * Vector3.one;
            _scale = Vector3.Min(_scale, ScaleMax * Vector3.one);
            _scale = Vector3.Max(_scale, ScaleMin * Vector3.one);
        }

        private IEnumerator DragCoroutine(InputAction.CallbackContext context)
        {
            while (context.ReadValueAsButton())
            {
                _position = Camera.main.ScreenToWorldPoint(_inputActions.CubismModel.Position.ReadValue<Vector2>());
                yield return null;
            }
        }

        private void Awake()
        {
            _raycaster = GetComponent<CubismRaycaster>();
            _raycastHits = new CubismRaycastHit[1];
            _inputActions = new CubismModelInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.CubismModel.AddCallbacks(this);
        }

        private void OnDisable()    
        {
            _inputActions.Disable();
            _inputActions.CubismModel.RemoveCallbacks(this);
        }

        private void Start()
        {
            _position = transform.position;
            _scale = transform.localScale;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _position + _offset, Time.deltaTime * DragSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, _scale, Time.deltaTime * ScaleSpeed);
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