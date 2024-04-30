using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterStop : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    [SerializeField]private float timeToDestroyAfterStop = 5;
    [SerializeField] private float velLimit=0.5f;
    bool isSetTOdestroy=false;
    public void setTime(float time)
    {
        timeToDestroyAfterStop=time;
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!isSetTOdestroy && rigidbody.velocity.magnitude <= velLimit)
        {
            Debug.Log("set to destroy");
            Invoke("destroy", timeToDestroyAfterStop);
            isSetTOdestroy=true;
        }
    }

    private void destroy()
    {
        Destroy(gameObject, timeToDestroyAfterStop);
    }
}
