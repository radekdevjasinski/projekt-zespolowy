using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;

public class NPCConversation : MonoBehaviour
{
    [SerializeField]  private TMP_Text dialogueText;
    [SerializeField] private LocalizedString [] eneterDialogues;
    void Start()
    {
        dialogueText = GameObject.Find("Text NPC").GetComponent<TMP_Text>();
    }

    public void setDialogeText(string text)
    {
        dialogueText.text=text;
    }
    public void setDialogeText(LocalizedString text)
    {
        setDialogeText(text.GetLocalizedString());
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
