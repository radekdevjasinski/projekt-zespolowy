using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    [SerializeField] TMP_Text textToSet;

    public void setText(string text)
    {
        textToSet.text=text;
    }
}
