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

    NavMeshPath currentPath;
    float range = 10.0f;

    const float movementThreshold = 0.5f;
    Vector3 closestPosition;

    public float health = 100, maxHealth = 100;
    public float speed = 1f;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        pathFinding = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();

        obstacle.enabled = false;

        ScaleZombie();
        StartCoroutine(UpdatePath());
    }
    
    void Update()
    {
        distanceToPlayer = Vector3.Distance(this.transform.position, playerPos.position);

        if (distanceToPlayer > 2f)
        {
            obstacle.enabled = false;
            pathFinding.enabled = true;

            pathFinding.speed = speed;
            StartCoroutine(UpdatePath());

            if(pathFinding.pathStatus == NavMeshPathStatus.PathPartial)
            {
                if (pathFinding.velocity.x >= movementThreshold && pathFinding.velocity.y >= movementThreshold && pathFinding.velocity.z >= movementThreshold)
                {
                    if (closestPosition == null)
                    {
                        if (FindRandomPoint(playerPos.position, range, out closestPosition))
                        {
                            Debug.DrawRay(closestPosition, Vector3.up, Color.blue, 1.0f);

                            currentPath = new NavMeshPath();
                            pathFinding.CalculatePath(closestPosition, currentPath);

                            pathFinding.SetPath(currentPath);
                        }
                    }
                }
                else
                {
                    pathFinding.velocity = new Vector3(0,0,0);

                    Vector3 targetRotation = playerPos.position - transform.position;
                    Vector3 newRotation = Vector3.RotateTowards(transform.forward, targetRotation, 5.0f * Time.deltaTime, 0.0f);
                    transform.rotation = Quaternion.LookRotation(newRotation);

                    StartCoroutine(UpdatePath());

                }
            }
        }
        else
        {
            obstacle.enabled = true;
            pathFinding.enabled = false;

            Vector3 targetRotation = playerPos.position - transform.position;
            Vector3 newRotation = Vector3.RotateTowards(transform.forward, targetRotation, 5.0f * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newRotation);
        }
    }

    IEnumerator UpdatePath()
    {
        yield return 0;
        if (playerPos.position != oldPlayerPos)
        {
            pathFinding.enabled = true;
            obstacle.enabled = false;

            currentPath = new NavMeshPath();

            pathFinding.CalculatePath(playerPos.position, currentPath);
            pathFinding.SetPath(currentPath);
            oldPlayerPos = playerPos.position;
        }
    }

    bool FindRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public void AdjustHealth(int amount)
    {
        health += amount;
    }

    void ScaleZombie()
    {
        RoundSystem system = GameObject.FindObjectOfType<RoundSystem>();

        maxHealth = 100 * Mathf.Sqrt(system.currentRound);
        health = maxHealth;
    }
}
