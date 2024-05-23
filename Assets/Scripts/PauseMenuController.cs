using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public int menuScene = 0;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.PauseMenu.performed += ctx => TogglePause();    
    }
    private void Update()
    {
        Debug.Log(GameIsPaused);
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void TogglePause()
    {
        if (GameIsPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void ResumeGame()
    {   
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        DisablePauseGame();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        EnablePauseGame();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        EnablePauseGame();
        SceneManager.LoadScene(menuScene);
    }
    public void EnablePauseGame()
    {
        GameIsPaused = true;
    }
    public void DisablePauseGame()
    {
        GameIsPaused = false;
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
