using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PriestPathFinding : MonoBehaviour
{
    Transform target;
    public NavMeshAgent agent;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void Move()
    {
        Vector3 directionAwayFromPlayer = transform.position - target.position;
        Vector3 newDestination = transform.position + directionAwayFromPlayer;

        agent.SetDestination(newDestination);
    }
}
