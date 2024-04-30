using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlingShototerActionContrrller : MonoBehaviour
{
    [SerializeField] private float shootingCooldown = 1;
    [SerializeField] private float forceMultiplayer = 10;
    [SerializeField] private float slingShotLengthLimit= 1;
    [SerializeField] private float clickiLimit = 3;

    Vector2 worldMousePositon;
    Vector2 restPostion;
    LineRenderer lineRenderer;
    private bool isHolding=false;
    private bool isReloading = true;
    private SlingshotController slingshotController;
    private void Awake()
    {
        lineRenderer=GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1, transform.position);

        lineRenderer.enabled = false;
        restPostion = transform.parent.position;
        slingshotController=GetComponentInParent<SlingshotController>();
    }

    public void setWorldMousePostion(Vector2 pos)
    {
        this.worldMousePositon = pos;
        updateHadnleMove();
    }
    public void setIsNotHolding()
    {
        isHolding = false;
        lineRenderer.enabled = false;
    }


    public void setIsHolding()
    {
        if ((worldMousePositon - new Vector2(transform.position.x, transform.position.y)).magnitude < clickiLimit && !this.isReloading)
        {
            Debug.Log("activate");
            isHolding = true;
            lineRenderer.enabled = true;
        }
    }

    public void resetPostion()
    {
        setIsNotHolding();

        transform.localPosition = new Vector3(0,0,0);
    }

    private void updateHadnleMove()
    {
        if (isHolding)
        {
            Vector2 newPos= worldMousePositon;
            Debug.Log("worldMousePositon: "+worldMousePositon);
            Debug.Log("ransform.localPosition: " + transform.parent.position);

            if ((worldMousePositon - restPostion).magnitude > slingShotLengthLimit)
            {
                newPos = (worldMousePositon - restPostion).normalized * slingShotLengthLimit + restPostion;
            }
            else
            {
                newPos = worldMousePositon;
            }
            lineRenderer.SetPosition(1, newPos);

            transform.position = newPos;
        }
    }

    public void shoot()
    {
        if (isHolding)
        {
            foreach (Rigidbody2D rb in GetComponentsInChildren<Rigidbody2D>())
            {
                rb.simulated = true;
                rb.WakeUp();
                rb.isKinematic = false;
                rb.AddForce(new Vector2(-transform.localPosition.x, -transform.localPosition.y) * forceMultiplayer,
                    ForceMode2D.Impulse);
                rb.AddComponent<DestroyAfterStop>();
            }

            transform.DetachChildren();
            resetPostion();
            slingshotController.reload();
        }
    }

    public void setIsReloadin(bool b)
    {
        this.isReloading = b;
    }
}
