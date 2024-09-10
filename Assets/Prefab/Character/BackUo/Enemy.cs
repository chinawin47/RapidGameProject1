using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 10;
    public float bounceForce = 5f;
    public int enemyHealth = 50;
    public float knockbackDelay = 0.5f;

    private bool isKnockedBack = false;
    private Rigidbody rb;

    public AudioSource hitSound;
    public GameObject hitEffectPrefab;

<<<<<<< HEAD
    public event Action<GameObject> OnEnemyDestroyed;

    
=======
    public event Action<GameObject> OnEnemyDestroyed; // Event to notify when the enemy is destroyed
>>>>>>> Tungkus

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on enemy.");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence") && !isKnockedBack)
        {
            Debug.Log("Fence hit!");
            Fence fence = collision.gameObject.GetComponent<Fence>();

            if (fence != null)
            {
<<<<<<< HEAD
=======
                // Damage the fence continuously while in contact
>>>>>>> Tungkus
                fence.TakeDamage(damageAmount);
                Debug.Log("Damage dealt to fence: " + damageAmount);

                if (rb != null)
                {
                    Vector3 bounceDirection = collision.contacts[0].normal;
                    rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
                    StartCoroutine(KnockbackCooldown());
                }

                PlayHitEffect();
            }
            else
            {
                Debug.LogWarning("Fence script not found on: " + collision.gameObject.name);
            }
        }
    }

    private IEnumerator KnockbackCooldown()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(knockbackDelay);
        isKnockedBack = false;
    }

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
<<<<<<< HEAD
=======
        // Logic for the enemy dying
>>>>>>> Tungkus
        Debug.Log("Enemy has died.");
        ScoreManager.scoreValue += 1;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnEnemyDestroyed?.Invoke(gameObject);
    }
}
