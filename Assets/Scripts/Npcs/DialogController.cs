using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogController : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float characterSpeed=0.1f;
    private IEnumerator coroutine;
    internal void SetText(string text)
    {
        if(coroutine != null)
        {

            StopCoroutine(coroutine);
        }
        coroutine = applyText(text);
        StartCoroutine(coroutine);

    }

    private void Start()
    {
        dialogueText = GameObject.Find("Text NPC").GetComponent<TMP_Text>();
        if (dialogueText == null)
        {
            Debug.Log("dialoge txt is null");
            throw new System.Exception("Couldnt find npc dialog");
        }
        else
        {
            Debug.Log("dialoge txt not null");
        }
    }

    private IEnumerator applyText(String txt)
    {
        dialogueText.text = "";
        int character = 0;
        for(int i = 0; i < txt.Length; i++)
        {
            Debug.Log("txt add: " + txt[i]);
            dialogueText.text += txt[i];
           yield return new WaitForSeconds(characterSpeed);
        }
       
    }




}
