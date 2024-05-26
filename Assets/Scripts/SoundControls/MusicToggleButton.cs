using UnityEngine;

public class MusicToggleButton : MonoBehaviour
{
    public SoundManager soundManager;
    private bool isPlayerOnButton = false;

    private void Start()
    {
        soundManager = GameObject.Find("Sounds").GetComponent<SoundManager>();
        if (soundManager == null)
        {
            Debug.LogError("SoundManager not found on object 'Sounds'");
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
        AudioSource[] audioSources = soundManager.GetComponentsInChildren<AudioSource>(true);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = !audioSource.mute;
        }
    }
}
