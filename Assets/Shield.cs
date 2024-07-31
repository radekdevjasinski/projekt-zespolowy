using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool shieldActive = true;
    private Animator animator;
    private ZombieShield zombieShield;
    private void Start()
    {
        animator = GetComponent<Animator>();
        zombieShield = GetComponentInParent<ZombieShield>();
    }
    public void shieldGlow()
    {
        if (shieldActive)
        {
            animator.SetTrigger("Glow");
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            zombieShield.ShieldBash();

        }
    }

}
