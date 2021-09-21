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


    // UI Events
    public delegate void ButtonFocusClickEvent();
    public ButtonFocusClickEvent OnButtonFocusClickEvent;
    public delegate void ButtonInfoClickEvent();
    public ButtonInfoClickEvent OnButtonInfoClickEvent;
    public delegate void ItemSelectEvent(string id);
    public ItemSelectEvent OnItemSelectEvent;
    public delegate void MeshPickEvent(GameObject gameObj);
    public MeshPickEvent OnMeshPickEvent;
    public delegate void InterestPointPickEvent(GameObject gameObj);
    public InterestPointPickEvent OnInterestPointPickEvent;
    
    // Materials

    Material interestPointMaterial;



    // Link dependencies
    private CameraController cameraController;
    public void SetCameraController(CameraController controller) {
        cameraController = controller;
    }
    private TabItems tabItems;
    public void SetTabItems(TabItems _tabItems) {
        tabItems = _tabItems;
    }
    private TabInfo tabInfo;
    public void SetTabInfo(TabInfo _tabInfo) {
        this.tabInfo = _tabInfo;
    }
    private TabInterestPoint tabTabInterestPoint;
    public void SetTabInterestPoint(TabInterestPoint _tabTabInterestPoint) {
        this.tabTabInterestPoint = _tabTabInterestPoint;
    }

    
    private void Awake() {

        // Debug.Log("[AppController.Awake]");
        
        configReader = new ConfigReader();

        inputManager = InputManager.Instance;

    }

    private void Start()
    {

        // Load Materials
        LoadMaterials();

        // Read Config file

        configReader.readConfig();

        // Load catalog

        catalogItems = configReader.readCatalog();

        tabItems.UpdateUI(catalogItems);

        // Set Camera position

        cameraController.setStartPosition(8f, 30f, 30f);

        // Events link

        inputManager.OnExit += () => Exit();

        OnItemSelectEvent += id => LoadObject(id);

        OnItemSelectEvent += id => UpdateTabInfo(id);

        OnButtonInfoClickEvent += () => tabInfo.Toggle();

        // ##OLD
        // OnButtonFocusClickEvent += () => Debug.Log("focus with camera");

        OnMeshPickEvent += (gameObj) => MeshPickEventHandler(gameObj);

        // Log App Start

        Debug.Log("[AppController.Start] App Started");

    }

    private void MeshPickEventHandler(GameObject gameObj) {

        InterestPoint interestPoint = gameObj.GetComponent<InterestPoint>();
        if (interestPoint == null) return;
        interestPoint.OnPickAction();

        tabTabInterestPoint.UpdateUI(interestPoint.itemPoint);
        tabTabInterestPoint.Toggle(true);

    }

    
    private void Exit() {
        Debug.Log("Exit from app");
        Application.Quit();
    }

    public void UpdateTabInfo(string id) {

        CatalogItem catalogItem = catalogItems.Find(catalogItem => catalogItem.id == id);
        if (catalogItem == null) return;
        
        tabInfo.UpdateUI(catalogItem);

    }


    public void LoadObject(string id) {

        if (itemObjSelected != null) {
            DestroyImmediate(itemObjSelected);
        }

        CatalogItem catalogItem = catalogItems.Find(catalogItem => catalogItem.id == id);
        catalogItemSelected = catalogItem;
        if (catalogItem == null) return;
        
        GameObject loadedObj = Resources.Load<GameObject>($"Models/{catalogItem.geometry.prefab}");
        if (loadedObj == null) {
            Debug.Log($"[AppController.LoadObject] Resource model {catalogItem.geometry.prefab} is null");
            return;
        }
        Vector3 startPosition = new Vector3(0, 2, 0);
        GameObject itemObj = Instantiate(loadedObj, startPosition, Quaternion.identity);
        itemObj.name = catalogItem.id;
        
        itemObjSelected = itemObj;

        cameraController.FocusItemObject(itemObj);

        for (int i = 0; i < catalogItem.points.Length; i++)
        {
            AddInterestPoint(itemObj, catalogItem.points[i]);
        }

    }
    
    public void LoadMaterials() {

        interestPointMaterial = Resources.Load<Material>("Materials/InterestPoint");

    }

    private void AddInterestPoint(GameObject itemObj, ItemPoint itemPoint) {

        Vector3 position = new Vector3(itemPoint.position[0], itemPoint.position[1], itemPoint.position[2] );  
        // Debug.Log($"[AppController.AddInterestPoint] point position: {position}");
        // Debug.Log($"[AppController.AddInterestPoint] itemObj position: {itemObj.transform.position}");

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
        sphere.transform.SetParent(itemObj.transform);

        sphere.transform.position = itemObj.transform.position + position;
        sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        sphere.name = "InterestPoint_" + itemPoint.id;
        sphere.tag = "pickable";

        InterestPoint interestPoint = sphere.AddComponent<InterestPoint>();
        // Debug.Log($"[AppController.AddInterestPoint] interestPoint {interestPoint}");

        // InterestPoint interestPoint2 = sphere.GetComponent<InterestPoint>();
        // Debug.Log($"[AppController.AddInterestPoint] interestPoint2 {interestPoint2}");

        interestPoint.itemPoint = itemPoint;

        MeshRenderer meshRenderer = sphere.GetComponent<MeshRenderer>();
        
        // ##TODO
        // meshRenderer.allowOcclusionWhenDynamic = false;
        // meshRenderer.sortingLayerName = "UI";
        // meshRenderer.sortingOrder = 0;

        meshRenderer.material = interestPointMaterial;

    }


}
