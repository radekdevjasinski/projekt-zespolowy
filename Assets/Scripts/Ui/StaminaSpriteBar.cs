using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaSpriteBar : MonoBehaviour
{
    public Sprite fullStaminaSprite;
    public Sprite halfStaminaSprite;

    public void SetStamina(float stamina, float maxStamina)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        int numStaminas = Mathf.CeilToInt((stamina / maxStamina) * (maxStamina * 2));

        int numFullStaminas = numStaminas / 2;
        int numHalfStaminas = numStaminas % 2;

        for (int i = 0; i < numFullStaminas; i++)
        {
            Image StaminaImage = new GameObject("FullStamina" + i).AddComponent<Image>();
            StaminaImage.sprite = fullStaminaSprite;
            StaminaImage.transform.SetParent(transform, false);
        }
        if (numHalfStaminas > 0)
        {
            Image halfStaminaImage = new GameObject("HalfStamina").AddComponent<Image>();
            halfStaminaImage.sprite = halfStaminaSprite;
            halfStaminaImage.transform.SetParent(transform, false);
        }
    }
}
