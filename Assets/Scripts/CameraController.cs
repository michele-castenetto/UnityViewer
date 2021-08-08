using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    Camera mainCamera;
    InputManager inputManager;
    AppController appController;



    [SerializeField]
    private float rotationSensibility = 0.1f;
    [SerializeField]
    private float zoomSensibility = 50f;


    float radius = 8f;
    float radiusMin = 2f;
    float radiusMax = 15f;
    float alpha = Mathf.Deg2Rad * 30f;
    float beta = Mathf.Deg2Rad * 30;
    float betaMin = Mathf.Deg2Rad * -89;
    float betaMax = Mathf.Deg2Rad * 89;


    // The last frame pointer position  
    Vector3 lastPointerPosition = new Vector3();
 
    // The target of camera
    Vector3 target = new Vector3();
 
    // Spherical coordinates (radius, alpha, beta)
    Vector3 sc = new Vector3();
    

    bool orbitEnabled = false;
    
    bool zoomEnabled = false;

    bool pointerDown = false;

    Vector2 pointerPosition = new Vector2(0, 0);

    float zoomValue = 0;



    // Helpers Spherical coordinates
    // --------------------------------

    Vector3 getSphericalCoordinates(Vector3 cartesian)
    {
        float r = Mathf.Sqrt(
            Mathf.Pow(cartesian.x, 2) + 
            Mathf.Pow(cartesian.y, 2) + 
            Mathf.Pow(cartesian.z, 2)
        );
 
        float phi = Mathf.Atan2(cartesian.z / cartesian.x, cartesian.x);
        float theta = Mathf.Acos(cartesian.y / r);
 
        if (cartesian.x < 0)
            phi += Mathf.PI;
 
        return new Vector3 (r, phi, theta);
    }
 
    Vector3 getCartesianCoordinates(Vector3 spherical)
    {
        Vector3 ret = new Vector3 ();
 
        ret.x = spherical.x * Mathf.Cos (spherical.z) * Mathf.Cos (spherical.y);
        ret.y = spherical.x * Mathf.Sin (spherical.z);
        ret.z = spherical.x * Mathf.Cos (spherical.z) * Mathf.Sin (spherical.y);
 
        return ret;
    }
     


    private void Awake() {
        AppController.Instance.SetCameraController(this);
    }
    

    private void Start()
    {

        mainCamera = Camera.main;
        inputManager = InputManager.Instance;
        appController = AppController.Instance;
        
        // Camera Inputs
        initInputMouseEvents();
        initInputTouchEvents();
        // initAppEvents();
        
        // Camera start position
        sc = new Vector3(radius, alpha, beta);
        this.transform.position = getCartesianCoordinates(sc);;
        
        // Camera Target 
        // Debug.Log($"[CameraController.Start] Target is {target}");
        this.transform.LookAt(target);

    }


    // private void initAppEvents() {
    //     appController.OnFocusEvent += () => Debug.Log("focus with camera");
    // }


    private void initInputMouseEvents() {

        inputManager.OnLeftMouseButtonDown += (pos, time) => setPointerDown(true);
        inputManager.OnLeftMouseButtonUp += (pos, time) => setPointerDown(false);
        inputManager.OnMouseMove += (pos, time) => setPointerPosition(pos);
        
        inputManager.OnWheelStart += () => setZoomEnabled(true);
        inputManager.OnWheelEnd += () => setZoomEnabled(false);
        inputManager.OnWheelMove += (value) => setZoomValueFromMouse(value);
        
        inputManager.OnLeftMouseButtonUp += (pos, time) => DetectPick();

    }

    private void initInputTouchEvents() {

        inputManager.OnTouchStart += (pos, time) => setPointerDown(true);
        inputManager.OnTouchEnd += (pos, time) => setPointerDown(false);
        inputManager.OnTouchMove += (pos, time) => setPointerPosition(pos);

        inputManager.OnZoomStart += () => setZoomEnabled(true);
        inputManager.OnZoomEnd += () => setZoomEnabled(false);
        inputManager.OnZoomChange += (value) => setZoomValue(value);

        inputManager.OnTouchEnd += (pos, time) => DetectPick();

    }


    private void setPointerDown(bool isDown) {
        this.pointerDown = isDown;
    }

    private void setPointerPosition(Vector2 position) {
        this.pointerPosition = position;
    }

    private void setZoomEnabled(bool enabled) {
        this.zoomEnabled = enabled;
    }

    private void setZoomValue(float value) {
        this.zoomValue = value;
    }
    private void setZoomValueFromMouse(float value) {
        this.zoomValue = value;
    }



    private void Update ()
    {
        
        // If pointer interact with canvas UI do nothing
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        if (zoomEnabled) {
            
            float radius = sc.x - Time.deltaTime * zoomValue/zoomSensibility;
            radius = Mathf.Clamp(radius, radiusMin, radiusMax);
            sc.x = radius;
            
            transform.position = getCartesianCoordinates(sc) + target;
            transform.LookAt(target);

            return;

        } 

        if (pointerDown == true && orbitEnabled == false) {
            lastPointerPosition = new Vector2(pointerPosition.x, pointerPosition.y);
            orbitEnabled = true;
        }

        if (pointerDown == false ) {
            orbitEnabled = false;
        }

        if(!orbitEnabled) {
            return;
        }

        float alphaChange = (lastPointerPosition.x - pointerPosition.x) * rotationSensibility * Time.deltaTime;
        float betaChange = (lastPointerPosition.y - pointerPosition.y) * rotationSensibility * Time.deltaTime;

        // Only update the camera's position if the mouse got moved in either direction
        if (alphaChange != 0f || betaChange != 0f)
        {
            
            // Rotate the camera on alpha angle (left and right)
            sc.y += alphaChange;
            // Rotate the camera on beta angle (up and down)
            sc.z += betaChange;
            sc.z = Mathf.Clamp(sc.z, betaMin, betaMax);

            // Sperical -> Cartesian
            transform.position = getCartesianCoordinates(sc) + target;

            // Look at the target
            transform.LookAt(target);

        }

        // Update the last pointer position
        lastPointerPosition = new Vector2(pointerPosition.x, pointerPosition.y);
        
    }



    private void DetectPick() {

        Ray ray = mainCamera.ScreenPointToRay(pointerPosition);
        RaycastHit hit; 
        if (Physics.Raycast(ray, out hit)){
            if (hit.collider != null) {
                // Debug.Log(hit.collider.tag);
                ISelectable selectable = hit.collider.gameObject.GetComponent<ISelectable>();
                if (selectable != null){
                    selectable.OnSelectAction();
                }
            }
        }

    }


    public void setStartPosition(float radius, float alpha, float beta) {
        this.radius = radius;
        this.alpha = alpha * Mathf.Deg2Rad;
        this.beta = beta * Mathf.Deg2Rad;
    }


    public void FocusItemObject(GameObject gameObject) {

        if (gameObject == null) return;
        
        this.target = gameObject.transform.position;

        // Collider collider = gameObject.GetComponent<Collider>();
        // if (collider == null) return;

        // float[] ext = new float[] {
        //     collider.bounds.extents.x,
        //     collider.bounds.extents.y,
        //     collider.bounds.extents.z
        // };
        // float radius = Mathf.Max(ext);
        // Debug.Log($"Radius: {radius}");

        // radiusMin = radius * 1.5f;
        // radiusMax = radius * 6f;
        

        
        // Renderer renderer = gameObject.GetComponent<Renderer>();
        
        

    }



}


