using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization;
public class NpcTrader : NPCConversation
{
    [SerializeField]
    private GameObject itemTradeBase;
    [SerializeField] private GameObject soundOnBuy;
    [Serializable]
    public struct itemToTrade
    {
        [SerializeField] public GameObject itemToBeBought;
        [SerializeField] public int basePrice;
        [SerializeField] public LocalizedString[] onBoughtDialog;
    }

    [Serializable]
    struct itemPosition{
        [SerializeField] public Transform itemToTradeLocation;
        [SerializeField] public bool isRandom; //if is Random tehn item seciton is ignored
        [SerializeField] public itemToTrade item;
    }
    [Serializable]
    struct ranadomItem
    {
        [SerializeField] public itemToTrade item;
        [SerializeField] public float procentChanceFactor;
    }

     [SerializeField] private itemPosition[] itemPostions;
    [SerializeField] private ranadomItem[] randomItems;
    [SerializeField] private LocalizedString[] outOfStockDialog;

    private int itemSeeling;


    override protected void Start()
    {
        base.Start();
        resetItems();
    }

    public void resetItems()
    {
        foreach(itemPosition itempos in itemPostions)
        {
            itemSeeling++;
            setupItemPosition(itempos);
        }
    }

    private void setupItemPosition(itemPosition itempos)
    {
        if (itempos.isRandom)
        {
            setItemOnPostion(itempos.itemToTradeLocation, getRandomItem());
        }
        else
        {
            setItemOnPostion(itempos.itemToTradeLocation, itempos.item);
        }
    }

    private itemToTrade getRandomItem()
    {
        float randsum = 0;
        foreach(ranadomItem itempos in randomItems)
        {
            randsum += itempos.procentChanceFactor;
        }
        float procentFactor = UnityEngine.Random.Range(0,randsum);
        //Debug.Log("procentFactor: " + procentFactor);
        float procentCount = 0;
        foreach(ranadomItem itempos in randomItems)
        {
            procentCount += itempos.procentChanceFactor;
            //Debug.Log("procentCount: " + procentCount);
            if (procentCount >= procentFactor)
                return itempos.item;
        }
        return randomItems[randomItems.Length-1].item;

    }

    private void setItemOnPostion(Transform itemToTradeLocation, itemToTrade item)
    {
        //Debug.Log("settign up item in wshop: " + item.itemToBeBought.name);
        removeAllChildren(itemToTradeLocation);
        GameObject gameObject = Instantiate(itemTradeBase, itemToTradeLocation);
        GameObject objSold= gameObject.GetComponent<TradeObject>().setupTrade(item.itemToBeBought, item.basePrice, soundOnBuy, () => { onItemBought(item); });

        IonItemPickUP[] onItmepikc=GetComponents<IonItemPickUP>();
        //Debug.Log("tring add compeonts");
        foreach(IonItemPickUP var in onItmepikc)
        {
            //Debug.Log("Adding component: "+ var.GetType());
            objSold.AddComponent(var.GetType());
        }

    }

    private void removeAllChildren(Transform parent)
    {
        if(parent.childCount>0)
        {
           
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
    }



    public void onItemBought(itemToTrade item)
    {
        itemSeeling--;
        if(item.onBoughtDialog.Length>0)
        setDialogeText(item.onBoughtDialog[UnityEngine.Random.Range(0, item.onBoughtDialog.Length)]);
        //Debug.Log("ITEM Bought: "+ item.itemToBeBought.name + " now have : "+ itemSeeling);
    }


    protected override void StartConversation()
    {
        if(itemSeeling>0)
        base.StartConversation();
        else
            setDialogeText(outOfStockDialog[UnityEngine.Random.Range(0, outOfStockDialog.Length)]);
    }

    internal void removeShop()
    {
        foreach(itemPosition pos in itemPostions)
        {
            Transform[] children=pos.itemToTradeLocation.GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                if(pos.itemToTradeLocation!=child.transform)
                Destroy(child.gameObject);
            }
        }
        itemSeeling = 0;
    }
}
