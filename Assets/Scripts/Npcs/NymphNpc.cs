using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class NymphNpc : NPCConversation
{
    [SerializeField] private LocalizedString firstDialog;

    [SerializeField]
    private ItemSummon itemSummon;
    private bool first = true;


    protected override void StartConversation()
    {
        if (!first)
        {
            base.StartConversation();
        }
        else
        {
            setDialogeText(firstDialog);
            itemSummon.summon();
            first=false;
        }
       
    }
}
