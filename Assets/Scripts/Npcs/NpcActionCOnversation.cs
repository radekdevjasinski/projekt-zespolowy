using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class NpcActionCOnversation : NPCConversation
{
    // Start is called before the first frame update

    //GetComponent<NPCSpells>().HealPlayer();
    // Update is called once per frame

    [SerializeField]
    private int possibleActions = 1;
    [SerializeField]
    private LocalizedString[] failedActionDialogues;
    [SerializeField]
    private LocalizedString[] outOfActionsDialogues;
    protected override void StartConversation()
    {
        if (possibleActions > 0)
        {
            if (perefromAction())
            {
                base.StartConversation();
                possibleActions--;
            }
            else
            {
                failedActionConversation();

            }
        }
        else
        {
            outOfActionConversation();
        }
    }


    private bool perefromAction()
    {
      
        InpcAction npcACtion=GetComponent<InpcAction>();
        if (npcACtion == null) return false;

            return npcACtion.perfromAction();

    }
    protected void failedActionConversation()
    {
        setDialogeText(failedActionDialogues[Random.Range(0, failedActionDialogues.Length)]);

    }
    protected void outOfActionConversation()
    {
        setDialogeText(outOfActionsDialogues[Random.Range(0, outOfActionsDialogues.Length)]);

    }
}
