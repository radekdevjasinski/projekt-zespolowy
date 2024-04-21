using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoned : MonoBehaviour
{
    private SummonedAttributesController summonedAttributesController; 
    private GameObject closeEnemy;
    private GameObject[] listOfEnemis;

    private void Start()
    {
        summonedAttributesController = GetComponent<SummonedAttributesController>();
    }

    public void DeffenseStance(Vector2 pos)
    {
        transform.position = Vector2.MoveTowards(transform.position, pos, summonedAttributesController.GetSpeed() * Time.deltaTime); 
    }

    public void AggresiveStance()
    {
        StartCoroutine(FindEnemies());
        if(closeEnemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, closeEnemy.transform.position, summonedAttributesController.GetSpeed() * Time.deltaTime);
        }
    }

    private void FindCloseEnemy()
    {
        float minDistance = 100f;
        foreach (GameObject enemy in listOfEnemis)
        {
            if(Vector2.Distance(transform.position,enemy.transform.position) < minDistance)
            {
                minDistance = Vector2.Distance(transform.position, enemy.transform.position);
                closeEnemy = enemy;
            }
        }
    }

    private IEnumerator FindEnemies()
    {
        yield return new WaitForSeconds(1f);
        listOfEnemis = GameObject.FindGameObjectsWithTag("Enemy");
        FindCloseEnemy();
    }


}
