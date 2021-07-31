using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    
    InputSystem inputSystem;
    CameraController cameraController;
    // MainController mainController;


    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch; 

    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch; 


    private void Awake() {
        inputSystem = new InputSystem();

        cameraController = GetComponent<CameraController>();
        // mainController = GetComponent<MainController>();
        
        InitInputSystem();
    }

    private void OnEnable() {
        inputSystem.Camera.Enable();
        inputSystem.Game.Enable();
    }


    private void OnDisable() {
        inputSystem.Camera.Disable();
        inputSystem.Game.Disable();
    }



    void InitInputSystem() {
        
        // Mouse

        // inputSystem.Camera.LeftMouseButton.started += ctx => cameraController.setLeftMouseButtonDown(true);
        // inputSystem.Camera.LeftMouseButton.canceled += ctx => cameraController.setLeftMouseButtonDown(false);

        // inputSystem.Camera.MousePosition.performed += ctx => cameraController.setMousePosition(ctx.ReadValue<Vector2>());

        // inputSystem.Camera.MouseWheel.performed += ctx => cameraController.setWheelValue(ctx.ReadValue<Vector2>());
        // inputSystem.Camera.MouseWheel.canceled += ctx => cameraController.setWheelValue(new Vector2(0, 0));

        // Touch

        // inputSystem.Camera.TouchPress.started += ctx => Debug.Log("touch started");
        inputSystem.Camera.TouchPress.started += ctx => cameraController.setLeftMouseButtonDown(true);
        inputSystem.Camera.TouchPress.started += ctx => StartTouch(ctx);
        // inputSystem.Camera.TouchPress.canceled += ctx => Debug.Log("touch finish");
        inputSystem.Camera.TouchPress.canceled += ctx => cameraController.setLeftMouseButtonDown(false);
        inputSystem.Camera.TouchPress.canceled += ctx => EndTouch(ctx);
        // inputSystem.Camera.TouchPosition.performed += ctx => Debug.Log(ctx.ReadValue<Vector2>());
        inputSystem.Camera.TouchPosition.performed += ctx => cameraController.setMousePosition(ctx.ReadValue<Vector2>());
        
        // Keyboard

        inputSystem.Game.Esc.canceled += ctx => cameraController.Exit();



    }

    void StartTouch(InputAction.CallbackContext context) {
        Debug.Log($"Touch Started at position {inputSystem.Camera.TouchPosition.ReadValue<Vector2>()}");
        if(OnStartTouch != null) {
            OnStartTouch(inputSystem.Camera.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    void EndTouch(InputAction.CallbackContext context) {
        Debug.Log($"Touch Ended at position {inputSystem.Camera.TouchPosition.ReadValue<Vector2>()}");
        if(OnStartTouch != null) {
            OnEndTouch(inputSystem.Camera.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }
    }





}
