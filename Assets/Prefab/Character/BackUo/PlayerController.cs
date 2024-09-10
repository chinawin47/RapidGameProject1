using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    private bool isShooting = false; // Track if the player is shooting

    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); // For 3D, if you're using 2D, change to Rigidbody2D
        sr = gameObject.GetComponent<SpriteRenderer>(); // Ensure sr is assigned
        rb.freezeRotation = true; // Freeze rotation to avoid flipping issues
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z; // Ensure we're comparing on the same Z plane

        // Determine the direction from the player to the mouse
        Vector3 directionToMouse = mousePos - transform.position;

        // Check if the mouse is to the right or left of the player
        if (directionToMouse.x < 0)
        {
            sr.flipX = true;  // Mouse is to the left of the player
        }
        else
        {
            sr.flipX = false; // Mouse is to the right of the player
        }

        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;

        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        // Check if the player is shooting
        isShooting = Input.GetMouseButtonDown(0); // Example for left mouse button (replace with your shooting logic)

        if (!isShooting) // Only move if the player is not shooting
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector3 moveDir = new Vector3(x, 0, y);
            rb.velocity = moveDir * speed;

            if (x < 0)
            {
                sr.flipX = true;  // Flip sprite to the left
            }
            else if (x > 0)
            {
                sr.flipX = false; // Flip sprite to the right
            }
        }


    }
}
