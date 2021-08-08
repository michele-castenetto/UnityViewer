

[System.Serializable]
public class Info 
{
    public string title;
    public string description;
}

[System.Serializable]
public class ItemPoint 
{
    public string id;
    public string label;
    public float[] position;
    public Info info;

}

[System.Serializable]
public class Geometry
{
    public string file;
}
[System.Serializable]
public class CatalogItem
{
    public string id;
    public string label;
    public float[] position;
    public float[] rotation;
    public string image;
    public Geometry geometry;
    public Info info;
    public ItemPoint[] points;

}