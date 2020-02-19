using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform playerPos;
    NavMeshAgent pathFinding;
    NavMeshObstacle obstacle;

    float distanceToPlayer;
    Vector3 oldPlayerPos;

    void Start()
    {
        pathFinding = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();

        obstacle.enabled = false;

        UpdatePath();
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
            obstacle.enabled = false;
            pathFinding.enabled = true;

            pathFinding.speed = 3.5f;
            StartCoroutine(WaitTimeToUpdate());

            if(pathFinding.pathStatus == NavMeshPathStatus.PathPartial)
            {
                pathFinding.enabled = false;
                obstacle.enabled = true;

                UpdatePath();
            }
        }
        else
        {
            obstacle.enabled = true;
            pathFinding.enabled = false;
        }
    }

    void UpdatePath()
    {
        if (playerPos.position != oldPlayerPos)
        {
            /*Vector3 differencePosition = this.transform.position - playerPos.position;
            Vector3 targetDirection = differencePosition.normalized;
            Vector3 targetPosition = playerPos.position + (targetDirection * 2f);*/

            pathFinding.SetDestination(playerPos.position);
            oldPlayerPos = playerPos.position;
        }
    }

    IEnumerator WaitTimeToUpdate()
    {
        yield return new WaitForSeconds(5f);
        UpdatePath();
    }
}
