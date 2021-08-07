using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Image))]
public class ItemUI : MonoBehaviour
{

    private AppController appController;

    public string id;
    public string label;
    public Sprite sprite;

    private Button button;
    private Text text;
    public Image image;

    private void Awake() {

        appController = AppController.Instance;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

        // List<Component> components = GetComponentsInChildren<Component>().ToList();
        // components.ForEach(component => Debug.Log(component));
        
        //##OLD
        // image.sprite = sprite;
        
    }

    private void OnButtonClick() {
        Debug.Log(this.id);

        appController.UpdateItemInfo(id);
        
        // appController.LoadObject(id);
        appController.OnItemSelectEvent?.Invoke(id);
        
    }



}
