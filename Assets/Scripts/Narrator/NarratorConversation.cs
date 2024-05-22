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
            Debug.Log("Found Lich");
        }
        lichCoords = lich.transform.position;

        if (Mathf.Abs(transform.parent.position.x - lichCoords.x) <= 50f && Mathf.Abs(transform.parent.position.y - lichCoords.y) <= 50f)
        {
            if (!beforeEnteringBoss)
            {
                globalMessage = "Before Enetering Boss";
                StartBeforeEnterBossConversation();
                beforeEnteringBoss = true;
            }
        } else
        {
            beforeEnteringBoss = false;
        }
        if (!dialogController.isTalking)
            EndConversation();

        if (gameStarted)
        {
            StartConversation();
            gameStarted = false;
        }

        if (sellerAppear)
        {
            StartSellerAppearConversation();
            sellerAppear = false;
        }

        if (playerKilled)
        {
            StartPlayerDeathConversation();
            playerKilled = false;
        }

        if (lichKilled)
        {
            StartPlayerKillLichConversation();
            lichKilled = false;
        }

        if (newItemAppear)
        {
            StartNewItemConversation();
            newItemAppear = false;
        }

        if (bossKilled)
        {
            StartPlayerKillBossConversation();
            bossKilled = false;
        }
        if (playerKilledByArcher)
        {
            StartPlayerDeathByArcherConversation();
            playerKilledByArcher = false;
        }
        if (playerKilledByZombie)
        {
            StartPlayerDeathByZombieConversation();
            playerKilledByZombie = false;
        }
        if (playerKilledByLich)
        {
            StartPlayerDeathByLichConversation();
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

    protected virtual void StartConversation()
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = beginGameDialogues[Random.Range(0, beginGameDialogues.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }

    protected virtual void StartPlayerDeathByLichConversation()
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = deathByLichDialogues[Random.Range(0, deathByLichDialogues.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }

    protected virtual void StartPlayerDeathByArcherConversation()
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = deathByArcherDialogues[Random.Range(0, deathByArcherDialogues.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }

    protected virtual void StartPlayerDeathByZombieConversation()
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = deathByZombieDialogues[Random.Range(0, deathByZombieDialogues.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }

    protected virtual void StartPlayerDeathConversation()
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = deathDialogues[Random.Range(0, deathDialogues.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }

    protected virtual void StartPlayerKillLichConversation()
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = killLichDialogues[Random.Range(0, killLichDialogues.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }

    protected virtual void StartPlayerKillBossConversation()
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = killBossDialogues[Random.Range(0, killBossDialogues.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }

    protected virtual void StartSellerAppearConversation()
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = sellerDialogues[Random.Range(0, sellerDialogues.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }

    protected virtual void StartBeforeEnterBossConversation()
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = beforeEnterBossDialogues[Random.Range(0, beforeEnterBossDialogues.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }

    protected virtual void StartNewItemConversation()
    {
        dialogController.isTalking = true;
        previousMessage = currentMessage;
        do
        {
            currentMessage = newItemDialogues[Random.Range(0, newItemDialogues.Length)];
        } while (currentMessage == previousMessage);

        setDialogeText(currentMessage);
    }

    void EndConversation()
    {
        globalMessage = "";
        setDialogeText("");
    }
}
