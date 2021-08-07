using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour, ISelectable
{
    
    public void OnSelectAction() {
        Debug.Log(gameObject.tag);
    }
    

}
