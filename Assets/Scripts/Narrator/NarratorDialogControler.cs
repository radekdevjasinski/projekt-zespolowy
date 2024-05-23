using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class NarratorDialogControler : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float characterSpeed = 0.04f;
    [SerializeField] private GameObject mumblePack;
    private RandomLoopSoundControls musicLoopSoundControls;
    private IEnumerator coroutine;
    public bool isTalking;

    internal void SetText(string text)
    {
        if (coroutine != null)
        {
            musicLoopSoundControls.stopSound();
            StopCoroutine(coroutine);
        }

        if (!string.IsNullOrEmpty(text))
        {
            musicLoopSoundControls.playSound();
            coroutine = applyText(text);
            StartCoroutine(coroutine);
        }
        else
        {
            dialogueText.text = "";
            musicLoopSoundControls.stopSound();
            isTalking = false;
        }
    }

    private void Start()
    {
        dialogueText = GameObject.Find("Text Narrator").GetComponent<TMP_Text>();
        if (dialogueText == null)
        {
            Debug.LogError("Dialogue text is null");
            throw new Exception("Could not find narrator dialogue");
        }

        if (mumblePack != null)
            musicLoopSoundControls = SoundManager.instance.playRandomLoop(transform, mumblePack);
    }

    private IEnumerator applyText(string txt)
    {
        dialogueText.text = "";
        for (int i = 0; i < txt.Length; i++)
        {
            dialogueText.text += txt[i];
            yield return new WaitForSeconds(characterSpeed);
        }
        musicLoopSoundControls.stopSound();
        isTalking = false;
    }
}
