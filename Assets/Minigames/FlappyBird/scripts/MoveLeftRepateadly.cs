using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRepateadly : MoveConstantLet
{
    Collider2D collider2D;
    [SerializeField]
    private float sectionWidth=10.6f;
    [SerializeField]
    private float start = 0f;
    void Start()
    {
        collider2D=GetComponent<Collider2D>();
        runColumnChnge();
        //transform.position = new Vector2(start, transform.position.y);
    }

    // Update is called once per frame
     override protected void Update()
    {
        base.Update();
        if (-sectionWidth > transform.position.x)
        {
            runColumnChnge();
            transform.position = new Vector2(sectionWidth, transform.position.y);
            
        }
        //Debug.Log("-(collider2D.bounds.size.x / 4 ): " + -(collider2D.bounds.size.x / 4) + " transform.position.x:" + transform.position.x);
    }


    void runColumnChnge()
    {
        foreach (IonRestColumn columnReset in GetComponents<IonRestColumn>())
        {
            columnReset.onCOlumnChange();
        }
    }
}
