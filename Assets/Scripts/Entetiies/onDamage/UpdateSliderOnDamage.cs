using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSliderOnDamage : MonoBehaviour ,OnDamage
{
    [SerializeField]
    private Slider slider;
    private EntityController<float> healthController;
    private void Awake()
    {
    healthController = GetComponent<EntityController<float>>();
    }

    public void onDamage()
    {

        slider.value = (healthController.getHealth() / healthController.getMaxHealth());
;    }
}
