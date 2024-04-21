using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DoorState
{
    OPENED,
    CLOSED,
    HIDDEN
}
public class Door : MonoBehaviour
{
    public Sprite doorSprite;
    public DoorState doorState = DoorState.OPENED;
    public void OpenDoor()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.black;
        GetComponent<SpriteRenderer>().sprite = doorSprite;
        doorState = DoorState.OPENED;
    }
    public void CloseDoor()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().sprite = doorSprite;
        doorState = DoorState.CLOSED;


    }
    public void HideDoor()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = null;
        doorState = DoorState.HIDDEN;
    }
}
