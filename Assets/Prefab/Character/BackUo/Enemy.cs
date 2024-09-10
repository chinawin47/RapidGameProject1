using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 10;
    public float bounceForce = 5f; // Adjust this value to control the bounce strength
    public int enemyHealth = 50;   // Enemy health
    public float knockbackDelay = 0.5f;  // Delay after knockback

    private bool isKnockedBack = false; // Check if the enemy is knocked back
    private Rigidbody rb;               // Cache the Rigidbody for efficiency

    // Optional: Reference to a sound effect or particle system for hit effects
    public AudioSource hitSound;        // Drag an AudioSource here in the inspector
    public GameObject hitEffectPrefab;  // Drag a prefab here in the inspector

    public event Action<GameObject> OnEnemyDestroyed; // Event to notify when the enemy is destroyed
    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        // Cache the Rigidbody component for efficiency
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on enemy.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence") && !isKnockedBack)
        {
            Debug.Log("Fence hit!");
            Fence fence = collision.gameObject.GetComponent<Fence>();

            if (fence != null)
            {
                // Damage the fence
                fence.TakeDamage(damageAmount);
                Debug.Log("Damage dealt to fence: " + damageAmount);

                // Apply bounce force
                if (rb != null)
                {
                    Vector3 bounceDirection = collision.contacts[0].normal; // Get the collision normal
                    rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

                    // Start knockback delay
                    StartCoroutine(KnockbackCooldown());
                }

                // Optionally, play sound or particles on hit
                PlayHitEffect();
            }
            else
            {
                Debug.LogWarning("Fence script not found on: " + collision.gameObject.name);
            }
        }
    }

    // Knockback cooldown coroutine to prevent the enemy from bouncing repeatedly
    private IEnumerator KnockbackCooldown()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(knockbackDelay);
        isKnockedBack = false;
    }

    // Method to play hit effects (sound, particles, etc.)
    private void PlayHitEffect()
    {
        if (hitSound != null)
        {
            hitSound.Play();
        }

        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        Debug.Log("Playing hit effect.");
    }

    // Example method to damage the enemy itself
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy health: " + enemyHealth);

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Logic for the enemy dying
        audioManager.PlaySFX(audioManager.zombiedeath);
        Debug.Log("Enemy has died.");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // Trigger the event when the enemy is destroyed
        OnEnemyDestroyed?.Invoke(gameObject);
    }
}
