using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room{
    override public Transform getCameraFollowPoint()
    {
        return GameControler.instance.getPlayer();
    }
}
