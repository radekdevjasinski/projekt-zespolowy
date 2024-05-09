using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class onDamgeUpdateText : MonoBehaviour ,OnDamage
{
    [SerializeField]
    private TMP_Text text;
    private EntityController<float> healthController;
    private void Awake()
    {
    healthController = GetComponent<EntityController<float>>();
    }

    public void onDamage()
    {
        text.SetText(healthController.getHealth()+" / "+ healthController.getMaxHealth())
;    }
}
