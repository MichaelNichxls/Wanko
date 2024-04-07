using Live2D.Cubism.Framework.Raycasting;
using System.Linq;
using UnityEngine;

namespace Wanko.EventSystems
{
    // move inside class?
    public enum PointerState
    {
        None,
        Idle,
        Move,
        Enter,
        Exit,
        Down,
        Up,
        Click
    }

    public enum DragState
    {
        None,
        Idle,
        Drag,
        BeginDrag,
        EndDrag
    }

    public enum ScrollState
    {
        None,
        Idle,
        Scroll
    }

    [DisallowMultipleComponent]
    public sealed class CubismEventSystem : MonoBehaviour // : CubismModelBehaviour?
    {
        private ICubismEventSystemHandler[] _handlers;
        private CubismRaycaster _raycaster;
        private CubismRaycastHit[] _raycastHits; // make apart of eventData, make number of hits configurable

        private Ray _lastRay;
        private bool _isMouseDown; // do this better

        private PointerState _pointerState = PointerState.None;
        private DragState _dragState = DragState.None;
        private ScrollState _scrollState = ScrollState.None;

        //// TODO: Implement
        //[field: SerializeField]
        //public int DragThreshold { get; private set; } = 10;

        private void Awake()
        {
            var cubismModelGameObject = GameObject.FindGameObjectWithTag("Cubism Model");
            
            _handlers = cubismModelGameObject.GetComponents<ICubismEventSystemHandler>();
            _raycaster = cubismModelGameObject.GetComponent<CubismRaycaster>();
            _raycastHits = new CubismRaycastHit[1];
        }

        // TODO: Optimize and log similarly to EventSystem
        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // move to eventData
            bool isHit              = _raycaster.Raycast(ray, _raycastHits) > 0;
            bool isOrigin           = ray.origin == _lastRay.origin;
            bool isMouseDownLeft    = Input.GetMouseButtonDown(0);
            bool isMouseDownRight   = Input.GetMouseButtonDown(1);
            bool isMouseDownMiddle  = Input.GetMouseButtonDown(2);
            bool isMouseDown        = isMouseDownLeft || isMouseDownRight || isMouseDownMiddle;
            bool isMouseUp          = Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2);
            bool isScroll           = Input.GetAxis("Mouse ScrollWheel") != 0f;

            // HACK
            CubismPointerEventData eventData = new(this)
            {
                Delta = (ray.origin - _lastRay.origin), // probably some variable for this; BUG
                ScrollDelta = Input.mouseScrollDelta,   // use this for isScroll
                //Button = isMouseDownLeft
                //    ? CubismPointerEventData.InputButton.Left
                //    : isMouseDownRight
                //        ? CubismPointerEventData.InputButton.Right
                //        : isMouseDownMiddle
                //            ? CubismPointerEventData.InputButton.Middle
                //            : (CubismPointerEventData.InputButton)(-1)
            };

            SetPointerState();
            SetDragState();
            SetScrollState();

            _lastRay = ray;
            _isMouseDown = _pointerState switch
            {
                PointerState.Down   => true,
                PointerState.Up     => false,
                _                   => _isMouseDown
            };

            void SetPointerState()
            {
                _pointerState = _pointerState switch
                {
                    PointerState.Exit or PointerState.None when isHit                           => PointerState.Enter,
                    PointerState.Enter or PointerState.Idle or PointerState.Move when !isHit    => PointerState.Exit,
                    _ when isHit && isMouseDown                                                 => PointerState.Down,
                    _ when _isMouseDown && isMouseUp                                            => PointerState.Up,
                    PointerState.Up when isHit                                                  => PointerState.Click,
                    not PointerState.Up when isHit && isOrigin                                  => PointerState.Idle,
                    not PointerState.Up when isHit && !isOrigin                                 => PointerState.Move,
                    _                                                                           => PointerState.None
                };

                switch (_pointerState)
                {
                    case PointerState.Move:
                        foreach (var handler in _handlers.OfType<ICubismPointerMoveHandler>())
                            handler.OnPointerMove(eventData);
                        break;

                    case PointerState.Enter:
                        foreach (var handler in _handlers.OfType<ICubismPointerEnterHandler>())
                            handler.OnPointerEnter(eventData);
                        break;

                    case PointerState.Exit:
                        foreach (var handler in _handlers.OfType<ICubismPointerExitHandler>())
                            handler.OnPointerExit(eventData);
                        break;

                    case PointerState.Down:
                        foreach (var handler in _handlers.OfType<ICubismPointerDownHandler>())
                            handler.OnPointerDown(eventData);
                        break;

                    case PointerState.Up:
                        foreach (var handler in _handlers.OfType<ICubismPointerUpHandler>())
                            handler.OnPointerUp(eventData);
                        break;

                    case PointerState.Click:
                        foreach (var handler in _handlers.OfType<ICubismPointerClickHandler>())
                            handler.OnPointerClick(eventData);
                        break;

                    default:
                        break;
                }
            }

            void SetDragState()
            {
                _dragState = _dragState switch
                {
                    DragState.EndDrag or DragState.None when _isMouseDown && !isOrigin                      => DragState.BeginDrag,
                    DragState.BeginDrag or DragState.Idle or DragState.Drag when _isMouseDown && isMouseUp  => DragState.EndDrag,
                    DragState.BeginDrag or DragState.Idle or DragState.Drag when _isMouseDown && isOrigin   => DragState.Idle,
                    DragState.BeginDrag or DragState.Idle or DragState.Drag when _isMouseDown && !isOrigin  => DragState.Drag,
                    _                                                                                       => DragState.None
                };

                switch (_dragState)
                {
                    case DragState.Drag:
                        foreach (var handler in _handlers.OfType<ICubismDragHandler>())
                            handler.OnDrag(eventData);
                        break;

                    case DragState.BeginDrag:
                        foreach (var handler in _handlers.OfType<ICubismBeginDragHandler>())
                            handler.OnBeginDrag(eventData);
                        break;

                    case DragState.EndDrag:
                        foreach (var handler in _handlers.OfType<ICubismEndDragHandler>())
                            handler.OnEndDrag(eventData);
                        break;

                    default:
                        break;
                }
            }

            void SetScrollState()
            {
                _scrollState = _scrollState switch
                {
                    _ when isHit && isScroll    => ScrollState.Scroll,
                    _ when isHit && !isScroll   => ScrollState.Idle,
                    _                           => ScrollState.None
                };

                switch (_scrollState)
                {
                    case ScrollState.Scroll:
                        foreach (var handler in _handlers.OfType<ICubismScrollHandler>())
                            handler.OnScroll(eventData);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}