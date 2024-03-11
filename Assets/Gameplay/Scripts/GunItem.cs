using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : ActiveItemBase
{
    // Start is called before the first frame update

    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed=20;
    public override void use(GameObject user)
    {
        Debug.Log("Gun: "+user.transform.right);
        Debug.Log("bound: " + user.GetComponent<Collider2D>().bounds.max.y);
        Vector3 forward=new Vector3(-user.transform.right.y, user.transform.right.x,0);
        float forwardSize = user.GetComponent<Collider2D>().bounds.size.x;
       GameObject project= Instantiate(projectile, user.transform.position+ forward * forwardSize, user.transform.rotation);
       project.GetComponent<Rigidbody2D>().AddForce(forward.normalized*speed,ForceMode2D.Impulse);
       Debug.Log("seppd: " + project.GetComponent<Rigidbody2D>().velocity);
       Destroy(project,2);
    }
}
