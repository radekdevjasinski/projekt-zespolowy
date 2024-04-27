using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;

public class NPCConversation : MonoBehaviour
{
    [SerializeField]  private TMP_Text dialogueText;
    [SerializeField] private LocalizedString [] eneterDialogues;
    protected virtual void Start()
    {
        dialogueText = GameObject.Find("Text NPC").GetComponent<TMP_Text>();
        if(dialogueText==null)
        {
            Debug.Log("dialoge txt is null");
            throw new System.Exception("Couldnt find npc dialog");
        }
        else
        {
            Debug.Log("dialoge txt not null");
        }
        }

    public void setDialogeText(string text)
    {
        dialogueText.SetText(text);
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
