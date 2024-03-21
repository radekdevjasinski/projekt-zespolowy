using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBlasting : MonoBehaviour
{
    public GameObject blastPrefab;
    void Blast()
    {
        GameObject blast = GameObject.Instantiate(blastPrefab);
        blast.transform.parent = this.gameObject.transform;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        blast.transform.position = new Vector3(mousePos.x, mousePos.y, -5);
        blast.GetComponent<ParticleSystem>().Play();
    }
    private void OnLeftClick()
    {
        Blast();
    }
    private void OnRightClick()
    {
        Blast();
    }

}
