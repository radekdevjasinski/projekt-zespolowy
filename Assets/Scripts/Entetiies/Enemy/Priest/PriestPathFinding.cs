using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PriestPathFinding : MonoBehaviour
{
    Transform target;
    public NavMeshAgent agent;
    public float safeDistance = 5.0f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void Move()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < safeDistance)
        {
            Vector3 directionToTarget = transform.position - target.position;
            Vector3 fleePosition = transform.position + directionToTarget.normalized * safeDistance;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(fleePosition, out hit, safeDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
        else
        {
            agent.ResetPath();
        }
    }
}
