using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Reference to the player's bullet prefab
    public GameObject bulletPrefab;

    // Fire rate of the player's bullets
    public float fireRate = 0.5f;

    // Speed at which the bullets will move
    public float bulletSpeed = 10f;

    // The position where the bullets will be instantiated
    public Transform firePosition;

    // Timer to track the time between shots
    private float timer;

    void Update()
    {
        // Increase the timer with the time since the last frame
        timer += Time.deltaTime;

        // Check if the player presses the fire button
        if (Input.GetButton("Fire1"))
        {
            // Check if the fire rate timer has expired
            if (timer >= fireRate)
            {
                // Reset the timer
                timer = 0;

                // Instantiate a new bullet at the fire position
                GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);

                // Get the Rigidbody2D component of the bullet
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                // Set the velocity of the bullet to move upward
                rb.velocity = new Vector2(0, bulletSpeed);
            }
        }
    }
}