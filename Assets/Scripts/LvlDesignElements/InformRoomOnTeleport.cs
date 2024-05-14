using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformRoomOnTeleport : MonoBehaviour, IOnTeleport
{
    public void onTeleport()
    {
        transform.parent.parent.parent.GetComponent<Room>().onEntry();
    }


}
