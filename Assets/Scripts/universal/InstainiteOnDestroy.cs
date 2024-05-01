using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InstainiteOnDestroy : MonoBehaviour
{
    [SerializeField] private GameObject instance;


    public void setObjectToInstance(GameObject obj)
    {
        instance = obj;
    }

    private void OnDestroy()
    {
        Instantiate(instance, transform.position, transform.rotation, transform.parent);
    }

}
