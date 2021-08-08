using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ConfigData
{
    public string appname;
    public string version;
    public string date;
}


public class ConfigReader
{

    ConfigData config = null;

    
    // Read from the assets path, don't work in case of a mobile app build
    public string readFileFromDataPath(string filePath) {
        var sr = new StreamReader(Application.dataPath + filePath);
        var content = sr.ReadToEnd();
        sr.Close();

        return content;
    }

    public string ReadFileFromResources(string name) {
        
        TextAsset testAsset = (TextAsset)Resources.Load(name);
        string content = testAsset.text;
        return content;

    }

    public ConfigData readConfig() {
        
        // ##OLD
        // string filePath = "/config/config.json";
        // string content = readTextFile(filePath);

        string content = ReadFileFromResources("config/config");

        this.config = JsonReader.parseJson<ConfigData>(content);

        return this.config;

    }


    public List<CatalogItem> readCatalog() {

        UnityEngine.Object[] objList = Resources.LoadAll("Catalog");
        
        List<CatalogItem> catalogItems = new List<CatalogItem>();
        foreach (var obj in objList)
        {
            TextAsset textAsset = (TextAsset)obj;
            CatalogItem item = JsonReader.parseJson<CatalogItem>(textAsset.text);
            catalogItems.Add(item);
        }

        return catalogItems;

    }

}







