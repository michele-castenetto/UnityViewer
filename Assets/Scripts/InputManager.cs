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

        // InitMouseInput();
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
        Debug.Log("Exit key clicked");
        if(OnExit != null) {
            OnExit();
        }
    }

    // Mouse

    void InitMouseInput() {
        // inputSystem.Camera.LeftMouseButton.started += ctx => cameraController.setLeftMouseButtonDown(true);
        // inputSystem.Camera.LeftMouseButton.canceled += ctx => cameraController.setLeftMouseButtonDown(false);

        // inputSystem.Camera.MousePosition.performed += ctx => cameraController.setMousePosition(ctx.ReadValue<Vector2>());

        // inputSystem.Camera.MouseWheel.performed += ctx => cameraController.setWheelValue(ctx.ReadValue<Vector2>());
        // inputSystem.Camera.MouseWheel.canceled += ctx => cameraController.setWheelValue(new Vector2(0, 0));
    }

    // Touch

    void InitTouchInput() {
        
        // inputSystem.Camera.TouchPress.started += ctx => cameraController.setLeftMouseButtonDown(true);
        inputSystem.Camera.TouchPress.started += ctx => StartTouch(ctx);
        
        // inputSystem.Camera.TouchPress.canceled += ctx => cameraController.setLeftMouseButtonDown(false);
        inputSystem.Camera.TouchPress.canceled += ctx => EndTouch(ctx);
        
        // inputSystem.Camera.TouchPosition.performed += ctx => cameraController.setMousePosition(ctx.ReadValue<Vector2>());
        inputSystem.Camera.TouchPosition.performed += ctx => TouchMove(ctx);
        
        // inputSystem.Camera.FingetTwoTouch.started += _ => ZoomStart();
        // inputSystem.Camera.FingetTwoTouch.canceled += _ => ZoomEnd();

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
