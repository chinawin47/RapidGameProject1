using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    public class Fence : MonoBehaviour
{
    public int damageAmount = 20;
    public Slider healthBar;
    public int health = 100;

    void Update()
    {
        healthBar.value = health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the fence when health is 0 or less
        }
    }
     

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            Fence fence = collision.gameObject.GetComponent<Fence>();
            if (fence != null)
            {
                fence.TakeDamage(damageAmount);
            }
        }
    }
}

