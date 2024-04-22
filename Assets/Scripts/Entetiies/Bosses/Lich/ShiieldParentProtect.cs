using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiieldParentProtect : MonoBehaviour
{
    protected void Start()
    {
        this.GetComponentInParent<InvurnabilityControl>().setIsInvurnable(true);
    }

    protected void OnDestroy()
    {
        this.GetComponentInParent<InvurnabilityControl>().setIsInvurnable(false);
    }


}
