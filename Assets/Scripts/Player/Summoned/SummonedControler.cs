using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonedControler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public SummonedStance summonedStance;
    public List<Summoned> summons;
    void Start()
    {
        
    }

    void Update()
    {
        if(summons.Count>0) 
        {
            if (summonedStance == SummonedStance.Defense)
            {
                /*foreach (Summoned summoned in summons)
                {
                    summoned.DeffenseStance();
                }*/
                Vector2 newPosition = (Vector2)player.transform.position + new Vector2(1f, 0);
                summons[0].DeffenseStance(newPosition);
                newPosition = (Vector2)player.transform.position + new Vector2(-1f, 0);
                summons[1].DeffenseStance(newPosition);
            }
            else
            {
                foreach (Summoned summoned in summons)
                {
                    summoned.AggresiveStance();
                }
            }
        }
    }
}
