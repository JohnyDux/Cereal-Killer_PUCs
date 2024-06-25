using UnityEngine;

public class VisionDetection : MonoBehaviour
{
    public float viewDistance = 10f;
    public float viewAngle = 90f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    AIController character;
    public AIController detective;

    

    void Update()
    {
        if (character!= null && !character.isAlive)
        {
            detective.agent.SetDestination(character.transform.position);
        }
        else
        {
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        foreach (Collider target in targetsInViewRadius)
        {
            Transform targetTransform = target.transform;
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    Debug.Log("Target in sight: " + targetTransform.name);

                    character = targetTransform.GetComponent<AIController>();
                }
            }
        }
    }
}
