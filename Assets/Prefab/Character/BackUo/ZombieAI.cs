using System.Collections;
using UnityEngine;
using UnityEngine.AI;  // Import the NavMesh namespace

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieAI : MonoBehaviour
{
    public GameObject Target;        // Primary target
    public GameObject Target2;       // Secondary target
    public float stopDuration = 1f;  // Time to stop after hitting the fence
    private bool isStopped = false;  // To track if the zombie is currently stopped
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component missing from this GameObject");
        }
    }

    private void Update()
    {
        // If the agent is stopped, don't set the destination
        if (isStopped || navMeshAgent == null || !navMeshAgent.isOnNavMesh)
        {
            return;
        }

        // Prioritize Target over Target2
        if (Target != null)
        {
            // Set the destination to the primary target's position
            navMeshAgent.SetDestination(Target.transform.position);
        }
        else if (Target2 != null)
        {
            // If no primary target, set the destination to the secondary target's position
            navMeshAgent.SetDestination(Target2.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            // Trigger stop movement when colliding with the fence
            StartCoroutine(StopMovement());
        }
    }

    private IEnumerator StopMovement()
    {
        // Ensure the NavMeshAgent stops movement
        isStopped = true;
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.isStopped = true;
        }

        // Wait for the stop duration before resuming movement
        yield return new WaitForSeconds(stopDuration);

        // Resume movement if the NavMeshAgent is on the NavMesh
        isStopped = false;
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.isStopped = false;
        }
    }
}
