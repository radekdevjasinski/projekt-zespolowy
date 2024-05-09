using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteFlashOnDamage : MonoBehaviour, OnDamage
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Color damageColor = Color.red;
    [SerializeField]
    private float flashDuration = 0.1f;

    private Color originalColor;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        originalColor = spriteRenderer.color;
    }


    public void onDamage()
    {
        StartCoroutine(FlashSprite());
    }
    private IEnumerator FlashSprite()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}
