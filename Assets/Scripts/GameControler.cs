using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{
    static private GameControler _instance;
    private GameObject player;


    static public GameControler instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            throw new Exception("There can be only one instance of GameControler");

        }
        player=GameObject.Find("Player");
        if(player==null)
            throw new Exception("couldn find player");
    }


    public void pauseGame()
    {
        Time.timeScale = 0;
        foreach (Ipausable pausable in transform.GetComponentsInChildren<Ipausable>())
        {
            pausable.pause();;
        }
    }


    public void resumeGame()
    {
        Time.timeScale = 1;

        foreach (Ipausable pausable in transform.GetComponentsInChildren<Ipausable>())
        {
            pausable.resume(); ;
        }
    }


    public void pausePlayerControls()
    {
player.GetComponent<PlayerControler>().pause();
    }

    public void resumePlayerControls()
    {
        player.GetComponent<PlayerControler>().resume();
    }
}
