using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[DefaultExecutionOrder(-1)]
//##OLD
// public class InputManager : MonoBehaviour
public class InputManager : Singleton<InputManager>
{
    
    InputSystem inputSystem;
    
    
    // Keyboard Events
    public delegate void ExitEvent();
    public event ExitEvent OnExit; 
    
    // Mouse Events

    public delegate void LeftMouseButtonDownEvent(Vector2 position, float time);
    public event LeftMouseButtonDownEvent OnLeftMouseButtonDown;

    public delegate void LeftMouseButtonUpEvent(Vector2 position, float time);
    public event LeftMouseButtonUpEvent OnLeftMouseButtonUp;

    public delegate void MouseMoveEvent(Vector2 position, float time);
    public event MouseMoveEvent OnMouseMove;

    public delegate void WheelStartEvent();
    public event WheelStartEvent OnWheelStart;

    public delegate void WheelEndEvent();
    public event WheelEndEvent OnWheelEnd;

    public delegate void WheelMoveEvent(float delta);
    public event WheelMoveEvent OnWheelMove;


    // Touch Events
    public delegate void TouchStartEvent(Vector2 position, float time);
    public event TouchStartEvent OnTouchStart; 

    public delegate void TouchEndEvent(Vector2 position, float time);
    public event TouchEndEvent OnTouchEnd; 
    public delegate void TouchMoveEvent(Vector2 position, float time);
    public event TouchMoveEvent OnTouchMove; 

    public delegate void ZoomStartEvent();
    public event ZoomStartEvent OnZoomStart;

    public delegate void ZoomEndEvent();
    public event ZoomEndEvent OnZoomEnd;

    public delegate void ZoomChangeEvent(float delta);
    public event ZoomChangeEvent OnZoomChange;
    

    
    

    private void Awake() {
        inputSystem = new InputSystem();

        InitMouseInput();
        InitKeyboardInput();
        InitTouchInput();

    }

    private void OnEnable() {
        inputSystem.Camera.Enable();
        inputSystem.Game.Enable();
    }

    private void OnDisable() {
        inputSystem.Camera.Disable();
        inputSystem.Game.Disable();
    }


    // Keyboard

    void InitKeyboardInput() {
        inputSystem.Game.Esc.canceled += ctx => Exit(ctx);
    }

    void Exit(InputAction.CallbackContext context) {
        // Debug.Log("Exit key clicked");
        if(OnExit != null) {
            OnExit();
        }
    }

    // Mouse

    void InitMouseInput() {

        inputSystem.Camera.LeftMouseButton.started += ctx => HandleLeftMouseButtonDown(ctx);
        inputSystem.Camera.LeftMouseButton.canceled += ctx => HandleLeftMouseButtonUp(ctx);

        inputSystem.Camera.MousePosition.performed += ctx => HandleMouseMove(ctx);

        inputSystem.Camera.MouseWheel.started += ctx => HandleWheelStart();
        inputSystem.Camera.MouseWheel.canceled += ctx => HandleWheelEnd();
        inputSystem.Camera.MouseWheel.performed += ctx => HandleWheelValue(ctx);

    }

    private void HandleLeftMouseButtonDown(InputAction.CallbackContext context) {
        // Debug.Log($"Left Mouse Button Down at position {inputSystem.Camera.MousePosition.ReadValue<Vector2>()}");
        if(OnLeftMouseButtonDown != null) {
            OnLeftMouseButtonDown(inputSystem.Camera.MousePosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    private void HandleLeftMouseButtonUp(InputAction.CallbackContext context) {
        // Debug.Log($"Left Mouse Button Up at position {inputSystem.Camera.MousePosition.ReadValue<Vector2>()}");
        if(OnLeftMouseButtonUp != null) {
            OnLeftMouseButtonUp(inputSystem.Camera.MousePosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    private void HandleMouseMove(InputAction.CallbackContext context) {
        // Debug.Log($"Mouse Move at position {inputSystem.Camera.MousePosition.ReadValue<Vector2>()}");
        if(OnMouseMove != null) {
            OnMouseMove(inputSystem.Camera.MousePosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    private void HandleWheelStart() {
        // Debug.Log($"Wheel Started");
        if(OnWheelStart != null) {
            OnWheelStart();
        }
    }

    private void HandleWheelEnd() {
        // Debug.Log($"Wheel Ended");
        if(OnWheelEnd != null) {
            OnWheelEnd();
        }
    }

    private void HandleWheelValue(InputAction.CallbackContext context) {
        float value = context.ReadValue<Vector2>().y;
        // Debug.Log($"Wheel Move with value {context.ReadValue<Vector2>()}");
        if(OnWheelMove != null) {
            OnWheelMove(value);
        }
    }



    // Touch

    void InitTouchInput() {
        
        inputSystem.Camera.TouchPress.started += ctx => StartTouch(ctx);
        
        inputSystem.Camera.TouchPress.canceled += ctx => EndTouch(ctx);
        
        inputSystem.Camera.TouchPosition.performed += ctx => TouchMove(ctx);

        inputSystem.Camera.FingetTwoTouch.started += _ => ZoomStart();
        inputSystem.Camera.FingetTwoTouch.canceled += _ => ZoomEnd();

    }


    // Touch Move

    private void StartTouch(InputAction.CallbackContext context) {
        // Debug.Log($"Touch Started at position {inputSystem.Camera.TouchPosition.ReadValue<Vector2>()}");
        if(OnTouchStart != null) {
            OnTouchStart(inputSystem.Camera.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    private void EndTouch(InputAction.CallbackContext context) {
        // Debug.Log($"Touch Ended at position {inputSystem.Camera.TouchPosition.ReadValue<Vector2>()}");
        if(OnTouchEnd != null) {
            OnTouchEnd(inputSystem.Camera.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }
    }

    private void TouchMove(InputAction.CallbackContext context) {
        // Debug.Log($"Touch Move at position {inputSystem.Camera.TouchPosition.ReadValue<Vector2>()}");
        if(OnTouchMove != null) {
            OnTouchMove(inputSystem.Camera.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    // Touch Zoom

    private bool zoomEnabled = false;
    private float previousDistance = 0f;
    private float distance = 0f;

    private float GetFingetDistance() {
        return Vector2.Distance(inputSystem.Camera.FingerOnePosition.ReadValue<Vector2>(), inputSystem.Camera.FingerTwoPosition.ReadValue<Vector2>());
    }

    private void ZoomStart() {
        // Debug.Log($"Zoom Started");
        previousDistance = GetFingetDistance();
        zoomEnabled = true;
        if(OnZoomStart != null) {
            OnZoomStart();
        }
    }

    private void ZoomEnd() {
        // Debug.Log($"Zoom Ended");
        zoomEnabled = false;
        if(OnZoomEnd != null) {
            OnZoomEnd();
        }
    } 

    private void Update() {

        if (!zoomEnabled) {
            return;
        }

        distance = GetFingetDistance();

        if(OnZoomChange != null) {
            OnZoomChange(distance - previousDistance);
        }

        previousDistance = distance;

    }


}
