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

    float x = Input.GetAxis("Horizontal");
    float y = Input.GetAxis("Vertical");

    // Log the x value for debugging
    Debug.Log("Horizontal Input: " + x);

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
    // Check for mouse click to fire the weapon
}
}