using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    [Header("Movement Settings")]
    public float moveSpeed = 1f;
    public bool canMove = true; // turret or tank mode

    [Header("Rotation Settings")]
    public float rotationSpeed = 90f; // degrees per second
    public float rotationDelay = 0.5f; 
    private float nextRotationTime = 0f; // time tracker for delayed rotation

    [Header("Shooting Settings")]
    public float minShootingRate = 1f;
    public float maxShootingRate = 3f;
    private float nextShootTime = 0f;
    

    void Update()
    {
        RotateTowardPlayer();
        if (canMove) MoveForward();
        Shooting();
    }

    void RotateTowardPlayer()
    {
        if (Time.time < nextRotationTime) return; // making rotation slightly slower when catching up to player's location

        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        targetAngle -= 90f; // making sure enemy looks to the left - if enemy is placed on left side, change this to -90

        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        // adding next rotation delay
        nextRotationTime = Time.time + rotationDelay;
    }

    void MoveForward()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime; // ai can only move forward or rotate
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
