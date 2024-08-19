using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject endingMenuUI;
    public int menuScene = 0;

    public GameObject tutorialScreen;
    public GameObject tutorialButtons;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Menu.PauseMenu.performed += ctx => TogglePause();    
    }
    private void Start()
    {
        GameControler.instance.pausePlayerControls();
    }
    private void Update()
    {
       // Debug.Log(GameIsPaused);
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
        //Time.timeScale = 1f;
        GameControler.instance.resumeGame();
        DisablePauseGame();
    }
    public void EndingScreenResumeGame()
    {
        endingMenuUI.SetActive(false);
        GameControler.instance.resumePlayerControls();
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        GameControler.instance.pauseGame();
        EnablePauseGame();
    }
    public void EndTutorial()
    {
        tutorialScreen.SetActive(false);
        tutorialButtons.SetActive(false);
        GameControler.instance.resumePlayerControls();
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
