using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TabItems : MonoBehaviour, ITab
{
    private AppController appController;

    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private GameObject panel;

    private void Awake() {

        appController = AppController.Instance;
        
        appController.SetTabItems(this);

    }
    

    public void Toggle(bool active) {
        gameObject.SetActive(active);
    }
    public void Toggle() {
        Toggle(!gameObject.activeSelf);
    }


    public void UpdateUI(List<CatalogItem> catalogItems)
    {
        catalogItems.ForEach(catalogItem => {
            AddItem(catalogItem);
        });
    }


    private void AddItem(CatalogItem catalogItem)
    {   
        if (panel == null) return;
        if (itemPrefab == null) return;
        
        GameObject item = Instantiate<GameObject>(itemPrefab);
        item.transform.SetParent(panel.transform, false);
        
        Item itemUI = item.GetComponent<Item>();
        itemUI.UpdateUI(catalogItem);

    }
    
}
