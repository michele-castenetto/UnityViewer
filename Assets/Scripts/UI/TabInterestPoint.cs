using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabInterestPoint : MonoBehaviour, ITab
{

    private AppController appController;
    
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;
    [SerializeField]
    private Button button;

    private void Awake() {

        Toggle(false);

        appController = AppController.Instance;
        
        appController.SetTabInterestPoint(this);

        if (button) {
            button.onClick.AddListener(() => Toggle(false));
        }

    }


    public void Toggle(bool active) {
        gameObject.SetActive(active);
    }
    public void Toggle() {
        Toggle(!gameObject.activeSelf);
    }

    
    public void UpdateUI(ItemPoint itemPoint)
    {

        if (title != null) {
            title.text = itemPoint.info.title;
        }

        if (description != null) {
            description.text = itemPoint.info.description;
        }

    }

}
