using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;

public class TradeObject : MonoBehaviour
{
    private GameObject objectToBeBought;
    private TMP_Text textMesh;

    public void Awake()
    {
        Transform PriceObject = transform.Find("CanvasPrice").Find("Price");
        if(PriceObject == null) {
            Debug.LogError("couldnt find Price");
            throw new System.Exception(" there shoudl be Price object");

        }
        this.textMesh = PriceObject.GetComponent<TMP_Text>(); 
        if (this.textMesh == null)
        {
            Debug.LogError("couldnt find textMeshCompoentn");
            throw new System.Exception(" there shoudl be text hadnler in object");
        }
    }

    public void setupTrade(GameObject objectToTrade,int price)
    {
      
    }

    internal void setupTrade(GameObject objectToTrade, int basePrice, GameObject soundOnBuy, Action fucntion)
    {
        objectToBeBought = Instantiate(objectToTrade, this.transform);
        objectToBeBought.AddComponent<DestroyParentOnPick>();
        objectToBeBought.AddComponent<MoneyPickUpValiation>().setMoneyRequired(basePrice);
        objectToBeBought.AddComponent<RemoveMoneyOnPickUp>().setMoneyToRemove(basePrice);
        objectToBeBought.AddComponent<PlaySoundOnPickUP>().setAudio(soundOnBuy);
        objectToBeBought.AddComponent<RunFunctionOnPick>().addFunction(fucntion);
        textMesh.SetText(basePrice + " g");
    }
}
