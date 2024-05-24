using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using Unity.VisualScripting;

[RequireComponent(typeof(NarratorDialogControler))]
public class NarratorConversation : MonoBehaviour
{
    public NarratorDialogControler dialogController;
    [SerializeField] private LocalizedString[] beginGameDialogues;
    [SerializeField] private LocalizedString[] deathByLichDialogues;
    [SerializeField] private LocalizedString[] killLichDialogues;
    [SerializeField] private LocalizedString[] killBossDialogues;
    [SerializeField] private LocalizedString[] sellerDialogues;
    [SerializeField] private LocalizedString[] hiddenCoinDialogues;
    [SerializeField] private LocalizedString[] beforeEnterBossDialogues;
    [SerializeField] private LocalizedString[] newItemDialogues;
    [SerializeField] private LocalizedString[] deathByArcherDialogues;
    [SerializeField] private LocalizedString[] deathByZombieDialogues;
    [SerializeField] private LocalizedString[] deathDialogues;

    public string globalMessage = "";

    [SerializeField] private bool gameStarted = true;
    [SerializeField] private bool sellerAppear = false;
    [SerializeField] private bool beforeEnteringBoss = false;
    [SerializeField] public bool newItemAppear = false;
    [SerializeField] public bool playerKilledByArcher = false;
    [SerializeField] public bool playerKilledByZombie = false;
    [SerializeField] public bool playerKilledByLich = false;
    [SerializeField] public bool playerKilled = false;
    [SerializeField] public bool lichKilled = false;
    [SerializeField] private bool bossKilled = false;
    private Vector2 lichCoords;
    private GameObject lich;

    private LocalizedString previousMessage;
    private LocalizedString currentMessage;

    private void Awake()
    {
        dialogController = GetComponent<NarratorDialogControler>();
        currentMessage = beginGameDialogues[Random.Range(0, beginGameDialogues.Length)];
        previousMessage = currentMessage;

    }

    private void Update()
    {
        lich = GameObject.FindGameObjectWithTag("LichBossRoom");
        if (lich != null)
        {
            lichCoords = lich.transform.position;

            if (Mathf.Abs(transform.parent.position.x - lichCoords.x) <= 50f && Mathf.Abs(transform.parent.position.y - lichCoords.y) <= 50f)
            {
                if (!beforeEnteringBoss)
                {
                    globalMessage = "Before Enetering Boss";
                    StartConversation(beforeEnterBossDialogues);
                    beforeEnteringBoss = true;
                }
            }
            else
            {
                beforeEnteringBoss = false;
            }
        }
        
        if (!dialogController.isTalking)
            EndConversation();

        if (gameStarted)
        {
            StartConversation(beginGameDialogues);
            gameStarted = false;
        }

        if (sellerAppear)
        {
            StartConversation(sellerDialogues);
            sellerAppear = false;
        }

        if (playerKilled)
        {
            StartConversation(deathDialogues);
            playerKilled = false;
        }

        if (lichKilled)
        {
            StartConversation(killLichDialogues);
            lichKilled = false;
        }

        if (newItemAppear)
        {
            StartConversation(newItemDialogues);
            newItemAppear = false;
        }

        if (bossKilled)
        {
            StartConversation(killBossDialogues);
            bossKilled = false;
        }
        if (playerKilledByArcher)
        {
            StartConversation(deathByArcherDialogues);
            playerKilledByArcher = false;
        }
        if (playerKilledByZombie)
        {
            StartConversation(deathByZombieDialogues);
            playerKilledByZombie = false;
        }
        if (playerKilledByLich)
        {
            StartConversation(deathByLichDialogues);
            playerKilledByLich = false;
        }
    }

    public void HandleNarratorMessage(string message)
    {
        globalMessage = message;
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
        if (other.CompareTag("NPC"))
        {
            if (other.TryGetComponent<NpcTrader>(out NpcTrader conversationScript))
            {
                globalMessage = "Seller";
                sellerAppear = true;
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

    protected virtual void StartConversation(LocalizedString[] name)
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = name[Random.Range(0, name.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }


    void EndConversation()
    {
        globalMessage = "";
        setDialogeText("");
    }
}
