using System.Collections;
using UnityEngine;
using UnityEngine.AI;  // Import the NavMesh namespace

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieAI : MonoBehaviour
{
    public GameObject Target;
    public GameObject Target2;
    public float stopDuration = 1f; // Time to stop after hitting the fence
    private bool isStopped = false; // To track if the zombie is currently stopped

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        // Get the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component missing from this GameObject");
        }
    }

    void Update()
    {
        if (!isStopped && navMeshAgent != null)
        {
            if (Target != null)
            {
                // Set the destination of the NavMeshAgent to the target's position
                navMeshAgent.SetDestination(Target.transform.position);
            }
           if (Target2 != null)
            {
                // Set the destination of the NavMeshAgent to the target's position
                navMeshAgent.SetDestination(Target2.transform.position);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            StartCoroutine(StopMovement());
        }
    }

    private IEnumerator StopMovement()
    {
        isStopped = true; // Stop the zombie from moving
        navMeshAgent.isStopped = true; // Ensure NavMeshAgent is stopped

        yield return new WaitForSeconds(stopDuration); // Wait for the specified duration

        isStopped = false; // Resume movement
        navMeshAgent.isStopped = false; // Resume NavMeshAgent
    }
}