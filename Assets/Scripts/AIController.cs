using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public float moveRadius = 10f; // Radius within which the agent will move
    public float timeBetweenMoves = 5f; // Time in seconds between each new random destination
    public float minDistance = 5f; // Minimum distance between consecutive points

    public Animator animator;
    public NavMeshAgent agent;
    [SerializeField] bool hasPath;
    private Vector3 lastDestination;
    private float timer;

    void Start()
    {
        timer = timeBetweenMoves;
        lastDestination = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        hasPath = agent.hasPath;

        if (timer >= timeBetweenMoves)
        {
            Vector3 newDestination = GetRandomPoint(transform.position, moveRadius);
            if (newDestination != Vector3.zero && Vector3.Distance(newDestination, lastDestination) >= minDistance)
            {
                agent.SetDestination(newDestination);
                
                lastDestination = newDestination;
                timer = 0f;
            }
        }

        // Rotate the GameObject to face the direction of movement
        if (agent.velocity.sqrMagnitude > 0.1f) // Check if the agent is moving
        {
            Vector3 direction = agent.velocity.normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        animator.SetBool("Walking", hasPath);
    }

    Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 30; i++) // Try 30 times to find a valid point
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += center;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                return hit.position;
            }
        }
        return Vector3.zero;
    }
}
