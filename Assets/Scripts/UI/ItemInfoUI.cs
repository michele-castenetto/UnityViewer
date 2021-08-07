using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    // public GameObject itemsTab;
    private List<ItemUI> itemList;
    private AppController appController;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;

    private void Awake() {

        appController = AppController.Instance;
        
        appController.SetItemInfo(this);

    }


    private void Start() {
        appController.OnInfoEvent += () => ToggleItemsTab();
    }

    
    private void ToggleItemsTab() {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    
    public void UpdateUI(CatalogItem catalogItem)
    {

        if (title != null)
        {
            title.text = catalogItem.info.title;
        }
        if (description != null)
        {
            description.text = catalogItem.info.description;
        }

    }

}
