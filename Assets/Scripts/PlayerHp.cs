using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public int maxHp = 5;
    public int hp;
    public GameOverScreen gameOverScreen;
    void Start()
    {
        hp = maxHp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        OnPlayerDamaged?.Invoke();
        if(hp <= 0)
        {
            Destroy(gameObject);
            gameOverScreen.Setup();
        }

    }
}
