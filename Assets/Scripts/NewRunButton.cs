using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewRunButton : MonoBehaviour
{
    public int sceneIndexToRestart = 1; 
    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneIndexToRestart);
    }
}


