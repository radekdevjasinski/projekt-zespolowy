using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpells : MonoBehaviour
{
    public void HealPlayer()
    {
        GameObject.Find("Player").GetComponent<PlayerEntityController>().heal();
    }
}
