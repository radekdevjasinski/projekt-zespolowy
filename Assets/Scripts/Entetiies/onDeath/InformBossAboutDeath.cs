using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformBossAboutDeath : MonoBehaviour, DeathSeuance
{

    private MinionBoss minionBoss;

    public void setMinionBoss(MinionBoss minionBoss)
    {
        this.minionBoss=minionBoss;
    }


    public void onDeath()
    {
        Debug.Log("trying informing boss aout death");
        if (minionBoss != null)
        {
            Debug.Log("informing boss aout death");
            minionBoss.onMinonDeath();
        }
    }
}
