using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageAmount = 25; // Amount of damage dealt to the enemy

    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collided with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Try to get the Enemy component from the object
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Reduce the enemy's health
                enemy.TakeDamage(damageAmount);
            }
        }

        // Destroy the bullet upon collision with any object
        Destroy(gameObject);
    }
}
