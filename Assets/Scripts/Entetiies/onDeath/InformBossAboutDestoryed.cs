using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformBossAboutDestoyed : MonoBehaviour
{

    private MinionBoss minionBoss;

    public void setMinionBoss(MinionBoss minionBoss)
    {
        this.minionBoss=minionBoss;
    }


    public void OnDestroy()
    {
        //Debug.Log("trying informing boss aout death");
        if (minionBoss != null)
        {
            Debug.Log("informing boss aout death");
            minionBoss.onMinonDeath();
        }
    }
}
