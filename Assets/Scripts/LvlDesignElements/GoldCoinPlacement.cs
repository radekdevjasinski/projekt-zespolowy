using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinPlacement : ActivatableAreaObject
{
    [SerializeField] private GameObject objectToActivate;

    public override bool activate(GameObject player)
    {
        PlayerItemsController playerItemsController;
        if (player.TryGetComponent(out playerItemsController) && playerItemsController.hasGoldenCoin())
        {
            objectToActivate.GetComponent<IActivatableObject>().activate(this.gameObject);
            this.GetComponent<Animator>().SetBool("active",true);
            playerItemsController.removeGoldCoin();
            return true;
        }

        return false;
    }
}
