using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI bombText;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI potionText;

    public void SetValuesToText(int coinsAmount,int bombAmount, int keyAmount, int potionAmount)
    {
        coinsText.text = coinsAmount.ToString("D2");
        bombText.text = bombAmount.ToString("D2");
        keyText.text = keyAmount.ToString("D2");
        potionText.text = potionAmount.ToString("D2");
    }
}
