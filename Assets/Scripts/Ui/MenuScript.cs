using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public int newGameScene = 1;
    public GameObject creditsPanel;
    private bool creditsActive = false; 

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void ToggleCredits()
    {
        creditsActive = !creditsActive;
        creditsPanel.SetActive(creditsActive);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
