using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEntityController : EntityController<float>, StageDeprndentElements
{

    [SerializeField] private float maxHealth;
    private BossFightController bossFightController;
    private float health;
    [SerializeField] private GameObject healthBar;
    private GameObject healthBarInstnace;
    
    private Animator animator;

    override protected float getMaxHealth()
    {
        return maxHealth;
    }

    BossUIHelathBar bossUIHelathBar;

    private void Awake()
    {
        health = maxHealth;
        bossFightController = GameObject.Find("BossFight").GetComponent<BossFightController>();
        bossFightController.addStageDependentElement(this.GetComponent<BossEntityController>());
        GameObject canvas = GameObject.Find("Canvas");
        healthBarInstnace = Instantiate(healthBar, canvas.transform);
        bossUIHelathBar = healthBarInstnace.GetComponent<BossUIHelathBar>();
        animator = GetComponent<Animator>();
    }





    protected override void onDie()
    {
        
        bossFightController.stopFight();
        Destroy(this.gameObject);
        Destroy(healthBarInstnace.gameObject);
    }

    protected override float getHealth()
    {
        return this.health;
    }



    protected override void reviceDamage(float damage)
    {
        this.health-=damage;
        this.bossUIHelathBar.updateSlider(this.health,this.maxHealth);
        if ((float)this.getHealth() / this.getMaxHealth() * 100 < this.bossFightController.getNextStageBarrier())
        {
            this.bossFightController.increaseStage();
        }

    }



    public void increaseStage()
    {
        Debug.Log("Crystal increase stage");
        animator.SetInteger("Stage", bossFightController.getStage());
    }
}
