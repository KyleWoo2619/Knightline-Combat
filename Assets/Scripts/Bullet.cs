using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public float maxDistance = 10; // Max distance before destroying itself

    private Vector2 startPosition;
    private float conqueredDistance = 0;
    private Rigidbody2D rb2d;
    private ScoreManager scoreManager;

    // Audio Sources (Auto-detected)
    private AudioSource bulletSpawnSound;
    private AudioSource hitPlayerSound;
    private AudioSource hitEnemySound;
    private AudioSource hitWallSound;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found");
        }

        // **Automatically Find Sounds in Scene**
        bulletSpawnSound = GameObject.Find("BulletSound")?.GetComponent<AudioSource>();
        hitPlayerSound = GameObject.Find("PlayerHit")?.GetComponent<AudioSource>();
        hitEnemySound = GameObject.Find("EnemyHit")?.GetComponent<AudioSource>();
        hitWallSound = GameObject.Find("WallHit")?.GetComponent<AudioSource>();
    }

    public void Initialize() // Create bullet within scene
    {
        startPosition = transform.position;
        rb2d.velocity = transform.up * speed;

        // Play bullet spawn sound if found
        bulletSpawnSound?.Play();
    }

    private void Update()
    {
        conqueredDistance = Vector2.Distance(transform.position, startPosition);
        if (conqueredDistance > maxDistance)
        {
            DisableObject();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerBehaviorA player = collision.GetComponent<PlayerBehaviorA>();
            if (player != null && !player.IsShielded)
            {
                scoreManager?.AddEnemyScore();
                hitPlayerSound?.Play(); // Play hit sound for Player
            }
            Debug.Log("Player Hit");
            //scoreManager?.AddEnemyScore();

            // Play hit sound for Player
            //hitPlayerSound?.Play();
        }
        else if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit");
            scoreManager?.AddPlayerScore();

            // Play hit sound for Enemy
            hitEnemySound?.Play();
        }
        else
        {
            // Play hit sound for walls/other objects
            hitWallSound?.Play();
        }

        DisableObject();
    }

    private void DisableObject()
    {
        rb2d.velocity = Vector2.zero;
        Destroy(gameObject);
    }
}
