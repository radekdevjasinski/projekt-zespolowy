using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [Header("Attacking")]
    public float knockbackForceAttacking = 5f;
    public float knockbackDurationAttacking = 0.1f;
    [Header("Attacked")]
    public float knockbackForceAttacked = 5f;
    public float knockbackDurationAttacked = 0.1f;
    private float currentKnockbackForce;

    private void Awake()
    {
        currentKnockbackForce = knockbackForceAttacked;
    }
    public void ApplyKnockback(Vector2 direction, float knockbackForce, float knockbackDuration)
    {
        StartCoroutine(DoKnockback(direction, knockbackForce, knockbackDuration));
    }

    private IEnumerator DoKnockback(Vector2 direction, float knockbackForce, float knockbackDuration)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            currentKnockbackForce = knockbackForce;
            currentKnockbackForce *= rb.mass;
            rb.AddForce(direction * currentKnockbackForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(knockbackDuration);
            rb.velocity = Vector2.zero;
        }
    }
}
