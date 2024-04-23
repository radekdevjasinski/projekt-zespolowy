using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCConversation : MonoBehaviour
{
    public TMP_Text dialogueText;
    public string[] dialogues;

    void Start()
    {
        dialogueText = GameObject.Find("Text NPC").GetComponent<TMP_Text>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartConversation();
            GetComponent<NPCSpells>().HealPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EndConversation();
        }
    }
    void StartConversation()
    {
        dialogueText.text = dialogues[Random.Range(0, dialogues.Length)];

    }

    void EndConversation()
    {
        dialogueText.text = "";

    }
}
