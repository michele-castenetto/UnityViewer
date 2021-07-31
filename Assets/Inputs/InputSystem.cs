// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/InputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""6ce0b579-0cd1-4a88-8d5d-af2357357117"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""de2bbc36-0469-477b-8251-b2b501c1067c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseWheel"",
                    ""type"": ""Value"",
                    ""id"": ""d540acb4-9e7c-4c7e-b053-cb0a64e45dbf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftMouseButton"",
                    ""type"": ""Button"",
                    ""id"": ""aeefc288-68e7-4f4b-a410-ec0104c9bb49"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ef0d805d-56a9-415c-8d4c-2b2a8b7aa72a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TouchPress"",
                    ""type"": ""PassThrough"",
                    ""id"": ""18cb2ab5-8dfb-444b-9434-47ef885dbeba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f7ea09f8-19e1-4290-8c07-5be65bd6d27a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d70303ef-c0f0-46c7-860a-4525466b2dfb"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""282a5816-6296-4944-a10c-3095f2933b4f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftMouseButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e9ff3906-d5a1-4e3f-8246-b3baca3d3f70"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e22df21a-edab-447c-ad65-ca803662483c"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Game"",
            ""id"": ""d7f7f409-f90a-46d9-b325-8e8b1569f1fc"",
            ""actions"": [
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""cd1b3da5-88c4-4fe5-b016-ec92768dc182"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""112f8cd1-9221-434c-bc1e-9af3b7b3f56a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_MousePosition = m_Camera.FindAction("MousePosition", throwIfNotFound: true);
        m_Camera_MouseWheel = m_Camera.FindAction("MouseWheel", throwIfNotFound: true);
        m_Camera_LeftMouseButton = m_Camera.FindAction("LeftMouseButton", throwIfNotFound: true);
        m_Camera_TouchPosition = m_Camera.FindAction("TouchPosition", throwIfNotFound: true);
        m_Camera_TouchPress = m_Camera.FindAction("TouchPress", throwIfNotFound: true);
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Esc = m_Game.FindAction("Esc", throwIfNotFound: true);
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

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_MousePosition;
    private readonly InputAction m_Camera_MouseWheel;
    private readonly InputAction m_Camera_LeftMouseButton;
    private readonly InputAction m_Camera_TouchPosition;
    private readonly InputAction m_Camera_TouchPress;
    public struct CameraActions
    {
        private @InputSystem m_Wrapper;
        public CameraActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_Camera_MousePosition;
        public InputAction @MouseWheel => m_Wrapper.m_Camera_MouseWheel;
        public InputAction @LeftMouseButton => m_Wrapper.m_Camera_LeftMouseButton;
        public InputAction @TouchPosition => m_Wrapper.m_Camera_TouchPosition;
        public InputAction @TouchPress => m_Wrapper.m_Camera_TouchPress;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMousePosition;
                @MouseWheel.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseWheel;
                @MouseWheel.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseWheel;
                @MouseWheel.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMouseWheel;
                @LeftMouseButton.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnLeftMouseButton;
                @LeftMouseButton.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnLeftMouseButton;
                @LeftMouseButton.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnLeftMouseButton;
                @TouchPosition.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPosition;
                @TouchPosition.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPosition;
                @TouchPress.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPress;
                @TouchPress.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPress;
                @TouchPress.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnTouchPress;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseWheel.started += instance.OnMouseWheel;
                @MouseWheel.performed += instance.OnMouseWheel;
                @MouseWheel.canceled += instance.OnMouseWheel;
                @LeftMouseButton.started += instance.OnLeftMouseButton;
                @LeftMouseButton.performed += instance.OnLeftMouseButton;
                @LeftMouseButton.canceled += instance.OnLeftMouseButton;
                @TouchPosition.started += instance.OnTouchPosition;
                @TouchPosition.performed += instance.OnTouchPosition;
                @TouchPosition.canceled += instance.OnTouchPosition;
                @TouchPress.started += instance.OnTouchPress;
                @TouchPress.performed += instance.OnTouchPress;
                @TouchPress.canceled += instance.OnTouchPress;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_Esc;
    public struct GameActions
    {
        private @InputSystem m_Wrapper;
        public GameActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Esc => m_Wrapper.m_Game_Esc;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Esc.started -= m_Wrapper.m_GameActionsCallbackInterface.OnEsc;
                @Esc.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnEsc;
                @Esc.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnEsc;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Esc.started += instance.OnEsc;
                @Esc.performed += instance.OnEsc;
                @Esc.canceled += instance.OnEsc;
            }
        }
    }
    public GameActions @Game => new GameActions(this);
    public interface ICameraActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseWheel(InputAction.CallbackContext context);
        void OnLeftMouseButton(InputAction.CallbackContext context);
        void OnTouchPosition(InputAction.CallbackContext context);
        void OnTouchPress(InputAction.CallbackContext context);
    }
    public interface IGameActions
    {
        void OnEsc(InputAction.CallbackContext context);
    }
}
