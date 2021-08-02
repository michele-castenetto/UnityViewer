using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ConfigData
{
    public string appname;
    public string version;
    public string date;
}

public class ConfigReader
{

    ConfigData config = null;
    
    public string readTextFile(string filePath) {
        var sr = new StreamReader(Application.dataPath + filePath);
        var content = sr.ReadToEnd();
        sr.Close();

        return content;
    }

    public string ReadFromResources() {
        
        TextAsset testAsset = (TextAsset)Resources.Load("config");
        string content = testAsset.text;
        return content;
    }

    public ConfigData read() {
        
        // string filePath = "/config/config.json";
        // string content = readTextFile(filePath);

        string content = ReadFromResources();

        this.config = JsonReader.parseJson<ConfigData>(content);

        return this.config;

    }

}







