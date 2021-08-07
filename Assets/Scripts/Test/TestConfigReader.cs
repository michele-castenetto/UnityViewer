using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestConfigReader : MonoBehaviour
{

    [ContextMenu("readCatalog")]
    public void readCatalog() {

        ConfigReader reader = new ConfigReader();

        List<CatalogItem> catalogItems = reader.readCatalog();
        catalogItems.ForEach(item => {
            // Debug.Log(item.id);
            // Debug.Log(item.points);
            Debug.Log(JsonReader.toJson(item));
            // for (int i = 0; i < item.points.Length; i++)
            // {
            //     Debug.Log(item.points[i].id);
            //     Debug.Log(item.points[i].position);
            // }
        });

    }


    [ContextMenu("readConfigFile")]
    public void readConfigFile() {

        ConfigReader reader = new ConfigReader();

        ConfigData config = reader.readConfig();
        
        Debug.Log($"App name: {config.appname}");
        Debug.Log($"App version: {config.version}");
        
    }


    [ContextMenu("parseJson")]
    public void parseJson() {

        string json = "{\"appname\": \"ObjectViewer\", \"version\": \"0.1\"}";
        
        ConfigData config = JsonReader.parseJson<ConfigData>(json);

        Debug.Log($"App name: {config.appname}");
        Debug.Log($"App version: {config.version}");
        
    }
    

}