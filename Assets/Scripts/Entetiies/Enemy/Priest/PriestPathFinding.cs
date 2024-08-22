using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PriestPathFinding : MonoBehaviour
{
    Transform target;
    public NavMeshAgent agent;
    public float safeDistance = 5.0f;
    public float chaseDistance = 15f;
    public float chaseSpeedModifier = 0.7f;
    public bool isCasting;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        isCasting = false;
    }

    public void Move()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < safeDistance && !isCasting)
        {
            Vector3 directionToTarget = transform.position - target.position;
            Vector3 fleePosition = transform.position + directionToTarget.normalized * agent.speed;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(fleePosition, out hit, safeDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
        else if (distance > chaseDistance && !isCasting)
        {
            Vector3 directionToTarget = transform.position - target.position;
            Vector3 chasePosition = transform.position - directionToTarget.normalized * agent.speed * chaseSpeedModifier;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(chasePosition, out hit, chaseDistance, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
        }
        else
        {
            agent.ResetPath();
            agent.velocity = Vector3.zero;
        }

    }
}
