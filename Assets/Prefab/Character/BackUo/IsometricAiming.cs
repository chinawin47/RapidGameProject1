using UnityEngine;

namespace BarthaSzabolcs.IsometricAiming
{
    public class IsometricAiming : MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings

        [SerializeField] private LayerMask groundMask;
        [SerializeField] private GameObject projectilePrefab;  // Reference to the projectile prefab
        [SerializeField] private float projectileSpeed = 10f;  // Speed of the projectile
        [SerializeField] private float shootRate = 0.2f;  // Time between shots

        #endregion
        #region Private Fields

        private Camera mainCamera;
        private float nextShootTime = 0f;  // Timer for managing shooting interval

        #endregion

        #endregion

        #region Methods

        #region Unity Callbacks

        private void Start()
        {
            // Cache the camera, Camera.main is an expensive operation.
            mainCamera = Camera.main;
        }

        private void Update()
        {
            // Continuous shooting functionality when the left mouse button is held down
            if (Input.GetMouseButton(0) && Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootRate;  // Set the next shoot time
            }
        }

        #endregion

        private void Shoot()
        {
            var (success, position) = GetMousePosition();
            if (success)
            {
                position.y = 2;
                
                // Instantiate the projectile
                var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                
                // Calculate direction from the shooter to the mouse position
                var direction = (position - transform.position).normalized;

                // Apply force to the projectile
                var rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = direction * projectileSpeed;
                }
                
                // Destroy the projectile after 5 seconds
                Destroy(projectile, 5f);
            }
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            {
                // The Raycast hit something, return with the position.
                return (success: true, position: hitInfo.point);
            }
            else
            {
                // The Raycast did not hit anything.
                return (success: false, position: Vector3.zero);
            }
        }

        #endregion
    }
}
