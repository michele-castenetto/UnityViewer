 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    
    InputManager inputManager;
    
    void Start()
    {
        
        inputManager = InputManager.Instance;

        inputManager.OnExit += () => Exit();

        Debug.Log("App Started");
           
    }
    
    public void Exit() {
        Debug.Log("Exit from app");
        Application.Quit();
    }

    
}
