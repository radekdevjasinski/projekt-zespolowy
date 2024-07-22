using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    private PlayerMovementController player;
    private AudioSource audioSource;
    private float pitch;
    public float volume = 0.05f;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovementController>();
        audioSource = GetComponent<AudioSource>();
        pitch = audioSource.pitch;

    }
    void Update()
    {
        if (player.getMoveValue().magnitude > 0)
        {
            audioSource.volume = volume;
            float newPitch = Random.Range(pitch - 0.2f, pitch + 0.2f);
            audioSource.pitch = newPitch;


        }
        else
        {
            audioSource.volume = 0;
        }
    }
}
