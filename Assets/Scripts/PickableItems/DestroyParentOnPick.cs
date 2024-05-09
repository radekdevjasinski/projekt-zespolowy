using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentOnPick : MonoBehaviour, IonItemPickUP
{
    public void onItemPicked(GameObject gameobject)
    {
        Debug.Log("-----destroying parent");
        Destroy(transform.parent.gameObject);
    }
}
