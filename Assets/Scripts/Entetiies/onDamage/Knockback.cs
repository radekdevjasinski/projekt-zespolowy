using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackDuration = 0.1f;
    private float currentKnockbackForce;

    private void Awake()
    {
        currentKnockbackForce = knockbackForce;
    }

    private void ResetForce()
    {
        currentKnockbackForce = knockbackForce;
    }


    public void ApplyKnockback(Vector2 direction)
    {
        StartCoroutine(DoKnockback(direction));
    }

    private IEnumerator DoKnockback(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            ResetForce();
            currentKnockbackForce *= rb.mass;
            rb.AddForce(direction * currentKnockbackForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(knockbackDuration);
            rb.velocity = Vector2.zero;
        }
    }
}
