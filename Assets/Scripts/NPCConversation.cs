using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCConversation : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text playerResponseText;
    private bool conversationStarted = false;

    void Start()
    {
        playerResponseText.gameObject.SetActive(false);
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

    void Update()
    {
        if (conversationStarted)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                RespondToNPC("1. Tak, chêtnie.");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                RespondToNPC("2. Nie, dziêkujê.");
            }
        }
    }

    void StartConversation()
    {
        dialogueText.text = "Czy mogê ci pomóc?";
        playerResponseText.gameObject.SetActive(true);
        playerResponseText.text = "1. Tak, chêtnie.\n2. Nie, dziêkujê.";
        conversationStarted = true; 
    }

    void EndConversation()
    {
        dialogueText.text = "";
        playerResponseText.text = "";
        playerResponseText.gameObject.SetActive(false);
        conversationStarted = false; 
    }

    void RespondToNPC(string response)
    {
        playerResponseText.text = "Gracz odpowiedzia³: " + response;
        if (response.StartsWith("1"))
        {
            RespondToPlayer("Dziêkujê! Jestem bardzo zadowolony z twojej decyzji.");
        }
        else if (response.StartsWith("2"))
        {
            RespondToPlayer("Rozumiem. Mo¿e nastêpnym razem.");
        }
        playerResponseText.gameObject.SetActive(false);
    }

    void RespondToPlayer(string response)
    {
        dialogueText.text = response;
    }
}
