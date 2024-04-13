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
        if(minionBoss!= null) 
         minionBoss.onMinonDeath();
    }
}
