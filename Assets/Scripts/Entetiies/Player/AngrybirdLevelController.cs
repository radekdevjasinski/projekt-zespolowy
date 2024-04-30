using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngrybirdLevelController : MonoBehaviour
{
    public void lostGame()
    {
        Debug.Log("lost");
        GameObject gameOverScreen = GameObject.Find("GameOver");
        gameOverScreen.GetComponent<GameOverScreen>().Setup();
    }
}
