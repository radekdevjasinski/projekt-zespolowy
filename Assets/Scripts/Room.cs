using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] Door leftDoor;
    [SerializeField] Door rightDoor;
    [SerializeField] Door topDoor;
    [SerializeField] Door bottomDoor;


    private Vector2Int roomPositon;

    bool alreadyEntered=false;


    public Vector2Int RoomIndex { get; set; }

    virtual public void  onEntry()
    {
        DungeonGenerator.instance.onRoomEnter(this.roomPositon);
        if (!alreadyEntered)
        {
            //Debug.Log("first Entry");
            onFirstEntry();


        }
        alreadyEntered=true;
        GameControler.instance.pausePlayerControls();
        StartCoroutine(wakeUpRoutine(DungeonGenerator.instance.getControlsOFTime()));
    }

    virtual public void onFirstEntry()
    {
        foreach (IOnFirstEntryInRoom var in GetComponents<IOnFirstEntryInRoom>())
        {
            var.onFirstEntry(roomPositon);
        }
       

    }


    protected virtual IEnumerator wakeUpRoutine(float controlsOfTime)
    {
        yield return new WaitForSeconds(controlsOfTime);
        GameControler.instance.resumePlayerControls();
    }

    internal void setupNewRoom(DungeonRoom[] roomsAround, Vector2Int positon)
    {
        roomPositon = positon;
        if (roomsAround[0] != null)
        {
            leftDoor.validateDoor();
            leftDoor.openDoor();
            leftDoor.setTeleportLocation(roomsAround[0].gameObject.GetComponent<Room>().rightDoor.getTeleport());
        }
        if (roomsAround[1] != null)
        {
            topDoor.validateDoor();
            topDoor.openDoor();
            topDoor.setTeleportLocation(roomsAround[1].gameObject.GetComponent<Room>().bottomDoor.getTeleport());

        }
        if (roomsAround[2] != null)
        {
            
            rightDoor.validateDoor();
            rightDoor.openDoor();
            rightDoor.setTeleportLocation(roomsAround[2].gameObject.GetComponent<Room>().leftDoor.getTeleport());

        }
        if (roomsAround[3] != null)
        {
            bottomDoor.validateDoor();
            bottomDoor.openDoor();
            bottomDoor.setTeleportLocation(roomsAround[3].gameObject.GetComponent<Room>().topDoor.getTeleport());

        }

    }

    public void closeAllDorrs()
    {
         leftDoor.closeDoor();
        rightDoor.closeDoor();
       topDoor.closeDoor();
        bottomDoor.closeDoor();

    }

    public void openAllDoors()
    {
        Debug.Log("OPening doots");
        leftDoor.openDoor();
        rightDoor.openDoor();
        topDoor.openDoor();
        bottomDoor.openDoor();

    }
}
