using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossLevelAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    private SpriteRenderer spriteRenderer;

    [Header("UI")]
    public Slider bossHealthBar; // Boss Health Bar

    [Header("Movement Settings")]
    public float moveSpeed = 1f;
    public bool canMove = true;
    public float distanceBetween = 5f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 90f;

    [Header("Shooting Settings")]
    public float minShootingRate = 1f;
    public float maxShootingRate = 3f;
    private float nextShootTime = 0f;

    [Header("Wall Detection")]
    public float wallCheckDistance = 1f;
    public LayerMask wallLayer;
    public float avoidanceForce = 2f;

    [Header("Health Settings")]
    public int maxHP = 5;
    private int currentHP;
    public bool isBoss = false;
    public float blinkDuration = 2f;

    [Header("Audio Settings")]
    public AudioSource explosionSound; // Assign explosion sound in the Inspector

    private ScoreManager scoreManager;

    void Start()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        scoreManager = FindObjectOfType<ScoreManager>();

        // **Initialize HP UI if Boss**
        if (isBoss && bossHealthBar != null)
        {
            bossHealthBar.gameObject.SetActive(true);
            bossHealthBar.maxValue = maxHP;
            bossHealthBar.value = currentHP;
        }
    }

    void Update()
    {
        if (currentHP > 0)
        {
            RotateTowardPlayer();
            if (canMove) MoveTowardPlayer();
            Shooting();
        }
    }

    void RotateTowardPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MoveTowardPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < distanceBetween)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 moveDirection = direction;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, wallCheckDistance, wallLayer);
            if (hit.collider != null)
            {
                Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
                moveDirection = (direction + avoidanceDirection * avoidanceForce).normalized;
            }

            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + moveDirection, moveSpeed * Time.deltaTime);
        }
    }

    void Shooting()
    {
        if (Time.time >= nextShootTime)
        {
            ShootBullet();
            nextShootTime = Time.time + Random.Range(minShootingRate, maxShootingRate);
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Bullet>().Initialize();
    }

    // **Handle Damage When Hit by a Fireball**
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fireball"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        UpdateBossHPBar();

        if (currentHP <= 0)
        {
            StartCoroutine(BlinkAndDeactivate());
        }
    }

    private void UpdateBossHPBar()
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.value = currentHP;
        }
    }

    IEnumerator BlinkAndDeactivate()
    {
        float timer = 0f;
        bool isVisible = true;

        if (explosionSound != null)
        {
            explosionSound.Play();
        }

        while (timer < blinkDuration)
        {
            isVisible = !isVisible;
            spriteRenderer.enabled = isVisible;
            yield return new WaitForSeconds(0.2f);
            timer += 0.2f;
        }

        gameObject.SetActive(false);

        if (isBoss && scoreManager != null)
        {
            scoreManager.WinGame();
        }
    }
}
