using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShooter : MonoBehaviour
{
    public GameObject bulletPrefab;         // The bullet to be instantiated
    public Transform bulletSpawnPoint;      // The point where the bullet will be spawned
    public float bulletSpeed = 20f;         // Speed of the bullet
    public float shootingRange = 15f;       // Max range at which NPC can shoot
    public float shootRate = 1f;            // Time between shots
    public int damageAmount = 10;           // Damage dealt by each bullet
    public LayerMask enemyLayerMask;        // Layer mask to identify enemies

    private float nextShootTime = 0f;       // Cooldown between shots
    private Transform targetEnemy;          // Enemy currently being targeted

    void Update()
    {
        FindTargetEnemy();

        if (targetEnemy != null)
        {
            // Rotate to face the target
            RotateTowards(targetEnemy.position);

            // Check if it's time to shoot and the enemy is within range
            if (Time.time >= nextShootTime && Vector3.Distance(transform.position, targetEnemy.position) <= shootingRange)
            {
                ShootAtEnemy();
                nextShootTime = Time.time + shootRate; // Set the next shoot time
            }
        }
    }

    // Method to find the closest enemy within range
    private void FindTargetEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootingRange, enemyLayerMask);
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (Collider collider in hitColliders)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = collider.transform;
            }
        }

        targetEnemy = nearestEnemy;
    }

    // Method to rotate the NPC towards the enemy
    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Method to shoot a bullet towards the enemy
    private void ShootAtEnemy()
    {
        // Instantiate a bullet and set its direction
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null && targetEnemy != null)
        {
            Vector3 direction = (targetEnemy.position - bulletSpawnPoint.position).normalized;
            rb.velocity = direction * bulletSpeed;
        }

        // Optionally, destroy the bullet after some time
        Destroy(bullet, 5f);
    }
}
