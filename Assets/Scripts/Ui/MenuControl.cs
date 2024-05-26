using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public CinemachineVirtualCamera CinemachineCamera;

    private void Start()
    {
        CinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
        ChangeConfiner("MenuRoom");
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
            ChangeConfiner("OptionsRoom");
        }
        else if (collision.CompareTag("Stats"))
        {
            TeleportTo(-44.3f);
            ChangeConfiner("StatsRoom");
        }
        else if (collision.CompareTag("OMenu"))
        {
            TeleportTo(5.5f);
            ChangeConfiner("MenuRoom");
        }
        else if (collision.CompareTag("SMenu"))
        {
            TeleportTo(-7.15f);
            ChangeConfiner("MenuRoom");
        }
    }

    private void TeleportTo(float xPosition)
    {
        transform.position = new Vector2(xPosition, 0.56f);
    }

    private void ChangeConfiner(string roomName)
    {
        GameObject room = GameObject.Find(roomName);
        if (room != null)
        {
            PolygonCollider2D roomCollider = room.GetComponentInChildren<PolygonCollider2D>();
            if (roomCollider != null)
            {
                CinemachineCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = roomCollider;
            }
            else
            {
                Debug.LogError("PolygonCollider2D not found in room: " + roomName);
            }
        }
        else
        {
            Debug.LogError("Room not found: " + roomName);
        }
    }
}
