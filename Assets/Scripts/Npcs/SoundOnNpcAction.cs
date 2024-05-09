using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnNpcAction : MonoBehaviour, IonPerformAction
{
    [SerializeField] GameObject sound;

    public void onPerformAction()
    {
        SoundManager.instance.playSound( sound, transform.position);
    }
}
