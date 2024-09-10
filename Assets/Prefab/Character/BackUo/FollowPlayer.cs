using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Import the NavMesh namespace

public class FollowPlayer : MonoBehaviour
{
    public NavMeshAgent enemy; // Drag your NavMeshAgent component here in the Inspector
    public string playerTag = "Player"; // Tag to identify the player

    private Transform playerTransform; // Cache for the player Transform

    void Start()
    {
        // Initialize or verify components and find the player
        if (enemy == null)
        {
            Debug.LogError("NavMeshAgent component is not assigned.");
        }

        // Find the player GameObject by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        
        if (playerObject != null)
        {
            playerTransform = playerObject.transform; // Cache the player's Transform
        }
        else
        {
            Debug.LogError("No GameObject with tag '" + playerTag + "' found.");
        }
    }

    void Update()
    {
        if (enemy != null && playerTransform != null)
        {
            enemy.SetDestination(playerTransform.position);
        }
    }
}
