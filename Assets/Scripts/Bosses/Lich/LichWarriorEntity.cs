using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichWarriorEntity : EntityController, StageDeprndentElements
{

    [SerializeField] private GameObject particleDeathLoading;

    [SerializeField]
    private Behavior[][] stagedBehaviorus;

    private Animator animator;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    public override void resetDrag()
    {
        throw new System.NotImplementedException(); // not aplicable, considering changing base class to avoid this
    }

    public override void setDrag(float drag)
    {
        throw new System.NotImplementedException(); // not aplicable, considering changing base class to avoid this
    }

    public override void setGroundSpeedAffect(float f)
    {
        throw new System.NotImplementedException(); // not aplicable, considering changing base class to avoid this
    }

    protected override void onDie()
    {
        Debug.Log(" animator.SetTrigger(\"LoadDeath\");");
        animator.SetTrigger("LoadDeath");
    }

    public void deathLoad()
    {
        Debug.Log("Death loading");
        Instantiate(particleDeathLoading, this.transform, false);
    }

    public void deathLoaded()
    {
        runDeathSequanceElemnts();
        Destroy(this.gameObject);
    }
    protected override int getHealth()
    {
        return 0;
    }

    protected override void reviceDamage(int damage)
    {
      
    }

    public void increaseStage()
    {
        throw new System.NotImplementedException();
    }
}
