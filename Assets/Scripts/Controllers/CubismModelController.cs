using Live2D.Cubism.Framework.Raycasting;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

#if !UNITY_EDITOR
using Wanko.Native;
using static Wanko.Native.NativeConstants;
#endif

// Utilize CubismModelInputActions.CubismModelActions more
namespace Wanko.Controllers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CubismRaycaster))]
    public sealed class CubismModelController : MonoBehaviour //, CubismModelInputActions.ICubismModelActions
    {
        private CubismRaycaster _raycaster;
        private CubismRaycastHit[] _raycastHits;
        private CubismModelInputActions _inputActions;
        // Rename
        private Vector3 _position, _offset;
        private Vector3 _scale;
#if !UNITY_EDITOR
        private uint _dwNewLong = WS_EX_LAYERED | WS_EX_TRANSPARENT;
#endif
        [field: Header("Drag")]
        [field: SerializeField]
        public float DragSpeed { get; private set; } = 10f;
        [field: Header("Scale")]
        [field: SerializeField]
        public float ScaleMin { get; private set; } = 1f;
        [field: SerializeField]
        public float ScaleMax { get; private set; } = 20f;
        [field: SerializeField]
        public float ScaleFactor { get; private set; } = .5f;
        [field: SerializeField]
        public float ScaleSpeed { get; private set; } = 10f;

        private void Awake()
        {
            _raycaster = GetComponent<CubismRaycaster>();
            _raycastHits = new CubismRaycastHit[1];
            _inputActions = new CubismModelInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.CubismModel.Drag.performed += Drag_Started;
            _inputActions.CubismModel.Drag.performed += Drag_Performed;
            _inputActions.CubismModel.DragPosition.performed += DragPosition_Performed;
            _inputActions.CubismModel.Scale.performed += Scale_Performed;
        }

        private void OnDisable()    
        {
            _inputActions.Disable();

            _inputActions.CubismModel.Drag.performed -= Drag_Started;
            _inputActions.CubismModel.Drag.performed -= Drag_Performed;
            _inputActions.CubismModel.DragPosition.performed -= DragPosition_Performed;
            _inputActions.CubismModel.Scale.performed -= Scale_Performed;
        }

        private void Start()
        {
            _offset = transform.position;
            _scale = transform.localScale;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _position + _offset, Time.deltaTime * DragSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, _scale, Time.deltaTime * ScaleSpeed);
        }

        private void Drag_Started(InputAction.CallbackContext context)
        {
            if (!_raycastHits.Any(x => !x.Equals(default(CubismRaycastHit))))
                context.action.Reset();
        }

        private void Drag_Performed(InputAction.CallbackContext context)
        {
            _position = Camera.main.ScreenToWorldPoint(_inputActions.CubismModel.DragPosition.ReadValue<Vector2>());
            _offset = transform.position - _position;
        }

        // Rename action
        private void DragPosition_Performed(InputAction.CallbackContext context)
        {
            Array.Clear(_raycastHits, 0, _raycastHits.Length);
            _raycaster.Raycast(Camera.main.ScreenPointToRay(context.ReadValue<Vector2>()), _raycastHits);
#if !UNITY_EDITOR
            _dwNewLong = _raycastHits.Any(x => !x.Equals(default(CubismRaycastHit)))
                ? WS_EX_LAYERED
                : WS_EX_LAYERED | WS_EX_TRANSPARENT;
#endif
            if (_inputActions.CubismModel.Drag.ReadValue<float>() != 1f)
                return;
            
            _position = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }

        private void Scale_Performed(InputAction.CallbackContext context)
        {
            if (!_raycastHits.Any(x => !x.Equals(default(CubismRaycastHit))))
                return;

            _scale += context.ReadValue<float>() / 120f * ScaleFactor * Vector3.one;
            _scale = Vector3.Min(_scale, ScaleMax * Vector3.one);
            _scale = Vector3.Max(_scale, ScaleMin * Vector3.one);
        }

        public void SetWindowLong(IntPtr hWnd)
        {
#if !UNITY_EDITOR
            if ((_dwNewLong & WS_EX_TRANSPARENT) != 0)
                return;

            NativeMethods.SetWindowLong(hWnd, GWL_EXSTYLE, _dwNewLong);
#endif
        }
    }
}