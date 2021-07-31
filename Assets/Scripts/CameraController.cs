using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    //##WORK limiti per raggio e valori degli angoli



    bool orbitEnabled = false;
 
    float rotationSpeed = 1f;
    float wheelSensibility = 1000f;




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
    

    // Mouse Inputs
    // --------------------------------

    bool leftMouseButtonDown = false;

    Vector2 mousePosition = new Vector2(0, 0);

    float wheelValue = 0;



    // ##OLD
    // Touch Inputs
    // --------------------------------
    // bool touchActive = false;
    // Vector2 touchPosition = new Vector2(0, 0);
    // public void setTouchActive(bool active) {
    //     Debug.Log($"Touch is {active}");
    //     this.touchActive = active;
    // }
    // public void setTouchPosition(Vector2 position) {
    //     // Debug.Log(position);
    //     this.touchPosition = position;
    // }



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

    public void Exit() {
        // Debug.Log("Exit key clicked");
        Application.Quit();
    }

    void Update ()
    {
        
        // Debug.Log(wheelValue);
        if(wheelValue != 0) {

            // Change the camera distance from target
            sc.x -= wheelValue / wheelSensibility;
            transform.position = getCartesianCoordinates(sc) + target;
            transform.LookAt (target);

        }

        if ( leftMouseButtonDown == true && orbitEnabled == false) {
            last = new Vector2(mousePosition.x, mousePosition.y);
            orbitEnabled = true;
        }

        if ( leftMouseButtonDown == false ) {
            orbitEnabled = false;
        }

        if(!orbitEnabled) {
            return;
        }


        
        // Get the deltas that describe how much the mouse cursor got moved between frames
        float dx = (last.x - mousePosition.x) * rotationSpeed;
        float dy = (last.y - mousePosition.y) * rotationSpeed;
        // Debug.Log(dx);
        // Debug.Log(dy);

        // Only update the camera's position if the mouse got moved in either direction
        if (dx != 0f || dy != 0f)
        {

            // Rotate the camera left and right
            sc.y += dx * Time.deltaTime;

            // Rotate the camera up and down
            // Prevent the camera from turning upside down (1.5f = approx. Pi / 2)
            sc.z = Mathf.Clamp (sc.z + dy * Time.deltaTime, -1.5f, 1.5f);

            // Calculate the cartesian coordinates for unity
            transform.position = getCartesianCoordinates(sc) + target;

            // Make the camera look at the target
            transform.LookAt (target);
        }

        // Update the last mouse position
        last = new Vector2(mousePosition.x, mousePosition.y);
        
    }


}


