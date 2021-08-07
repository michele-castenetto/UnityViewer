using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "ItemList/ItemData")]
public class ItemData : ScriptableObject
{

    public string id = "";
    public Sprite image = null;
    public bool isActive = true;
    public string geometryFile = "";

}
