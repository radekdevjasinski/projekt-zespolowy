using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUIHelathBar : MonoBehaviour
{


    private Slider slider;

    private void Awake()
    {
        slider = this.GetComponent<Slider>();
    }


    public void updateSlider(int a, int b)
    {
        slider.value = (float)a / b;
    }
    public void updateSlider(float a, float b)
    {
        slider.value = a / b;
    }
}
