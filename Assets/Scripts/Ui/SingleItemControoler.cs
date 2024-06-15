using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleItemControoler : MonoBehaviour
{
    [SerializeField] private Transform ItemObject;

   

    public void setImage(Sprite sprite)
    {
        if (this.ItemObject != null)
        {
            ItemObject.gameObject.active=true;
            ItemObject.GetComponent<Image>().sprite=sprite;
        }
    }

    public void clearContainer()
    {
        if (this.ItemObject != null)
        {
            ItemObject.gameObject.active = false;
            ItemObject.GetComponent<Image>().sprite = null;
        }
    }

}
