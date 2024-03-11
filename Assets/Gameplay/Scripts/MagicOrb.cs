using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrb : ActiveItemBase
{
    // Start is called before the first frame update
    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed = 20;
    [SerializeField] private float radius = 3;
    [SerializeField] private int amount = 10;
    public override void use(GameObject user)
    {
        Debug.Log("Orb: " + user.transform.right);
        Debug.Log("bound: " + user.GetComponent<Collider2D>().bounds.max.y);
        for (float i = 0; i < 360; i+=360/5)
        {
            float rad = Mathf.Deg2Rad*i;
            Vector3 newPos= new Vector2(radius*Mathf.Cos(rad), radius*Mathf.Sin(rad));

            GameObject project = Instantiate(projectile, user.transform.position + newPos, user.transform.rotation);
            project.GetComponent<Rigidbody2D>().AddForce(newPos.normalized * speed, ForceMode2D.Impulse);
            Debug.Log("seppd: " + project.GetComponent<Rigidbody2D>().velocity);
            Destroy(project, 2);

        }

    }
}
