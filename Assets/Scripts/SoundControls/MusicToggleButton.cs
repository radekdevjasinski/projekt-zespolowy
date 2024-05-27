using UnityEngine;

public class MusicToggleButton : MonoBehaviour
{
    public SoundManager soundManager;
    private bool isPlayerOnButton = false;

    private void Start()
    {
        soundManager = SoundManager.instance;
        if (soundManager == null)
        {
            Debug.LogError("SoundManager instance not found");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlayerOnButton)
        {
            ToggleMusic();
            isPlayerOnButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerOnButton = false;
        }
    }

    private void ToggleMusic()
    {
        soundManager.IsMuted = !soundManager.IsMuted;
        soundManager.SaveSettings();

        AudioSource[] audioSources = soundManager.GetComponentsInChildren<AudioSource>(true);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = soundManager.IsMuted;
        }
    }
}
