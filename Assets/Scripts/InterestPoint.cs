using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterestPoint : MonoBehaviour
{

    public ItemPoint itemPoint;

    private void Awake() {
        // gameObject.tag = "InterestPoint";
        // Debug.Log($"[InterestPoint.Awake]");
    }

    public void OnPickAction() {
        
        Debug.Log($"[InterestPoint.OnClickAction] itemPoint id: {itemPoint.id}");

    }


}
