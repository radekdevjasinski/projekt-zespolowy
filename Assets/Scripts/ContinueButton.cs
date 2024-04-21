using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    public int sceneIndexToContinue = 1; 
    public void ContinueGame()
    {
        SceneManager.LoadScene(sceneIndexToContinue);
    }
}
