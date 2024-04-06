using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntityController : EntityController
{

    [SerializeField] private int maxHealth;
    private BossFightController bossFightController;
     private int health;
    [SerializeField] private GameObject healthBar;


    BossUIHelathBar bossUIHelathBar;

    private void Awake()
    {
        health = maxHealth;
        bossFightController = GameObject.Find("BossFight").GetComponent<BossFightController>();
        GameObject canvas = GameObject.Find("Canvas");
        GameObject healthbar= Instantiate(healthBar, canvas.transform);
        bossUIHelathBar = healthbar.GetComponent<BossUIHelathBar>();
    }

    public override void resetDrag()
    {
       // not aplicable, considering changing base class to avoid this
    }

    public override void setDrag(float drag)
    {
        // not aplicable, considering changing base class to avoid this
    }

    public override void setGroundSpeedAffect(float f)
    {
        // not aplicable, considering changing base class to avoid this
    }

    protected override void onDie()
    {
        bossFightController.stopFight();
    }

    protected override int getHealth()
    {
        return this.health;
    }

    protected override void reviceDamage(int damage)
    {
        this.health-=damage;
        this.bossUIHelathBar.updateSlider(this.health,this.maxHealth);
    }
}
