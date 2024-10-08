using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarratorDialogControler : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float characterSpeed = 0.04f;
    [SerializeField] private float pause = 2f;
    [SerializeField] private GameObject mumblePack;
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private Image image;
    private RandomLoopSoundControls musicLoopSoundControls;
    private IEnumerator coroutine;
    public bool isTalking;
    public bool canTalk;

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

        if(image == null)
        {
            Debug.LogError("Border image is null");
            throw new Exception("Could not find border image");
        }

        if (mumblePack != null)
        {
            musicLoopSoundControls = SoundManager.instance.playRandomLoop(transform, mumblePack);
        }
    }

    private IEnumerator applyText(string txt)
    {
        dialogueText.text = "";
        int endLength = (txt.Length / 4) * 2;

        StartCoroutine(FadeImage(false));
        for (int i = 0; i < txt.Length; i++)
        {
            dialogueText.text += txt[i];
            if (i == endLength)
            {
                foreach (var source in musicLoopSoundControls.GetComponentsInChildren<AudioSource>())
                {
                    StartCoroutine(FadeOutSound());
                }
            }
            yield return new WaitForSeconds(characterSpeed);
        }
        yield return new WaitForSeconds(pause);
        musicLoopSoundControls.stopSound();
        isTalking = false;
        AudioSource[] sources = musicLoopSoundControls.GetComponentsInChildren<AudioSource>();
        foreach (var source in sources)
        {
            source.volume = 1.0f;
        }
        StartCoroutine(FadeImage(true));
    }

    private IEnumerator FadeOutSound()
    {
        float currentTime = 0;

        AudioSource[] sources = musicLoopSoundControls.GetComponentsInChildren<AudioSource>();

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            foreach (var source in sources)
            {
                source.volume -= Time.deltaTime / fadeDuration;
            }
            yield return null;
        }

        foreach (var source in sources)
        {
            source.volume = 0.1f;
        }
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                image.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
