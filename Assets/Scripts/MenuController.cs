using JSONConverters;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int newGameScene = 1;
    void Start()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        if (File.Exists(saveFilePath))
        {
            GameObject.Find("Continue").SetActive(false);
        }
    }
    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
        PlayerPrefs.SetInt("Loaded", 0);

    }
    public void Continue()
    {
        SceneManager.LoadScene(newGameScene);
        PlayerPrefs.SetInt("Loaded", 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}


