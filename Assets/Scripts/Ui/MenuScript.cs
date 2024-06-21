using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public int newGameScene = 1;
    public GameObject creditsPanel;
    public GameObject menuPanel;
    public GameObject title;
    public GameObject back;
    private bool creditsActive = false; 

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void ToggleCredits()
    {
        creditsActive = !creditsActive;
        creditsPanel.SetActive(creditsActive);
        menuPanel.SetActive(!creditsActive);
        title.SetActive(!creditsActive);
        back.SetActive(creditsActive);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
