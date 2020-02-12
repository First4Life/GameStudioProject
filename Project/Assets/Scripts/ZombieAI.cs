using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform playerPos;
    NavMeshAgent pathFinding;

    float distanceToPlayer;
    Vector3 oldPlayerPos;

    void Start()
    {
        pathFinding = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, playerPos.position);
        if(distanceToPlayer > 15f)
        {
            pathFinding.speed = 10f;
        }
        if (distanceToPlayer > 2f)
        {
            pathFinding.speed = 3.5f;
            UpdatePath();
        }
    }

    void UpdatePath()
    {
        if (playerPos.position != oldPlayerPos)
        {
            Vector3 differencePosition = this.transform.position - playerPos.position;
            Vector3 targetDirection = differencePosition.normalized;
            Vector3 targetPosition = playerPos.position + (targetDirection * 2f);

            pathFinding.SetDestination(targetPosition);
            oldPlayerPos = playerPos.position;

            Debug.Log("Created new path to player");
        }
    }
}
