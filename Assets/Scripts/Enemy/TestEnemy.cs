using UnityEngine;

public class TestEnemy : EnemyBase
{
    // Update is called once per frame
    void Update()
    {
        Move();
        if (IsWithinRange())
        {
            Die();
        }
    }

    protected override void Die()
    {
        ExplodeOnDeath();
        base.Die();
    }

    private void ExplodeOnDeath()
    {
        Debug.Log("Exploded");
    }
}
