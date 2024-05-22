using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;

[RequireComponent(typeof(DialogController))]
public class NPCConversation : MonoBehaviour
{
    private DialogController dialogController;
    [SerializeField] private LocalizedString [] eneterDialogues;
    protected NarratorDialogControler narratorDialog;
    private void Awake()
    {
        dialogController = GetComponent<DialogController>();

    }
    protected virtual void Start()
    {
        narratorDialog = FindObjectOfType<NarratorDialogControler>();
        if (narratorDialog != null)
        {
            Debug.Log("NarratorDialogControler found.");
        }
        else
        {
            Debug.LogError("NarratorDialogControler not found.");
        }

    }

    public void setDialogeText(string text)
    {
        dialogController.SetText(text);
    }
    public void setDialogeText(LocalizedString text)
    {
        setDialogeText(text.GetLocalizedString());
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(narratorDialog != null)
            {
                if (!narratorDialog.isTalking)
                {
                    StartConversation();
                }
            }

            
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
