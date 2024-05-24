using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class EnemiesRoom : Room
{
    [SerializeField]private int enemiesCount;
    private AddEnemy[] addEnemis;
    private Object locker = new Object();
    private void Awake()
    {
        addEnemis = GetComponentsInChildren<AddEnemy>();
        enemiesCount = addEnemis.Length;
    }


   public void decreaseEnemyCount()
    {
        lock (locker)
        {
            
            Debug.Log("ENEMIES FROM  COUNT: " + enemiesCount);
            enemiesCount--;
            Debug.Log("ENEMIES TO  COUNT: " + enemiesCount);

            if (enemiesCount == 0)
            {
                onCleared();
            }
        }
    }

   

    private void onCleared()
    {
        Debug.Log("RROOOM CLEARED: "+ GetComponents<IOnRoomCleared>().Length);
        foreach (IOnRoomCleared var in GetComponents<IOnRoomCleared>())
        {
            var.onRoomCleared();
            //Debug.Log("runngig: "+ var.GetType());
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
              
            }

    }
    private void summonEnemies()
    {
        foreach (AddEnemy var in addEnemis)
        {
            GameObject enemy= var.Spawn();
            enemy.AddComponent<InformRoomAboutEnemyDeath>();
            InformRoomAboutEnemyDeath tmp;
            if (!enemy.TryGetComponent<InformRoomAboutEnemyDeath>(out tmp))
                throw new Exception("InformRoomAboutEnemyDeath was not added");
        }
    }


}
