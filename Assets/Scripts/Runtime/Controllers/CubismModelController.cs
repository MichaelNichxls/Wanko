using Live2D.Cubism.Framework.Raycasting;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Wanko.Runtime.Managers;
using Wanko.Runtime.Utilities;

namespace Wanko.Runtime.Controllers
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CubismRaycaster))]
    public sealed class CubismModelController : MonoBehaviour, ApplicationInputActions.ICubismModelActions, IWindowClickthroughHandler
    {
        private CubismRaycaster _raycaster;
        private ApplicationInputActions.CubismModelActions _actions;
        private DummyTransform _target;

        public CubismRaycastHit[] RaycastHit { get; private set; }
        public bool HasRaycastHit { get; private set; }

        [field: SerializeField]
        public Drag Drag { get; private set; }
        [field: SerializeField]
        public Scale Scale { get; private set; }

        void ApplicationInputActions.ICubismModelActions.OnPosition(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            HasRaycastHit = _raycaster.Raycast(Camera.main.ScreenPointToRay(context.ReadValue<Vector2>()), RaycastHit) > 0;
        }

        void ApplicationInputActions.ICubismModelActions.OnClick(InputAction.CallbackContext context)
        {
            if (!context.performed || !HasRaycastHit)
                return;

            StartCoroutine(OnDrag(context));
        }

        void ApplicationInputActions.ICubismModelActions.OnScroll(InputAction.CallbackContext context)
        {
            if (!context.performed || !HasRaycastHit)
                return;

            _target.Scale += context.ReadValue<Vector2>().y / 120f * Scale.Factor * Vector3.one;
            _target.Scale = Vector3.Min(_target.Scale, Scale.Max * Vector3.one);
            _target.Scale = Vector3.Max(_target.Scale, Scale.Min * Vector3.one);
        }

        bool IWindowClickthroughHandler.SetClickthrough(Vector3 position) =>
            !HasRaycastHit;

        private IEnumerator OnDrag(InputAction.CallbackContext context)
        {
            _target.Position = Camera.main.ScreenToWorldPoint(_actions.Position.ReadValue<Vector2>());
            Vector3 offset = transform.position - _target.Position;

            while (context.ReadValueAsButton())
            {
                _target.Position = Camera.main.ScreenToWorldPoint(_actions.Position.ReadValue<Vector2>()) + offset;
                yield return null;
            }
        }

        private void Reset()
        {
            Drag = new() { Speed = 10f };
            Scale = new() { Min = 1f, Max = 20f, Factor = .5f, Speed = 10f };
        }

        private void Awake()
        {
            _raycaster = GetComponent<CubismRaycaster>();
            _actions = new ApplicationInputActions().CubismModel;

            RaycastHit = new CubismRaycastHit[1];
        }

        private void OnEnable()
        {
            _actions.Enable();
            _actions.AddCallbacks(this);
        }

        private void OnDisable()    
        {
            _actions.Disable();
            _actions.RemoveCallbacks(this);
        }

        private void Start() =>
            _target = (DummyTransform)transform;

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _target.Position, Time.deltaTime * Drag.Speed);
            transform.localScale = Vector3.Lerp(transform.localScale, _target.Scale, Time.deltaTime * Scale.Speed);
        }
    }
}