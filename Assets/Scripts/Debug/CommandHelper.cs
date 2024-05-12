using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
