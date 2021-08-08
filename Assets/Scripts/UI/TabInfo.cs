using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabInfo : MonoBehaviour, ITab
{

    private AppController appController;
    
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;

    private void Awake() {

        appController = AppController.Instance;
        
        appController.SetTabInfo(this);

    }


    // ##OLD
    // private void Start() {
    //     appController.OnInfoEvent += () => ToggleTabInfo();
    // }

    
    public void Toggle(bool active) {
        gameObject.SetActive(active);
    }
    public void Toggle() {
        Toggle(!gameObject.activeSelf);
    }

    
    public void UpdateUI(CatalogItem catalogItem)
    {

        if (title != null) {
            title.text = catalogItem.info.title;
        }

        if (description != null) {
            description.text = catalogItem.info.description;
        }

    }

}
