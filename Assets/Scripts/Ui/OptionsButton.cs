using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject muteButton;

    private bool isSoundEnabled = true;

    private void Start()
    {
        exitButton.SetActive(false);
        muteButton.SetActive(false);
    }

    public void OptionsMenu()
    {
        exitButton.SetActive(true);
        muteButton.SetActive(true);
    }

    private void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;

        if (isSoundEnabled)
        {
            AudioListener.volume = 1f;
        }
        else
        {
            AudioListener.volume = 0f;
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
