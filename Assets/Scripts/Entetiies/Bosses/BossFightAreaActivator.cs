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
        if (collision.CompareTag("Player"))
        {
            bossFightController.startFight();
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

}
