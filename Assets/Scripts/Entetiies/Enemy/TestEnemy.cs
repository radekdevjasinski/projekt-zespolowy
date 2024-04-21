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

}
