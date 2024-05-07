using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuControl : MonoBehaviour
{
    public CinemachineVirtualCamera CinemachineCamera;


    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Exit"))
        {
            Debug.Log("Game closed");
            Application.Quit();
        }
        else if (collision.CompareTag("Start"))
        {
            SceneManager.LoadScene(1);
        }
        else if (collision.CompareTag("Options"))
        {
            TeleportTo(44.3f);
        }
        else if (collision.CompareTag("Stats"))
        {
            TeleportTo(-44.3f);
        }
        else if (collision.CompareTag("OMenu"))
        {
            TeleportTo(6.9f);
        }
        else if (collision.CompareTag("SMenu"))
        {
            TeleportTo(-6.9f);
        }
    }
    private void TeleportTo(float xPosition)
    {
        transform.position = new Vector2(xPosition, transform.position.y);
    }
}


