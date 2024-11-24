using Live2D.Cubism.Framework.Raycasting;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Wanko.Runtime.Utilities.Serializable;
using Wanko.Runtime.Window;
using static Wanko.InputActions;

namespace Wanko.Runtime.Cubism
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CubismRaycaster))]
    public sealed class CubismModelController : MonoBehaviour, ICubismModelActions, IWindowTransparentHandler
    {
        private CubismRaycaster _raycaster;
        private CubismModelActions _actions;
        private DummyTransform _target;

        public CubismRaycastHit[] RaycastHit { get; } = new CubismRaycastHit[1];
        public bool HasRaycastHit => _raycaster.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), RaycastHit) > 0;

        [field: SerializeField]
        public MoveOptions Move { get; private set; } = new() { Speed = 10f };
        [field: SerializeField]
        public ScaleOptions Scale { get; private set; } = new() { Min = 1f, Max = 20f, Factor = .5f, Speed = 10f };

        void ICubismModelActions.OnMove(InputAction.CallbackContext context)
        {
            if (!context.performed || !HasRaycastHit)
                return;

            _ = StartCoroutine(OnMoveCoroutine());

            IEnumerator OnMoveCoroutine()
            {
                _target.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                Vector3 offset = transform.position - _target.position;

                while (context.ReadValueAsButton())
                {
                    _target.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + offset;
                    yield return null;
                }
            }
        }

        void ICubismModelActions.OnScale(InputAction.CallbackContext context)
        {
            if (!context.performed || !HasRaycastHit)
                return;
            
            _target.localScale += context.ReadValue<Vector2>().y / 120f * Scale.Factor * Vector3.one;
            _target.localScale = Vector3.Min(_target.localScale, Scale.Max * Vector3.one);
            _target.localScale = Vector3.Max(_target.localScale, Scale.Min * Vector3.one);
        }

        bool IWindowTransparentHandler.SetTransparent(Vector2 position) =>
            !HasRaycastHit;

        private void Awake()
        {
            _raycaster = GetComponent<CubismRaycaster>();
            _actions = new InputActions().CubismModel;
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
            transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * Move.Speed);
            transform.localScale = Vector3.Lerp(transform.localScale, _target.localScale, Time.deltaTime * Scale.Speed);
        }
    }
}