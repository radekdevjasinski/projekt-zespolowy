using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerEntityController : EntityController<int>
{

    private PlayerAttributesController playerAttributesController;

    [SerializeField] private float invincLength = 1;
    private Rigidbody2D rb;
    public bool isKnocked;
    [SerializeField] private float knockbackForce = 20f;
    [SerializeField] private float knockbackDuration = 1f;

    protected void Awake()
    {
        playerAttributesController = GetComponent<PlayerAttributesController>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Code Functionality from previous Entity controller
    // that included also gorund affected behaviour
    // I left if there was a need for changing behaviour based on ground later


    //public override void resetDrag()
    //{
    //    Debug.Log("reset drag");
    //    this.GetComponent<Rigidbody2D>().drag = this.groundDragBase;
    //}

    //public override void setDrag(float drag)
    //{
    //    Debug.Log("set drag");
    //    this.GetComponent<Rigidbody2D>().drag=drag;
    //}

    //public override void setGroundSpeedAffect(float f)
    //{
    //  this.GetComponent<PlayerMovementController>().setGroundSpeedAffect(f);
    //}
    

    private void Update()
    {
        if (invincCount > 0)
        {
            invincCount -= Time.deltaTime;
        }
    }
    public override int getMaxHealth()
    {
        return playerAttributesController.getMaxHealth();


    }

    public override void reviceDamage(int damage, Vector2 damageDirection)
    {
        if (playerAttributesController.Armor > 0)
        {
            playerAttributesController.increaseArmor(-damage);
            GameObject ArmorBar = GameObject.Find("ArmorBar");
            ArmorBar.GetComponent<ArmorBar>().DrawArmor();
        }
        else
        {
            playerAttributesController.increaseHealth(-damage);
            GameObject HpBar = GameObject.Find("HpBar");
            HpBar.GetComponent<AnimatedHealthBar>().DrawHearts();
        }
        
        invincCount = invincLength;
        // Odwracamy kierunek odrzutu
        Vector2 knockbackDirection = -damageDirection.normalized;
        StartCoroutine(HitKnockback(knockbackDirection));

    }
    public override void reviceDamage(int damage)
    {
        Debug.Log("player recieved not direcitnla damge");
        if (playerAttributesController.Armor > 0)
        {
            playerAttributesController.increaseArmor(-damage);
            GameObject ArmorBar = GameObject.Find("ArmorBar");
            ArmorBar.GetComponent<ArmorBar>().DrawArmor();
        }
        else
        {
            playerAttributesController.increaseHealth(-damage);
            GameObject HpBar = GameObject.Find("HpBar");
            //HpBar.GetComponent<AnimatedHealthBar>().DrawHearts();
            HpBar.GetComponent<AnimatedHealthBar>().DrawHearts();
        }

        invincCount = invincLength;

    }

    public override void dealDamageUniversal(float amount)
    {
        dealDamage((int)amount);
    }

    public override void dealDamageUniversal(float dmg, Vector2 vector2)
    {
        dealDamage((int)dmg, vector2);
    }

    protected virtual IEnumerator HitKnockback(Vector2 knockbackDirection)
    {
        isKnocked = true;


        rb.velocity = knockbackDirection * knockbackForce;

        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
    }


    public void heal()
    {
        playerAttributesController.resetHealth();
        GameObject HpBar = GameObject.Find("HpBar");
        HpBar.GetComponent<AnimatedHealthBar>().DrawHearts();
    }
    public void heal(int amount)
    {
        GetComponent<PlayerAttributesController>().increaseHealth(amount);
        GameObject HpBar = GameObject.Find("HpBar");
        HpBar.GetComponent<AnimatedHealthBar>().DrawHearts();
    }

    public void addArmor(int amount)
    {
        playerAttributesController.increaseArmor(amount);
        GameObject ArmorBar = GameObject.Find("ArmorBar");
        ArmorBar.GetComponent<ArmorBar>().DrawArmor();

    }

    public override int getHealth()
    {
        return
           playerAttributesController.Health;
    }

    protected override void onDie()
    {
        //Destroy(this.gameObject);
        PlayerLastHitted playerLastHitted = GetComponent<PlayerLastHitted>();
        if (playerLastHitted != null)
        {
            GameObject lastHittedObject = playerLastHitted.GetLastTouchedObject();
            if (lastHittedObject != null)
            {
                NarratorConversation conversation = transform.GetComponentInChildren<NarratorConversation>();
                if (conversation != null)
                {
                    if (lastHittedObject.TryGetComponent<ZombieController>(out ZombieController component))
                    {
                        conversation.playerKilledByZombie = true;
                    }
                    else if ((lastHittedObject.TryGetComponent<EnemyShooting>(out EnemyShooting component1)) || 
                        (lastHittedObject.TryGetComponent<Arrow>(out Arrow arrow)))
                    {
                        conversation.playerKilledByArcher = true;
                    }
                    else if (lastHittedObject.TryGetComponent<LichWarriorEntity>(out LichWarriorEntity component2))
                    {
                        conversation.playerKilledByLich = true;
                    }
                    else
                    {
                        conversation.playerKilled = true;
                    }
                }
            }
        } else
        {
            Debug.Log("Didnt found last hitted object");
        }
        GameObject gameOverScreen = GameObject.Find("GameOver");
        gameOverScreen.GetComponent<GameOverScreen>().Setup();
        Time.timeScale = 1f;
        //PauseMenuController.GameIsPaused = true;
        GetComponent<PlayerControler>().enabled = false;
        GetComponent<PlayerMovementController>().enabled = false;
        GetComponent<PlayerActionsController>().enabled = false;
        GetComponent<PlayerAttributesController>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<PlayerUpdateScreenPostion>().enabled = false;
        GetComponent<PlayerItemsController>().enabled = false;
        GetComponent<PlaySounfOnHit>().enabled = false;
        GetComponent<SpriteFlashOnDamage>().enabled = false;
        GetComponent<RandomIdleController>().enabled = false;
        GetComponent<PlayerLastHitted>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;




    }
}
