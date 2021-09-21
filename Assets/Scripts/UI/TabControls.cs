using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabControls : MonoBehaviour, ITab
{

    private AppController appController;
    
    private void Awake() {

        Toggle(false);

        appController = AppController.Instance;
        
    }


    public void Toggle(bool active) {
        gameObject.SetActive(active);
    }
    public void Toggle() {
        Toggle(!gameObject.activeSelf);
    }

}
