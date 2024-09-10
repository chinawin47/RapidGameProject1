using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100; // Starting health for the player
    public int damageAmount = 15;  // Damage taken from collision with enemies
    public float damageInterval = 1f; // Time interval between taking damage

    private bool canTakeDamage = true; // Control damage interval

    private void OnCollisionStay(Collision collision)
    {
        // Check if the player collides with an enemy
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            // Apply damage to the player
            TakeDamage(damageAmount);
            StartCoroutine(DamageCooldown()); // Add a cooldown between damage ticks
        }
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Player health: " + playerHealth);

        // Check if the player's health has dropped to zero or below
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Logic for the player dying
        Debug.Log("Player has died.");
        Destroy(gameObject); // Destroy the player GameObject
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageInterval); // Wait for the damage interval
        canTakeDamage = true;
    }
}
