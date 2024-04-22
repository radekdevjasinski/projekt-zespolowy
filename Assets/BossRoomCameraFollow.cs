using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomCameraFollow : MonoBehaviour
{
    public GameObject cameraHolder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerTeleporter playerTeleporter = collision.GetComponent<PlayerTeleporter>();

            if (playerTeleporter != null && playerTeleporter.activePlayerRoom != null && playerTeleporter.activePlayerRoom.roomType == RoomType.BOSSROOM)
            {
                cameraHolder.GetComponent<CameraFollowRoom>().enabled = false;

                cameraHolder.transform.Find("BossRoomCamera").gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerTeleporter playerTeleporter = other.GetComponent<PlayerTeleporter>();
            if (playerTeleporter != null && playerTeleporter.activePlayerRoom != null && playerTeleporter.activePlayerRoom.roomType == RoomType.BOSSROOM)
            {
                cameraHolder.GetComponent<CameraFollowRoom>().enabled = true;
               
                cameraHolder.transform.Find("BossRoomCamera").gameObject.SetActive(false);
            }
        }
    }

}
