using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRidOfCurrentParent : MonoBehaviour
{

    void Start()
    {
        foreach (Transform tr in GetComponentsInChildren<Transform>())
        {
            if(tr.parent==this.transform)
                tr.SetParent(this.transform.parent);
        }
        Destroy(this.gameObject);
    }


}
