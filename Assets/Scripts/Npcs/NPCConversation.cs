using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCConversation : MonoBehaviour
{
    [SerializeField]  private TMP_Text dialogueText;
    [SerializeField] private string[] eneterDialogues;
    void Start()
    {
        dialogueText = GameObject.Find("Text NPC").GetComponent<TMP_Text>();
    }

    public void setDialogeText(string text)
    {
        dialogueText.text=text;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartConversation();
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EndConversation();
        }
    }
    protected virtual void StartConversation()
    {
        setDialogeText(eneterDialogues[Random.Range(0, eneterDialogues.Length)]);

    }

    void EndConversation()
    {
        setDialogeText ("");

    }



}
