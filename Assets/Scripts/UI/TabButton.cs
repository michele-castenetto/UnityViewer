using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    private AppController appController;
    private Button button;


    [SerializeField]
    private GameObject targetTab;        
    // [SerializeField]
    // private Sprite imageActive;
    // [SerializeField]
    // private Sprite imageUnActive;
    // [SerializeField]
    // private Image image;

    private void Awake() {
        appController = AppController.Instance;
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleTab);
    }

    private void ToggleTab() {
        targetTab.GetComponent<ITab>().Toggle();
        // image.sprite = targetTab.activeSelf ? imageActive : imageUnActive;
    }

}
