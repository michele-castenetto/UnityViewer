using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    private AppController appController;
    private Button button;
    private void Awake() {
        appController = AppController.Instance;
        button = GetComponent<Button>();
        button.onClick.AddListener(() => appController.OnButtonInfoClickEvent?.Invoke());
    }
}
