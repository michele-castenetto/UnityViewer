 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(0)]
public class AppController : Singleton<AppController>
{
    
    private InputManager inputManager;
    
    private ConfigReader configReader;

    // Catalog items
    List<CatalogItem> catalogItems;
    // Current Catalog Item
    CatalogItem catalogItemSelected;
    // Current itemObj
    GameObject itemObjSelected;

    // Events
    public delegate void FocusEvent();
    public FocusEvent OnFocusEvent;
    public delegate void InfoEvent();
    public InfoEvent OnInfoEvent;
    public delegate void ItemSelectEvent(string id);
    public ItemSelectEvent OnItemSelectEvent;

    
    // Link dependencies
    private CameraController cameraController;
    public void SetCameraController(CameraController controller) {
        cameraController = controller;
    }
    private ItemsTabUI itemsTab;
    public void SetItemsTab(ItemsTabUI _itemsTab) {
        itemsTab = _itemsTab;
    }
    private ItemInfoUI itemInfo;
    public void SetItemInfo(ItemInfoUI _itemInfo) {
        itemInfo = _itemInfo;
    }
    
    private void Awake() {

        // Debug.Log("[AppController.Awake]");
        
        configReader = new ConfigReader();

        inputManager = InputManager.Instance;

    }

    private void Start()
    {
        // Debug.Log("[AppController.Start]");

        // Debug.Log($"[AppController.Start] itemsTab: {itemsTab}");

        configReader.readConfig();

        // Load catalog

        catalogItems = configReader.readCatalog();

        itemsTab.UpdateUI(catalogItems);

        cameraController.setStartPosition(8f, 0, 0);

        // Events link

        inputManager.OnExit += () => Exit();

        OnItemSelectEvent += id => LoadObject(id);


        // 

        Debug.Log("[AppController.Start] App Started");

    }
    
    private void Exit() {
        Debug.Log("Exit from app");
        Application.Quit();
    }

    public void UpdateItemInfo(string id) {

        CatalogItem catalogItem = catalogItems.Find(catalogItem => catalogItem.id == id);
        if (catalogItem == null) return;
        
        itemInfo.UpdateUI(catalogItem);

    }


    public void LoadObject(string id) {

        if (itemObjSelected != null)
        {
            DestroyImmediate(itemObjSelected);
            // Destroy(itemObjSelected, 0);
        }

        CatalogItem catalogItem = catalogItems.Find(catalogItem => catalogItem.id == id);
        catalogItemSelected = catalogItem;
        if (catalogItem == null) return;
        
        // GameObject itemObj = Resources.Load($"Models/{catalogItem.geometry.file}") as GameObject;
        GameObject loadedObj = Resources.Load<GameObject>($"Models/{catalogItem.geometry.file}");
        if (loadedObj == null) {
            Debug.Log("[AppController.LoadObject] itemObj is null");
            return;
        }
        Vector3 startPosition = new Vector3(0, 2, 0);
        GameObject itemObj = Instantiate(loadedObj, startPosition, Quaternion.identity);
        itemObj.name = catalogItem.id;
        
        itemObjSelected = itemObj;

        cameraController.FocusItemObject(itemObj);

    }
    

}
