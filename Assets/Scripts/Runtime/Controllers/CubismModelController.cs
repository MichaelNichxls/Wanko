using Live2D.Cubism.Framework.Raycasting;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Wanko.Runtime.InputActions;
using Wanko.Runtime.Managers;

namespace Wanko.Runtime.Controllers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CubismRaycaster))]
    public sealed class CubismModelController : MonoBehaviour, CubismModelInputActions.ICubismModelActions, IWindowClickthroughHandler
    {
        private CubismRaycaster _raycaster;
        private CubismModelInputActions _inputActions;
        private Vector3 _position, _offset, _scale;

        public CubismRaycastHit[] RaycastHit { get; private set; } = new CubismRaycastHit[1];
        public bool HasRaycastHit { get; private set; }

        // indent? // IDraggable & IScalable
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
            if (!context.performed)
                return;

            HasRaycastHit = _raycaster.Raycast(Camera.main.ScreenPointToRay(context.ReadValue<Vector2>()), RaycastHit) > 0;
        }

        void CubismModelInputActions.ICubismModelActions.OnClick(InputAction.CallbackContext context)
        {
            if (!context.performed || !HasRaycastHit)
                return;

            _position = Camera.main.ScreenToWorldPoint(_inputActions.CubismModel.Position.ReadValue<Vector2>());
            _offset = transform.position - _position;

            StartCoroutine(OnDrag(context));
        }

        void CubismModelInputActions.ICubismModelActions.OnScroll(InputAction.CallbackContext context)
        {
            if (!context.performed || !HasRaycastHit)
                return;

            _scale += context.ReadValue<Vector2>().y / 120f * ScaleFactor * Vector3.one;
            _scale = Vector3.Min(_scale, ScaleMax * Vector3.one);
            _scale = Vector3.Max(_scale, ScaleMin * Vector3.one);
        }

        bool IWindowClickthroughHandler.SetClickthrough(Vector3 position) =>
            !HasRaycastHit;

        private IEnumerator OnDrag(InputAction.CallbackContext context)
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
    }
}