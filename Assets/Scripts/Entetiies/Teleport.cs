using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Teleport secondTeleport;
    [SerializeField] private float offset=5;

    private Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
       
    }

    public bool isActve()
    {
        return collider.gameObject.active;
    }

    public void setIsActive(bool state)
    {
        collider.gameObject.SetActive(state);
    }

    public void teleport(GameObject obj)
    {
        if (secondTeleport != null)
        {
            Vector3 offsetVector = secondTeleport.transform.right;
            offsetVector = new Vector3(offsetVector.y, -offsetVector.x, 0);

            //Debug.Log("second teleport positon: " + secondTeleport.transform.position);
            //Debug.Log("second local teleport positon: " + secondTeleport.transform.localPosition);
            obj.transform.position = secondTeleport.transform.position + offsetVector * offset;
            //Debug.Log("telpeort postion : " + secondTeleport.transform.position);

            //Debug.Log("OFfset vector: " + offsetVector * offset);
            secondTeleport.onTeleport();
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
             //Debug.Log("Teleprot to "+ secondTeleport);

        if (collision.gameObject.CompareTag("Player") && isActve())
        {
            teleport(collision.gameObject);

            //GameObject.Find("Player").GetComponent<PlayerTeleporter>().Teleport(direction);
        }
    }

    private void onTeleport()
    {
        foreach (IOnTeleport var in GetComponents<IOnTeleport>())
        {
            var.onTeleport();
        }
    }

    public void setTeleport(Teleport teleport)
    {

        Debug.Log("seting teleport: "+ teleport.name+", "+teleport.transform.parent.name+", "+ teleport.transform.parent.transform.parent.name + ", " + teleport.transform.parent.transform.parent.transform.parent.GetComponent<Room>().transform.position);
      secondTeleport=teleport;;
    }
}
