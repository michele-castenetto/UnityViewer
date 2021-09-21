using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Item : MonoBehaviour
{
    
    private AppController appController;

    public string id;
    public CatalogItem catalogItem;
    
    private Button button;
    public Text text;
    public Image image;

    private void Awake() {

        appController = AppController.Instance;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);

    }

    private void OnButtonClick() {
        
        Debug.Log($"[Item.OnButtonClick] item id: {this.id}");

        appController.OnItemSelectEvent?.Invoke(id);
        
    }

    public void UpdateUI(CatalogItem catalogItem) {

        if (catalogItem == null) return;
        
        this.catalogItem = catalogItem;
        this.id = catalogItem.id;

        this.text.text = catalogItem.label;
        
        Sprite sprite = Resources.Load<Sprite>($"Images/{catalogItem.image}");
        this.image.sprite = sprite;
        
        // ##TODO
        // Texture2D image = AssetPreview.GetMiniThumbnail(Resources.Load("Models/Axe"));
        // Sprite sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0, 0), 100, 0 , SpriteMeshType.Tight);
        // this.image.sprite = sprite;

    }



}
