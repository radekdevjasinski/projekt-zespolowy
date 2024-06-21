using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScreenScript : MonoBehaviour
{
    public void Setup()
    {
        GameControler.instance.pausePlayerControls();
    }
}
