using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnterableObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        onEnter(other.gameObject);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        onExitEnter(other.gameObject);
    }

    protected abstract void onEnter(GameObject gameobject);
    protected abstract void onExitEnter(GameObject gameobject);
}
