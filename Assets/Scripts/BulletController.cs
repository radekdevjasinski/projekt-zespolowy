using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] int damage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null) 
            {
                enemy.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

}
