using UnityEngine;

public class NewEnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    [Header("Movement Settings")]
    public float moveSpeed = 1f;
    public bool canMove = true; // turret or tank mode
    public float distanceBetween = 5f; // distance to start moving toward the player

    [Header("Rotation Settings")]
    public float rotationSpeed = 90f; // degrees per second

    [Header("Shooting Settings")]
    public float minShootingRate = 1f;
    public float maxShootingRate = 3f;
    private float nextShootTime = 0f;

    [Header("Wall Detection")]
    public float wallCheckDistance = 1f;
    public LayerMask wallLayer;
    public float avoidanceForce = 2f;

    void Update()
    {
        RotateTowardPlayer();
        if (canMove) MoveTowardPlayer();
        Shooting();
    }

    void RotateTowardPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Adjusted to look forward in direction
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Smoothly rotate toward the player
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MoveTowardPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < distanceBetween)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 moveDirection = direction;

            // Wall detection and avoidance
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, wallCheckDistance, wallLayer);
            if (hit.collider != null)
            {
                // If a wall is detected, adjust the move direction to avoid it
                Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
                moveDirection = (direction + avoidanceDirection * avoidanceForce).normalized;
            }

            // Move toward the player (or around the wall)
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

}