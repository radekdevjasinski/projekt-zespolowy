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
        coinsText.text = coinsAmount.ToString();
        bombText.text = bombAmount.ToString();
        keyText.text = keyAmount.ToString();
        potionText.text = potionAmount.ToString();
    }
}
