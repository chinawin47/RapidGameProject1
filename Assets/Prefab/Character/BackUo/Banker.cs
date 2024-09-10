using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Banker : MonoBehaviour
{
    public int damageAmount = 20;
    public Slider healthBar;
    public int health = 100;
    public int maxHealth = 100;
    public int healAmount = 10;  // Amount of health restored
    public float healInterval = 1.0f; // Time between health restoration
    private bool isHealing = false; // Flag to check if healing is active
    AudioManager audioManager;

    void Start()
    {
        // Initially hide the health bar
        healthBar.gameObject.SetActive(false);
        healthBar.maxValue = maxHealth; // Assuming max health is 100
        healthBar.value = health;
    }

    void Update()
    {
        // Update health bar value
        healthBar.value = health;

        // Show health bar if health is 80 or less
        if (health <= 80)
        {
            healthBar.gameObject.SetActive(true);
        }
        
    }
    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the fence when health is 0 or less
            audioManager.PlaySFX(audioManager.breakwall);
        }
    }

    // Gradually heal the fence while the player holds the collision
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isHealing)
        {
            StartCoroutine(HealOverTime());
        }
    }

    // Coroutine to heal the fence over time while player is colliding
    IEnumerator HealOverTime()
    {
        isHealing = true;
        
        while (health < maxHealth)
        {
            health += healAmount;
            health = Mathf.Min(health, maxHealth); // Ensure health does not exceed maxHealth
            yield return new WaitForSeconds(healInterval); // Wait for the specified time interval between healing

            // Break the loop if the player is no longer colliding
            if (health >= maxHealth) 
            {
                break;
            }
        }

        isHealing = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Banker"))
        {
            Banker banker = collision.gameObject.GetComponent<Banker>();
            if (banker != null)
            {
                banker.TakeDamage(damageAmount);
            }
        }
    }
}
