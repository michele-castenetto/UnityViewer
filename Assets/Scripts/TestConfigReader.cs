using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestConfigReader : MonoBehaviour
{

    [ContextMenu("readConfigFile")]
    public void readConfigFile() {

        ConfigReader reader = new ConfigReader();

        ConfigData config = reader.read();
        
        Debug.Log($"App name: {config.appname}");
        Debug.Log($"App version: {config.version}");
        
    }


    [ContextMenu("parseJson")]
    public void parseJson() {

        string json = "{\"appname\": \"ObjectViewer\"}";

        ConfigData config = JsonReader.parseJson<ConfigData>(json);

        Debug.Log($"App name: {config.appname}");
        Debug.Log($"App version: {config.version}");
        
    }
    

}