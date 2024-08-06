using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFightAreaActivator : MonoBehaviour
{
    private BossFightController bossFightController;



    private void Awake()
    {
        this.bossFightController = GetComponentInParent<BossFightController>();
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("Narrator"))
        {
            bossFightController.startFight();
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

}
