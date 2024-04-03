using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCConversation : MonoBehaviour
{
    public TMP_Text dialogueText; 
    private bool conversationStarted = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            conversationStarted = true; 
            Debug.Log("Gracz wszed� w obszar kolizji NPC."); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            conversationStarted = false; 
            Debug.Log("Gracz opu�ci� obszar kolizji NPC.");
            dialogueText.text = "";
        }
    }
    void Update()
    {
        if (conversationStarted && Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("Rozpocz�to rozmow� z NPC.");
            dialogueText.text = "Hello!"; 
            Debug.Log("Tekst wy�wietlony na ekranie: " + dialogueText.text); 
        }
    }

}
