using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;        // The bullet prefab
    public Transform shootPoint;           // The point where bullets will be spawned
    public float shootForce = 10f;         // The force applied to the bullet
    public int maxAmmo = 10;               // Maximum ammo per magazine
    public int currentAmmo;                // Current ammo in the magazine
    public float reloadTime = 2f;          // Time to reload in seconds
    public bool isReloading = false;       // Whether the player is currently reloading

    void Start()
    {
        // Initialize current ammo
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        // Check for shoot input and if the player is not reloading
        if (Input.GetMouseButtonDown(0) && !isReloading)
        {
            Shoot();
        }

        // Check for reload input
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        // Check if the player has ammo
        if (currentAmmo > 0)
        {
            // Create the bullet at the shoot point
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

            // Apply force to the bullet's Rigidbody to shoot it
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
            }

            // Reduce ammo count
            currentAmmo--;

            // Destroy the bullet after 5 seconds to avoid clutter
            Destroy(bullet, 5f);
        }
        else
        {
            Debug.Log("Out of ammo! Press R to reload.");
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;   // Set the reloading flag
        Debug.Log("Reloading...");

        // Simulate reload time
        yield return new WaitForSeconds(reloadTime);

        // Refill ammo
        currentAmmo = maxAmmo;
        isReloading = false;  // Reset the reloading flag

        Debug.Log("Reload complete!");
    }
}
