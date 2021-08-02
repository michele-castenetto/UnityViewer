using UnityEngine;

public class JsonReader
{

    public static T parseJson<T>(string json) {

        T result = JsonUtility.FromJson<T>(json);

        return result;
    }

    public static string toJson(object obj) {

        string json = JsonUtility.ToJson(obj);
        
        return json;
    }

}