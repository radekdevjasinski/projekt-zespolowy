using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    public GameObject gameOverScreen;
    public int menuScene = 0;

    public void Setup()
    {
        gameOverScreen.SetActive(true);
    }
    public void RestartButton()
    {
        ResetPauseState();
        Debug.Log("Restarting the scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMenu()
    {
        ResetPauseState();
        SceneManager.LoadScene(menuScene);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    private void ResetPauseState()
    {
        PauseMenuController.GameIsPaused = false;
        Time.timeScale = 1;
        Debug.Log("Game is unpaused.");
    }
}
