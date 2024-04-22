using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerActionsController : MonoBehaviour
{
    [Header("Putting")]
    [SerializeField] private GameObject puttableItem;
    [Header("Slashing")]
    [SerializeField] private GameObject slashableItem;
    [Header("Shooting")]
    [SerializeField] private GameObject shootableItem;
    [Header("Summoning")]
    [SerializeField] private GameObject summonableAlly;
    [SerializeField] private SummonedControler summonedControler;
    [SerializeField] private float shootSpeed;
    [SerializeField] private float shootAttributeMultiplayer=1;

    [SerializeField] private float shootCooldown;
    [SerializeField] private float shootCooldownAttributeMultiplayer;
    private Transform shootParent;
    private Transform puttedParent;
    private Transform summonedParent;
    [Header("Single item use")] 
    [SerializeField] private GameObject singleUseItem;
    [Header("Activated item use")]
    //states
    bool canShoot;
    bool isShooting;
    Vector2 direction;
    bool isMouseLeftPressed;
    bool isShootButtonPress;
    Vector2 worldMousePositon;
    Vector2 offsetSummoning = new Vector2(1f, 0f);


    //components
    Collider2D collider;
    private Animator animator;


    //controllers
    private PlayerAttributesController playerAttributes;

    private void Awake()
    {
        canShoot = true;
        this.collider=GetComponent<Collider2D>();
        shootParent = GameObject.Find("Projectiles").transform;
        puttedParent = GameObject.Find("Putted").transform;
        summonedParent = GameObject.Find("Summoned").transform;
        animator = this.gameObject.GetComponent<Animator>();
        playerAttributes = this.gameObject.GetComponent<PlayerAttributesController>();
    }

    void Update()
    {
        udpateMouseShoot();
    }
    #region Slashing
    
    private void Slash()
    {
        if (canShoot)
        {
            canShoot = false;

            Vector3 startPosition = new Vector3(
                transform.position.x + collider.bounds.size.x * 1.4f * direction.x,
                transform.position.y + collider.bounds.size.y * 1.4f * direction.y,
                0f);

            Debug.Log(direction);
            GameObject slashable = Instantiate(
                slashableItem,
                 startPosition,
               new Quaternion(0f, 0f, 0f, 0f),
               shootParent
               );
            slashable.GetComponent<Projectile>().setupProjectileParams(
                playerAttributes.Damage,
                playerAttributes.Range
                );
            RotateSword(slashable);

            slashable.GetComponent<Projectile>().setWasShootByPlayer(true);
            Invoke("resetShootingCooldDown", shootCooldown / (1 + playerAttributes.FireRate * shootCooldownAttributeMultiplayer));
        }
    }

    private void RotateSword(GameObject slashable)
    {
        if (direction == new Vector2(-1f, 0))
        {
            slashable.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        if (direction == new Vector2(0, -1f))
        {
            slashable.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        if (direction == new Vector2(1f, 0))
        {
            slashable.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
        }
    }

    public void UseAbilityOne()
    {
        if(summonedControler.summons.Count == 0)
        {
            int i;
            Vector2 newPosition = (Vector2)this.transform.position;
            for (i = 1; i <= 2; i++)
            {
                Vector2 newPositionTmp = newPosition;
                if (i % 2 == 0)
                {
                    offsetSummoning = new Vector2(-1f, 0f);
                }
                else if (i % 3 == 0)
                {
                    offsetSummoning = new Vector2(1f, 0);
                }
                newPositionTmp = newPosition + offsetSummoning;
                GameObject summoned = Instantiate(
                summonableAlly,
                newPositionTmp,
                new Quaternion(0f, 0f, 0f, 0f),
                summonedParent); 
                summonedControler.summons.Add(summoned.GetComponent<Summoned>());
            }
        }
        else
        {
            if(summonedControler.summonedStance == SummonedStance.Defense)
            {
                summonedControler.summonedStance = SummonedStance.Aggresive;
            }
            else
            {
                summonedControler.summonedStance = SummonedStance.Defense;
            }
        }
       
    }



    #endregion

    #region Shooting

    private void shoot()
    {
    if(canShoot)
        {
            canShoot = false;
           
      
        Vector3 startPosition = new Vector3(
            transform.position.x + collider.bounds.size.x* 1.4f * direction.x,
            transform.position.y + collider.bounds.size.y*1.4f * direction.y,
            0f)
            ;
            GameObject shootable = Instantiate(
                shootableItem,
                 startPosition,
               new Quaternion(0f, 0f, 0f, 0f),
               shootParent
               );
            shootable.GetComponent<Projectile>().setupProjectileParams(
                playerAttributes.Damage,
                playerAttributes.Range
                );
            shootable.GetComponent<Projectile>().setWasShootByPlayer(true);
            shootable.GetComponent<Rigidbody2D>().AddForce(direction * shootSpeed *(1+playerAttributes.ProjectileSpeed) , ForceMode2D.Impulse);
            Invoke("resetShootingCooldDown", shootCooldown/(1+playerAttributes.FireRate* shootCooldownAttributeMultiplayer));
        }
    }


    private void resetShootingCooldDown()
    {
    this.canShoot = true;

        if (isShooting)
        {
            ClassActionSelection();
        }
    }

    public void setIsShooting(bool state,Vector2 direction)
    {
        this.isShooting = state;
        this.direction = direction;
        if (state && canShoot)
            ClassActionSelection();
    }


    void udpateMouseShoot()
    {
       
    if(isMouseLeftPressed)
        {
            this.setShootingDirectionFromMouse();
        } 
    }

    public void setWorldMousePostion(Vector2 pos)
    {
        this.worldMousePositon = pos;
    }

    public void setLeftMousePress(bool state)
    {
        this.isMouseLeftPressed = state;
    }

    private Vector2 bount(Vector2 vec)
    {
       
        Vector2 tmp = new Vector2(
           vec.x == 0 ? 0 : vec.x < 0 ? -1 : 1,
             vec.y == 0 ? 0 : vec.y < 0 ? -1 : 1
            );
    
        return new Vector2(
            Mathf.Abs(vec.y)> Mathf.Abs(vec.x) ? 0:tmp.x,
            Mathf.Abs(vec.y) <= Mathf.Abs(vec.x) ? 0 : tmp.y
            );
        

    }

    internal void setShootingDiretion(Vector2 direction)
    {
        Vector2 dir=bount(direction);

        animator.SetFloat("HorizontalShoot", dir.x);
        animator.SetFloat("VerticalShoot", dir.y);
        this.direction= dir;
    }

    internal void setIsShooting(bool state)
    {
        if (state)
        {
            animator.ResetTrigger("Shooting");
            if (canShoot)
            {
                this.isShooting = state;
                
                animator.SetTrigger("Shooting");
                ClassActionSelection();
            }
            
        }
        else
        {
            this.isShooting = state;
        }
    }

    private void ClassActionSelection()
    {
        if (GetComponent<PlayerAttributesController>().GetPlayerClass() == PlayerClass.Warrior)
        {
            this.Slash();
        }
        if (GetComponent<PlayerAttributesController>().GetPlayerClass() == PlayerClass.Archer)
        {
            this.shoot();
        }
        if (GetComponent<PlayerAttributesController>().GetPlayerClass() == PlayerClass.Mage)
        {
            this.shoot();
        }
    }

    internal void setShootingDirectionFromMouse()
    {
        this.setShootingDiretion(worldMousePositon - new Vector2(this.transform.position.x, this.transform.position.y));
    }
    #endregion

    #region Putting

    public void putObject()
    {

        GameObject shootable = Instantiate(
            puttableItem,
            this.transform.position,
            new Quaternion(0f, 0f, 0f, 0f),
            puttedParent
        );

    }


    #endregion

    #region single use item
    public void setSingleUseItem(GameObject o)
    {
        Debug.Log("-------new single use item: "+ o.GetComponent<UsableItem>().getAudioClip().name);
        this.singleUseItem = o;
    }

    public void useSingleUseItem()
    {
        Debug.Log("attempt use singel itme");

        if (singleUseItem != null)
        {
            Debug.Log("----------Use item");
            singleUseItem.GetComponent<UsableItem>().use(this.gameObject);
            Destroy(singleUseItem);
            singleUseItem = null;
        }
        else
        {
            Debug.Log("single use item null");

        }
    }


#endregion
}
