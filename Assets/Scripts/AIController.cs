using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#region AILogic
public enum CharacterClass
{
    NPC,
    Detective
}

public enum SignType
{
    KillingSign,
    DialogueSign
}

public class AIController : MonoBehaviour
{
    [SerializeField] float moveRadius = 10f; // Radius within which the agent will move
    [SerializeField] float timeBetweenMoves = 5f; // Time in seconds between each new random destination
    [SerializeField] float minDistance = 5f; // Minimum distance between consecutive points

    [SerializeField] Animator animator;
    public NavMeshAgent agent;
    public bool hasPath;
    private Vector3 lastDestination;
    [SerializeField] private float timer;

    public bool isAlive;

    CharacterClass ThisCharacterClass;

    public CharacterClass characterClass;

    public NPCInteraction nPC;

    void Start()
    {
        ThisCharacterClass = characterClass;
        timer = timeBetweenMoves;
        lastDestination = transform.position;

        isAlive = true;
    }

    void Update()
    {
        //NPC Logic
        if(ThisCharacterClass == CharacterClass.NPC)
        {
            if (isAlive)
            {
                if (nPC.talking == false)
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
                else
                {
                    animator.SetBool("Walking", false);
                } 
            }
            else
            {
                animator.SetBool("Walking", false);
                animator.SetTrigger("Death");
            }
        }
        //Detective Logic
        else if(ThisCharacterClass == CharacterClass.Detective)
        {
            if (nPC.talking == false)
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
            else
            {
                animator.SetBool("Walking", false);
            }
        }
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
#endregion