using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject respawnPoint;

    public float MoveSpeed;
    public float MinX;
    public float MaxX;

    public int health = 1;

    // Reference to the explosion prefab
    public GameObject explosionPrefab;

    public float respawnDelay = 1f; // time to wait before respawning

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 moveDir = Vector3.zero;

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > MinX)
        {
            moveDir = Vector3.left;
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < MaxX)
        {
            moveDir = Vector3.right;
        }

        transform.Translate (moveDir * MoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy collides with a bullet
        if (other.CompareTag("EnemyBullet"))
        {
            // Decrease the enemy's health
            health--;

            // Check if the enemy's health has reached 0
            if (health <= 0)
            {
                // Instantiate the explosion prefab
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);

                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<TrailRenderer>().enabled = false;

                StartCoroutine(Respawn());
            }

            // Destroy the bullet
            Destroy(other.gameObject);
        }
    }

    IEnumerator Respawn()
    {
        // Wait for the specified time before respawning
        yield return new WaitForSeconds(respawnDelay);

        // move the player to the respawn point
        transform.position = respawnPoint.transform.position;

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<TrailRenderer>().enabled = true;

    }
}
