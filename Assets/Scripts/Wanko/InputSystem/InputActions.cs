//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Scripts/Wanko/InputSystem/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Wanko.InputSystem
{
    public partial class @InputActions: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""32745723-a3e0-4886-a1d4-bef1fe2ceee1"",
            ""actions"": [
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""02544ccd-763f-4815-8c69-edae14f0243d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4da49188-e314-42f1-a984-31b8faa33e40"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""eab2c8b5-eea0-4618-bf87-9cef46f0901f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a9cac462-21be-41f2-9e65-e9fd324b9e3b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e8f95a3c-db94-49af-9bf5-fe864cbf87cb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""245bf4dc-60d2-4c8e-b18b-b5da6e476064"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""070800a3-7c1b-4563-96b7-672c6e1cf78f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""7dd6c01e-9d5f-455c-95ab-27a8647d9d55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""90ad7fd8-c32d-4c71-b37e-db76ee4d1ccb"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b097ff61-67fb-4b4b-9383-350be7893421"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a1956baa-d3f2-4ec6-9bab-60be68e0f5a3"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""373b1777-7bf8-4012-9cd7-4070c8ea2b6d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2373b813-2d39-493d-b43e-c3bb1049843a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3173f2d3-8082-4c27-84ce-1f9a400cae66"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""076f8a4a-18bb-4e51-b180-551ddd24dd4e"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c987f50c-33ff-4fbb-b2b3-840a2a3c3ce0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aac48535-bc6a-40a7-8d6a-da5086753c78"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2574923d-b03c-47d7-aacc-62b56018bbdd"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Gamepad;Touch;Joystick;XR"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba31d8f9-9bb3-4d82-aee5-9ef01f14f959"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Gamepad;Touch;Joystick;XR"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55148cd5-ce34-4d2a-8aed-dec7d20f0ac1"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13c6b7e2-3ff6-466c-904a-3e645e68f4e4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbced928-5a43-40ed-95fc-930b38392034"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""771b7f98-6268-428f-9aaa-be8bbe2eaffe"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb1416ef-3133-4229-80c2-5b9d48f39bf6"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CubismModel"",
            ""id"": ""41fb999f-be0d-4898-bd83-a672996b351e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""49598694-48f5-4ac8-b44e-b132da5a909a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Scale"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6e6750eb-8ad3-43d0-9c27-02ebd4a07a17"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3872aaf4-7a64-4a22-8575-ba87a1b58cde"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d28af76a-bae6-4831-bfc7-01cc9738b342"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scale"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
            m_UI_LeftClick = m_UI.FindAction("LeftClick", throwIfNotFound: true);
            m_UI_MiddleClick = m_UI.FindAction("MiddleClick", throwIfNotFound: true);
            m_UI_RightClick = m_UI.FindAction("RightClick", throwIfNotFound: true);
            m_UI_ScrollWheel = m_UI.FindAction("ScrollWheel", throwIfNotFound: true);
            m_UI_Move = m_UI.FindAction("Move", throwIfNotFound: true);
            m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
            m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
            // CubismModel
            m_CubismModel = asset.FindActionMap("CubismModel", throwIfNotFound: true);
            m_CubismModel_Move = m_CubismModel.FindAction("Move", throwIfNotFound: true);
            m_CubismModel_Scale = m_CubismModel.FindAction("Scale", throwIfNotFound: true);
        }

        ~@InputActions()
        {
            UnityEngine.Debug.Assert(!m_UI.enabled, "This will cause a leak and performance issues, InputActions.UI.Disable() has not been called.");
            UnityEngine.Debug.Assert(!m_CubismModel.enabled, "This will cause a leak and performance issues, InputActions.CubismModel.Disable() has not been called.");
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // UI
        private readonly InputActionMap m_UI;
        private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
        private readonly InputAction m_UI_Point;
        private readonly InputAction m_UI_LeftClick;
        private readonly InputAction m_UI_MiddleClick;
        private readonly InputAction m_UI_RightClick;
        private readonly InputAction m_UI_ScrollWheel;
        private readonly InputAction m_UI_Move;
        private readonly InputAction m_UI_Submit;
        private readonly InputAction m_UI_Cancel;
        public struct UIActions
        {
            private @InputActions m_Wrapper;
            public UIActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Point => m_Wrapper.m_UI_Point;
            public InputAction @LeftClick => m_Wrapper.m_UI_LeftClick;
            public InputAction @MiddleClick => m_Wrapper.m_UI_MiddleClick;
            public InputAction @RightClick => m_Wrapper.m_UI_RightClick;
            public InputAction @ScrollWheel => m_Wrapper.m_UI_ScrollWheel;
            public InputAction @Move => m_Wrapper.m_UI_Move;
            public InputAction @Submit => m_Wrapper.m_UI_Submit;
            public InputAction @Cancel => m_Wrapper.m_UI_Cancel;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void AddCallbacks(IUIActions instance)
            {
                if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @MiddleClick.started += instance.OnMiddleClick;
                @MiddleClick.performed += instance.OnMiddleClick;
                @MiddleClick.canceled += instance.OnMiddleClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }

            private void UnregisterCallbacks(IUIActions instance)
            {
                @Point.started -= instance.OnPoint;
                @Point.performed -= instance.OnPoint;
                @Point.canceled -= instance.OnPoint;
                @LeftClick.started -= instance.OnLeftClick;
                @LeftClick.performed -= instance.OnLeftClick;
                @LeftClick.canceled -= instance.OnLeftClick;
                @MiddleClick.started -= instance.OnMiddleClick;
                @MiddleClick.performed -= instance.OnMiddleClick;
                @MiddleClick.canceled -= instance.OnMiddleClick;
                @RightClick.started -= instance.OnRightClick;
                @RightClick.performed -= instance.OnRightClick;
                @RightClick.canceled -= instance.OnRightClick;
                @ScrollWheel.started -= instance.OnScrollWheel;
                @ScrollWheel.performed -= instance.OnScrollWheel;
                @ScrollWheel.canceled -= instance.OnScrollWheel;
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
                @Submit.started -= instance.OnSubmit;
                @Submit.performed -= instance.OnSubmit;
                @Submit.canceled -= instance.OnSubmit;
                @Cancel.started -= instance.OnCancel;
                @Cancel.performed -= instance.OnCancel;
                @Cancel.canceled -= instance.OnCancel;
            }

            public void RemoveCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IUIActions instance)
            {
                foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public UIActions @UI => new UIActions(this);

        // CubismModel
        private readonly InputActionMap m_CubismModel;
        private List<ICubismModelActions> m_CubismModelActionsCallbackInterfaces = new List<ICubismModelActions>();
        private readonly InputAction m_CubismModel_Move;
        private readonly InputAction m_CubismModel_Scale;
        public struct CubismModelActions
        {
            private @InputActions m_Wrapper;
            public CubismModelActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_CubismModel_Move;
            public InputAction @Scale => m_Wrapper.m_CubismModel_Scale;
            public InputActionMap Get() { return m_Wrapper.m_CubismModel; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CubismModelActions set) { return set.Get(); }
            public void AddCallbacks(ICubismModelActions instance)
            {
                if (instance == null || m_Wrapper.m_CubismModelActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_CubismModelActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Scale.started += instance.OnScale;
                @Scale.performed += instance.OnScale;
                @Scale.canceled += instance.OnScale;
            }

            private void UnregisterCallbacks(ICubismModelActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
                @Scale.started -= instance.OnScale;
                @Scale.performed -= instance.OnScale;
                @Scale.canceled -= instance.OnScale;
            }

            public void RemoveCallbacks(ICubismModelActions instance)
            {
                if (m_Wrapper.m_CubismModelActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(ICubismModelActions instance)
            {
                foreach (var item in m_Wrapper.m_CubismModelActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_CubismModelActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public CubismModelActions @CubismModel => new CubismModelActions(this);
        public interface IUIActions
        {
            void OnPoint(InputAction.CallbackContext context);
            void OnLeftClick(InputAction.CallbackContext context);
            void OnMiddleClick(InputAction.CallbackContext context);
            void OnRightClick(InputAction.CallbackContext context);
            void OnScrollWheel(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
            void OnSubmit(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
        }
        public interface ICubismModelActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnScale(InputAction.CallbackContext context);
        }
    }
}