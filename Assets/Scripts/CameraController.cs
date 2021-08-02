using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Camera mainCamera;

    InputManager inputManager;

    bool orbitEnabled = false;
    
    bool zoomEnabled = false;

    [SerializeField]
    private float rotationSensibility = 0.1f;
    [SerializeField]
    private float wheelSensibility = 1000f;
    [SerializeField]
    private float zoomSensibility = 50f;



    float radius = 8f;
    float radiusMin = 1f;
    float radiusMax = 10f;


    float alpha = Mathf.Deg2Rad * 0f;
    float beta = Mathf.Deg2Rad * 0f;
    
    
    // The mouse cursor's position during the last frame
    Vector3 last = new Vector3();
 
    // The target that the camera looks at
    Vector3 target = new Vector3 (0, 0, 0);
 
    // The spherical coordinates
    Vector3 sc = new Vector3();
    

    // Pointer Inputs
    // --------------------------------

    bool leftMouseButtonDown = false;
    bool pointerDown = false;

    Vector2 mousePosition = new Vector2(0, 0);
    Vector2 pointerPosition = new Vector2(0, 0);

    float wheelValue = 0;
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
     

    void Start()
    {

        mainCamera = Camera.main;
        inputManager = InputManager.Instance;
        
        // inputManager.OnTouchStart += (pos, time) => setLeftMouseButtonDown(true);
        // inputManager.OnTouchEnd += (pos, time) => setLeftMouseButtonDown(false);
        // inputManager.OnTouchMove += (pos, time) => setMousePosition(pos);
        inputManager.OnTouchStart += (pos, time) => setPointerDown(true);
        inputManager.OnTouchEnd += (pos, time) => setPointerDown(false);
        inputManager.OnTouchEnd += (pos, time) => DetectPick();
        inputManager.OnTouchMove += (pos, time) => setPointerPosition(pos);

        inputManager.OnZoomStart += () => setZoomEnabled(true);
        inputManager.OnZoomEnd += () => setZoomEnabled(false);
        inputManager.OnZoomChange += (value) => setZoomValue(value);

        // Camera start position
        this.transform.position = new Vector3(radius, alpha, beta);
        // Camera Target 
        this.transform.LookAt(target);

        sc = getSphericalCoordinates(this.transform.position);

    }


    public void setLeftMouseButtonDown(bool isDown) {
        this.leftMouseButtonDown = isDown;
    }
    public void setMousePosition(Vector2 position) {
        this.mousePosition = position;
    }
    public void setWheelValue(Vector2 value) {
        this.wheelValue = value.y;
    }

    public void setPointerDown(bool isDown) {
        this.pointerDown = isDown;
    }

    public void setPointerPosition(Vector2 position) {
        this.pointerPosition = position;
    }

    public void setZoomEnabled(bool enabled) {
        this.zoomEnabled = enabled;
    }

    public void setZoomValue(float value) {
        this.zoomValue = value;
    }



    void Update ()
    {
        
        // ##OLD
        // if(wheelValue != 0) {
        //     sc.x -= wheelValue / wheelSensibility;
        //     transform.position = getCartesianCoordinates(sc) + target;
        //     transform.LookAt (target);
        // }

        if (zoomEnabled) {
            
            float radius = sc.x - Time.deltaTime * zoomValue/zoomSensibility;
            radius = Mathf.Clamp(radius, radiusMin, radiusMax);
            sc.x = radius;
            
            transform.position = getCartesianCoordinates(sc) + target;
            transform.LookAt(target);

            return;

        } 

        if (pointerDown == true && orbitEnabled == false) {
            last = new Vector2(pointerPosition.x, pointerPosition.y);
            // Debug.Log(last);
            orbitEnabled = true;
        }

        if (pointerDown == false ) {
            orbitEnabled = false;
        }

        if(!orbitEnabled) {
            return;
        }

        // Get the deltas that describe how much the mouse cursor got moved between frames
        float dx = (last.x - pointerPosition.x) * rotationSensibility;
        float dy = (last.y - pointerPosition.y) * rotationSensibility;
        // Debug.Log(dx);


        // Only update the camera's position if the mouse got moved in either direction
        if (dx != 0f || dy != 0f)
        {

            // Rotate the camera left and right
            sc.y += dx * Time.deltaTime;

            // Rotate the camera up and down
            // Prevent the camera from turning upside down (1.5f = approx. Pi / 2)
            sc.z = Mathf.Clamp (sc.z + dy * Time.deltaTime, -1.5f, 1.5f);

            // Calculate the cartesian coordinates
            transform.position = getCartesianCoordinates(sc) + target;

            // Look at the target
            transform.LookAt(target);
        }

        // Update the last pointer position
        last = new Vector2(pointerPosition.x, pointerPosition.y);
        
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



}


