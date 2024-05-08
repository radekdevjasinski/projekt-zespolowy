using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private PlayerAttributesController playerAttributesController;
    private Slider slider;
    void Start()
    {
        playerAttributesController = GameObject.Find("Player").GetComponent<PlayerAttributesController>();
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = playerAttributesController.Stamina;
    }

    void Update()
    {
        DrawStamina();
    }
    public void DrawStamina()
    {
        slider.value = playerAttributesController.Stamina;
    }
}
