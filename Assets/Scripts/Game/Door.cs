using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DoorState
{
    OPENED,
    CLOSED,
    HIDDEN
}
public class Door : MonoBehaviour
{
    public Sprite doorSprite;
    private Teleport teleport;



    [SerializeField] private GameObject soundOnOpen;
    [SerializeField] private GameObject soundOnclose;

    [SerializeField] private bool openOnstar=false;
    private Collider2D col;
    private SpriteRenderer renderer;

    private bool _valid;
    private bool _isOpened;



    public bool isValid()
    {
        return _valid;
    }
    public void invalidateDoor()
    {
        _valid = false;
        col.enabled = true;
        renderer.enabled = false;
    }

    public void validateDoor()
    {
        _valid=true;
        col.enabled = true;
        renderer.enabled = true;
    }

   
    public void openDoor()
    {
        //Debug.Log("trying top open door: "+ isValid());
        if (isValid())
        {
            //Debug.Log("opening door");
            col.enabled = false;
            renderer.color = Color.black;
            if (soundOnOpen != null && SoundManager.instance!=null)
                SoundManager.instance.playSound(transform, soundOnOpen);
        }
    }
    public void closeDoor()
    {
        if (isValid())
        {
            col.enabled = true;
            renderer.color = Color.white;
            if (soundOnclose != null)
                SoundManager.instance.playSound(transform, soundOnclose);
        }

    }

    public void setTeleportLocation(Teleport teleport)
    {
        this.teleport.setTeleport(teleport);
    }

   public Teleport getTeleport()
    {
        return teleport; 
    }

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        renderer= GetComponent<SpriteRenderer>();
        invalidateDoor();
        teleport = transform.GetComponentInChildren<Teleport>();
        if (openOnstar)
        {
            validateDoor();
            openDoor();;
        }
    }


   
}
