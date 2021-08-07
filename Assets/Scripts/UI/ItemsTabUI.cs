using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemsTabUI : MonoBehaviour
{

    private List<ItemUI> itemList;
    private AppController appController;
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private GameObject panel;

    private void Awake() {

        // Debug.Log("[ItemsTabUI.Awake]");

        appController = AppController.Instance;
        
        // itemList = GetComponentsInChildren<ItemUI>().ToList();

        // itemList.ForEach(item => item.gameObject.SetActive(false));

        appController.SetItemsTab(this);

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
        if (catalogItem == null) return;
        
        GameObject item = Instantiate(itemPrefab) as GameObject;
        item.transform.SetParent(panel.transform, false);

        ItemUI itemUI = item.GetComponent<ItemUI>();
        itemUI.id = catalogItem.id;
        itemUI.label = catalogItem.label;

        Sprite sprite = Resources.Load<Sprite>($"Images/{catalogItem.image}");
        itemUI.image.sprite = sprite;
        
    }
    
}
