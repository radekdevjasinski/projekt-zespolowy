using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    [SerializeField] GameObject soundOnDamage;
    public Color damageColor = Color.red;
    public float flashDuration = 0.1f;

    private Color originalColor;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage()
    {
        if(soundOnDamage != null)
        {
            SoundManager.instance.playSound(this.transform, this.soundOnDamage, this.transform.position);
        }
        StartCoroutine(FlashSprite());
    }

    private IEnumerator FlashSprite()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}
