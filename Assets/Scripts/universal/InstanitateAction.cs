using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanitateAction : MonoBehaviour
{

    [SerializeField]
    private GameObject objectToInstanaite;
    [SerializeField]
    Transform instaniteParent;


    public void doAction()
    {
        Instantiate(objectToInstanaite,instaniteParent);
    }
}
