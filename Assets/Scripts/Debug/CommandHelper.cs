using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAttributesController;

public class CommandHelper : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerControler playerControler;
    void Start()
    {
    playerControler = GameObject.Find("Player").GetComponent<PlayerControler>();;
    }


    public PlayerControler getPlayer()
    {
        return playerControler;
    }

    internal string setAttribute(float val, attributes attrib)
    {
        if (val < 0)
        {
            return "value should be bigger or equal to zero";
        }

        PlayerAttributesController playerAttributes = playerControler.GetComponent<PlayerAttributesController>();
        playerAttributes.setAttrib(attrib, val);
        return "attribute " + attrib + " set to " + playerAttributes.getAtrib(attrib);

    }
}
