using UnityEngine;

public class GameVolumeSlider : MonoBehaviour
{
    public float sensitivity = 0.5f;
    public float setVolume = 0.5f;
    public Transform leftLimit;
    public Transform rightLimit;

    private bool isPlayerPushing = false;

    private void Start()
    {

        float initialPositionX = Mathf.Lerp(leftLimit.position.x, rightLimit.position.x, setVolume);
        transform.position = new Vector3(initialPositionX, transform.position.y, transform.position.z);

        AudioListener.volume = setVolume;
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

            AudioListener.volume = volume;
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
