using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy " + gameObject.name + " has: " + currentHealth + " health points!");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy dies!");
        Destroy(gameObject);
    }
}
