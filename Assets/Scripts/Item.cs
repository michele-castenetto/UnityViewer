using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ISelectable
{

    public void OnSelectAction() {
        Debug.Log(gameObject.tag);
    }
    

}
