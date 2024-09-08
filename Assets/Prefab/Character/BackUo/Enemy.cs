using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 10;
    public float bounceForce = 5f; // Adjust this value to control the bounce strength

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Fence"))
        {
            Debug.Log("Fence hit!");
            Fence fence = collision.gameObject.GetComponent<Fence>();
            if (fence != null)
            {
                fence.TakeDamage(damageAmount);
                Debug.Log("Damage dealt: " + damageAmount);

                // Apply bounce force
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 bounceDirection = collision.contacts[0].normal; // Get the collision normal
                    rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
                }
            }
            else
            {
                Debug.Log("Fence script not found on: " + collision.gameObject.name);
            }
        }
    }
}
