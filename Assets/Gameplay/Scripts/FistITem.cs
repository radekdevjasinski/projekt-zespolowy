using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistITem : ActiveItemBase
{

    public override void use(GameObject user)
    {
        //Debug.Log("Fist");
        Transform _trasnform = user.GetComponent<Transform>();
         Vector2 force=  new Vector3(-_trasnform.right.y, _trasnform.right.x, 0)*40;
        user.GetComponent<Rigidbody2D>().AddForce(force,ForceMode2D.Impulse);
         //Debug.Log("\\Fist: " + _trasnform.position);
    }
}
