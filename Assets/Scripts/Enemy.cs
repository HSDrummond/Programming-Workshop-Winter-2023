using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float TargetY;
    public float MoveSpeed;
    public float MinX;
    public float MaxX;

    bool movingLeft;

    public int health = 1;

    // Reference to the explosion prefab
    public GameObject explosionPrefab;

    // Reference to the player
    public GameObject player;
    public GameObject bulletPrefab;

    // Fire rate of the enemy's bullets
    public float fireRate = 2f;

    // Speed at which the bullets will move
    public float bulletSpeed = 10f;

    // Timer to track the time between shots
    private float timer;

    private void Start ()
    {
        // Randomly start moving either left or right
        movingLeft = Random.Range (0.0f, 1.0f) < 0.5f;
    }

    private void Update ()
    {
        // Increase the timer with the time since the last frame
        timer += Time.deltaTime;

        // Check if the fire rate timer has expired
        if (timer >= fireRate)
        {
            // Reset the timer
            timer = 0;

            // Instantiate a new bullet at the enemy's position
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Get the Rigidbody2D component of the bullet
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Set the velocity of the bullet to move straight down
            rb.velocity = new Vector2(0, -bulletSpeed);
        }

        if (transform.position.y > TargetY)
        {
            // Enemy starts offscreen, so move down until it's in the right place
            transform.position -= Vector3.up * Time.deltaTime * MoveSpeed;
            return;
        }

        // Move
        Vector3 movementDirection = movingLeft ? Vector3.left : Vector3.right;
        transform.position += movementDirection * Time.deltaTime * MoveSpeed;

        // If moved to the left/right all the way, turn around
        if (transform.position.x < MinX || transform.position.x > MaxX)
        {
            movingLeft = !movingLeft;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy collides with a bullet
        if (other.CompareTag("Bullet"))
        {
            // Decrease the enemy's health
            health--;

            // Check if the enemy's health has reached 0
            if (health <= 0)
            {
                // Instantiate the explosion prefab
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);

                // Destroy the enemy
                Destroy(gameObject);
            }

            // Destroy the bullet
            Destroy(other.gameObject);
        }
    }
}
