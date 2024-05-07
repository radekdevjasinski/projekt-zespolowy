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
        ARMOR,
        SPEED,
        FIRE_RATE,
        DAMAGE,
        RANGE,
        PROJECTILE_SPEED,
        LUCK
    }

    //Attributes
  [SerializeField] private int MaxHealth=6;
  [SerializeField] private int health=6;
  [SerializeField] private int armor=2;
  [SerializeField] private int speed=0;
  [SerializeField] private int fireRate=0;
  [SerializeField] private int damage=0;
  [SerializeField] private int range=0;
  [SerializeField] private int projectileSpeed=0;
  [SerializeField] private int luck=0;
  [SerializeField] private int power=0;

   //getters
   public int Health => health;
   public int Armor => armor;

   public int Speed => speed;

   public int FireRate => fireRate;

   public int Damage => damage;

   public int Range => range;

   public int ProjectileSpeed => projectileSpeed;

   public int Luck => luck;


   public PlayerClass GetPlayerClass()
    {
        return playerClass;
    }


   //setters
   public void increaseHealth(int change)
   {
       this.health+= change;
   }
    public void resetHealth()
    {
        this.health = MaxHealth;
    }
    public void increaseArmor(int change)
    {
        this.armor += change;
    }
    public void increaseSpeed(int change)
   {
        this.speed+= change;
   }

   public void increaseFireRate(int change)
   {
    this.fireRate+= change;
   }

   public void increaseDamage(int change)
   {
    this.damage += change;
   }
   public void increaseRange(int change)
   {
        this.range += change;
   }
   public void increaseProjectileSpeed(int change)
   {
    this.projectileSpeed += change;
   }
   public void increaseLuck(int change)
   {
        this.luck += change;
   }
   public void setPlayerClass(PlayerClass change)
    {
        this.playerClass = change;
    }
    public void increase(attributes attrib, int change)
   {
       switch (attrib)
       {
            case attributes.HEALTH: this.increaseHealth(change); break;
            case attributes.DAMAGE: this.increaseDamage(change); break;
            case attributes.ARMOR: this.increaseArmor(change); break;
            case attributes.FIRE_RATE: this.increaseFireRate(change); break;
            case attributes.LUCK: this.increaseLuck(change); break;
            case attributes.PROJECTILE_SPEED: this.increaseProjectileSpeed(change); break;
            case attributes.RANGE: this.increaseRange(change); break;
            case attributes.SPEED: this.increaseSpeed(change); break;
       }
   }


}
