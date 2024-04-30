using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformShootweAboutDeath : MonoBehaviour
{
    [SerializeField]
    private SlingshotController slingshotController;

    public void setSlingShotContrler(SlingshotController slingshotController)
    {
        this.slingshotController = slingshotController;
    }

    public void OnDestroy()
    {
        slingshotController.decreaseNumberOfAliveProjetile();
    }
}
