using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributesController : MonoBehaviour
{
    [SerializeField] private PlayerClass playerClass;
    //enum
    public enum attributes{
        HEALTH,
        STAMINA,
        ARMOR,
        SPEED,
        FIRE_RATE,
        DAMAGE,
        RANGE,
        PROJECTILE_SPEED,// currently not used, not sure about future implemenation
        LUCK,// currently not used, not sure about future implemenation
        PHYSICALRESISTANCE,// currently not used, not sure about future implemenation
        MAGICALRESISTANCE,// currently not used, not sure about future implemenation
        MAX_HEALTH,
        MAX_STAMINA
    }

    //Attributes
    [SerializeField] private int MaxHealth=6;
    [SerializeField] private int health=6;
    [SerializeField] private float MaxStamina = 6;
    [SerializeField] private float stamina=6;
    [SerializeField] private int armor=2;
    [SerializeField] private float speed =0;
    [SerializeField] private float fireRate=0;
    [SerializeField] private float damage=0;
    [SerializeField] private float range =0;
    [SerializeField] private float projectileSpeed = 0; //currently not used, not sure about future implemenation
    [SerializeField] private float luck = 0; //currently not used, not sure about future implemenation
    [SerializeField] private float power = 0;// currently not used, not sure about future implemenation
    [SerializeField] private float physicalResistance = 0;// currently not used, not sure about future implemenation
    [SerializeField] private float magicalResistance = 0;// currently not used, not sure about future implemenation

    //getters
    public int Health => health;
    
    public int Armor => armor;
    
    public float Stamina => stamina;

   public float Speed => speed;

   public float FireRate => fireRate;

   public float Damage => damage;

   public float Range => range;

   public float ProjectileSpeed => projectileSpeed;

   public float Luck => luck;


   public PlayerClass GetPlayerClass()
    {
        return playerClass;
    }


    //setters
    public void increaseHealth(float change)
   {
       this.health += (int) change;
        GameObject HpBar = GameObject.Find("HpBar");
        //HpBar.GetComponent<AnimatedHealthBar>().DrawHearts();
        HpBar.GetComponent<AnimatedHealthBar>().DrawHearts();
    }
    public void resetHealth()
    {
        this.health = MaxHealth;
    }
    public void increaseStamina(float change)
    {
        if (stamina + change >= MaxStamina)
            stamina = MaxStamina;
        else if (stamina + change <= 0)
            stamina = 0;
        else
            stamina += change;

        GameObject.Find("StaminaSpriteBar").GetComponent<StaminaSpriteBar>().SetStamina(stamina, MaxStamina);

    }
    public void increaseArmor(int change)
    {
        this.armor += change;
        GameObject ArmorBar = GameObject.Find("ArmorBar");
        ArmorBar.GetComponent<ArmorBar>().DrawArmor();
    }

    private void increaseSpeed(float change)
   {
        this.speed+= change;
   }

   private void increaseFireRate(float change)
   {
    this.fireRate+= change;
   }

   private void increaseDamage(float change)
   {
    this.damage += change;
   }
   private void increaseRange(float change)
   {
        this.range += change;
   }
   private void increaseProjectileSpeed(float change)
   {
    this.projectileSpeed += change;
   }
   private void increaseLuck(float change)
   {
        this.luck += change;
   }
   private void increasePhysicalResistance(float change)
   {
        this.physicalResistance += change;
   }

   private void increaseMagicalResistance(float change)
   {
        this.magicalResistance += change;
   }

   private void setPlayerClass(PlayerClass change)
    {
        this.playerClass = change;
    }
    public int getMaxHealth()
    {
        return MaxHealth;
    }
    public float getMaxStamina()
    {
        return MaxStamina;
    }
    private void increaseMaxHealth(float change)
    {
        this.MaxHealth +=(int)change;
        if (Health + (int)change < 1)
            health = 1;
        else
        {
            increaseHealth((int)change);
        }
        GameObject HpBar = GameObject.Find("HpBar");
        //HpBar.GetComponent<AnimatedHealthBar>().DrawHearts();
        HpBar.GetComponent<AnimatedHealthBar>().DrawHearts();
    }
    public int getArmor()
    {
        return armor;
    }
    private void increase(attributes attrib, float change)
   {
 
       switch (attrib)
       {
            case attributes.HEALTH: this.increaseHealth(change); break;
            case attributes.STAMINA: this.increaseStamina(change); break;
            case attributes.DAMAGE: this.increaseDamage(change); break;
            case attributes.ARMOR: this.increaseArmor((int)change); break;
            case attributes.FIRE_RATE: this.increaseFireRate(change); break;
            case attributes.LUCK: this.increaseLuck(change); break;
            case attributes.PROJECTILE_SPEED: this.increaseProjectileSpeed(change); break;
            case attributes.RANGE: this.increaseRange(change); break;
            case attributes.SPEED: this.increaseSpeed(change); break;
            case attributes.PHYSICALRESISTANCE: this.increasePhysicalResistance(change); break;
            case attributes.MAGICALRESISTANCE: this.increaseMagicalResistance(change);break;
            case attributes.MAX_HEALTH: this.increaseMaxHealth(change); break;

        }
        Debug.Log("increase: " + attrib + " with " + change+ " to overall value: "+ getAtrib(attrib));

    }

    public void increaseWithlimit(attributes attrib, float change)
    {
        Debug.Log("trying to incease: " + attrib + " with " + change);

        switch (attrib)
        {
            case attributes.HEALTH: increase(attrib, change); ; return;
            case attributes.MAX_HEALTH:  if(getMaxHealth()+change<1) setAttrib(attributes.MAX_HEALTH,1); else increaseMaxHealth(change); return;
            case attributes.ARMOR: if (getArmor() + change < 0) setAttrib(attributes.ARMOR, 0); else increaseArmor((int)change); return;
            default:
                if (getAtrib(attrib) + change < 0.1)
                {
                    setAttrib(attrib, 0.1f);
                }
                else
                {
                    increase(attrib, change);
                }
                return;
        }

        

}

    public void setAttrib(attributes attrib, float val)
    {
        switch (attrib)
        {
            case attributes.HEALTH: health=(int)val; break;
            case attributes.DAMAGE: damage=val; break;
            case attributes.FIRE_RATE: fireRate=val; break;
            case attributes.LUCK: luck=val; break;
            case attributes.PROJECTILE_SPEED: projectileSpeed=val; break;
            case attributes.RANGE: range=val; break;
            case attributes.SPEED: speed = val; break;
            case attributes.PHYSICALRESISTANCE: physicalResistance=val; break;
            case attributes.MAGICALRESISTANCE: magicalResistance=val; break;
            case attributes.MAX_HEALTH: MaxHealth=(int)val; break;

        }
    }

    public float getAtrib(attributes attrib)
    {
        switch (attrib)
        {
            case attributes.HEALTH: return (float)health; break;
            case attributes.DAMAGE: return (float)damage; break;
            case attributes.FIRE_RATE: return (float)fireRate; break;
            case attributes.LUCK: return (float)luck;  break;
            case attributes.PROJECTILE_SPEED: return (float)projectileSpeed; break;
            case attributes.RANGE: return (float)range; break;
            case attributes.SPEED: return (float)speed; break;
            case attributes.PHYSICALRESISTANCE: return (float)physicalResistance; break;
            case attributes.MAGICALRESISTANCE: return (float)magicalResistance; break;
            case attributes.MAX_HEALTH: return (float)getMaxHealth() ; break;
        }
        return -1;
    }


}
