using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesRoom : Room
{
    private int enemiesCount;
    private AddEnemy[] addEnemis;
    private void Awake()
    {
        addEnemis = GetComponentsInChildren<AddEnemy>();
        enemiesCount = addEnemis.Length;
    }


    void decreaseEnemyCount()
    {
        enemiesCount--;
        Debug.Log("ENEMIES COUNT: "+ enemiesCount);

        if (enemiesCount == 0)
        {
            onCleared();
        }
    }

   

    private void onCleared()
    {
        Debug.Log("RROOOM CLEARED: "+ GetComponents<IOnRoomCleared>().Length);
        foreach (IOnRoomCleared var in GetComponents<IOnRoomCleared>())
        {
            var.onRoomCleared();
            Debug.Log("runngig: "+ var.GetType());
        }
    }

    public override void onFirstEntry()
    {
        base.onFirstEntry();
        this.closeAllDorrs();
        summonEnemies();
   
    }

    protected override IEnumerator wakeUpRoutine(float controlsOfTime)
    {
        //yield return new WaitForSeconds(0);
        yield return base.wakeUpRoutine(controlsOfTime);
        WakeUpEnemies();
    }

    private void WakeUpEnemies()
    {
  
            EnemyBase[] enemies = transform.GetComponentsInChildren<EnemyBase>();
            foreach (EnemyBase enemy in enemies)
            {
                enemy.LockMovement = false;
                enemy.AddComponent<InformRoomAboutEnemyDeath>().setup(()=>{decreaseEnemyCount();});
            }

    }
    private void summonEnemies()
    {
        foreach (AddEnemy var in addEnemis)
        {
            var.Spawn();
        }
    }


}
