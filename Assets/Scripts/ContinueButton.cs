using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    public int continueScene = 1; 
    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }
}
