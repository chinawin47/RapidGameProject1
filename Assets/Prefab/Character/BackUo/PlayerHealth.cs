using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerHealth = 100; // Starting health for the player
    public int damageAmount = 15;  // Damage taken from collision with enemies
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Apply damage to the player
            TakeDamage(damageAmount);
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
        audioManager.PlaySFX(audioManager.death);
        Debug.Log("Player has died.");
        Destroy(gameObject); // Destroy the player GameObject
    }
}
