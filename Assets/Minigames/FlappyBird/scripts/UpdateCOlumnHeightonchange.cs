using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCOlumnHeightonchange : MonoBehaviour, IonRestColumn
{
    [SerializeField]
    private float heightChangeRange=6;

    public void onCOlumnChange()
    {
        transform.position= new Vector2 (transform.position.x,Random.Range(-heightChangeRange, heightChangeRange));
    }
}
