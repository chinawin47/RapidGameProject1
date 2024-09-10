using System.Collections;
using UnityEngine;

namespace BarthaSzabolcs.IsometricAiming
{
    public class IsometricAiming : MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed = 10f;
        [SerializeField] private float shootRate = 0.2f;
        [SerializeField] private int maxAmmo = 6;           // Max bullets before reloading
        [SerializeField] private float reloadTime = 2f;     // Time it takes to reload
        [SerializeField] private TextMesh reloadText;       // TextMesh to display reloading message

        #endregion

        #region Private Fields

        private Camera mainCamera;
        private float nextShootTime = 0f;
        private int currentAmmo;
        private bool isReloading = false;

        #endregion

        #endregion

        #region Methods

        #region Unity Callbacks

        private void Start()
        {
            mainCamera = Camera.main;
            currentAmmo = maxAmmo; // Initialize with full ammo
            if (reloadText != null)
                reloadText.gameObject.SetActive(false); // Hide the reload text at the start
        }

        private void Update()
        {
            if (isReloading)
                return; // Block shooting if reloading

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload()); // Reload when out of ammo
                return;
            }

            if (Input.GetMouseButton(0) && Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootRate;
            }
        }

        #endregion

        private void Shoot()
        {
            var (hitEnemy, hitPosition) = GetMouseClickTarget();
            
            if (hitEnemy)
            {
                Debug.Log("Enemy hit!");
            }
            else
            {
                Debug.Log("Shooting at ground or empty space");
            }

            if (hitPosition != Vector3.zero)
            {
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                var direction = (hitPosition - transform.position).normalized;
                var rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = direction * projectileSpeed;
                }
                Destroy(projectile, 5f);

                currentAmmo--; // Reduce ammo after shooting
                Debug.Log($"Ammo left: {currentAmmo}");
            }
        }

        private IEnumerator Reload()
        {
            isReloading = true;
            if (reloadText != null)
                reloadText.gameObject.SetActive(true); // Show the reload text

            Debug.Log("Reloading...");

            yield return new WaitForSeconds(reloadTime); // Wait for the reload time

            currentAmmo = maxAmmo; // Refill ammo
            isReloading = false;
            if (reloadText != null)
                reloadText.gameObject.SetActive(false); // Hide the reload text

            Debug.Log("Reload complete.");
        }

        private (bool hitEnemy, Vector3 position) GetMouseClickTarget()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var enemyHitInfo, Mathf.Infinity, enemyMask))
            {
                return (hitEnemy: true, position: enemyHitInfo.point);
            }
            
            if (Physics.Raycast(ray, out var groundHitInfo, Mathf.Infinity, groundMask))
            {
                return (hitEnemy: false, position: groundHitInfo.point);
            }

            return (hitEnemy: false, position: Vector3.zero);
        }

        #endregion
    }
}
