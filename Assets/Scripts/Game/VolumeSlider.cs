using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public SoundManager soundManager;
    public float sensitivity = 0.5f;
    public Transform leftLimit;
    public Transform rightLimit;

    private bool isPlayerPushing = false;

    private void Start()
    {
        soundManager = SoundManager.instance;
        if (soundManager == null)
        {
            Debug.LogError("SoundManager instance not found");
        }

        float initialPositionX = Mathf.Lerp(leftLimit.position.x, rightLimit.position.x, soundManager.MusicVolume);
        transform.position = new Vector3(initialPositionX, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        if (isPlayerPushing)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.right * horizontalInput * sensitivity * Time.deltaTime);

            if (transform.position.x < leftLimit.position.x)
            {
                transform.position = new Vector3(leftLimit.position.x, transform.position.y, transform.position.z);
            }
            else if (transform.position.x > rightLimit.position.x)
            {
                transform.position = new Vector3(rightLimit.position.x, transform.position.y, transform.position.z);
            }

            float volume = Mathf.InverseLerp(leftLimit.position.x, rightLimit.position.x, transform.position.x);
            soundManager.MusicVolume = volume;
            soundManager.SaveSettings();

            AudioSource[] audioSources = soundManager.GetComponentsInChildren<AudioSource>(true);
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = volume;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerPushing = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerPushing = false;
        }
    }
}
