using UnityEngine;
using System.Collections;

public class EnemyControlle : MonoBehaviour
{
    [Header("References")]
    public Transform player; // Assign the player in the Inspector
    public Transform bulletSpawn; // Bullet spawn position
    public GameObject bulletPrefab; // Bullet prefab

    [Header("Movement Settings")]
    public float moveSpeed = 2f; // AI movement speed
    public bool canMove = true; // Toggle movement

    [Header("Rotation Settings")]
    public float rotationSpeed = 300f; // Increased rotation speed for quick alignment
    public float stopRotationThreshold = 2f; // Degrees within which AI considers itself aligned

    [Header("Shooting Settings")]
    public float minShootingRate = 1f;
    public float maxShootingRate = 3f;
    private float nextShootTime = 0f;

    private bool isRotating = false; // Prevents movement while rotating

    void Start()
    {
        StartCoroutine(AI_BehaviorLoop());
    }

    IEnumerator AI_BehaviorLoop()
    {
        while (true)
        {
            yield return StartCoroutine(RotateTowardPlayer()); // Stop and rotate first
            if (canMove) yield return StartCoroutine(MoveForward()); // Then move if allowed
        }
    }

    IEnumerator RotateTowardPlayer()
    {
        isRotating = true; // Stop movement while rotating

        while (true)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Adjust for Unity's 2D forward being +Y
            targetAngle -= 90f;

            // Smoothly rotate towards the target angle
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);

            // Stop rotating if close enough to the correct angle
            if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle)) < stopRotationThreshold)
                break;

            yield return null; // Continue rotating
        }

        isRotating = false; // Allow movement again
    }

    IEnumerator MoveForward()
    {
        while (!isRotating) // Move only if not rotating
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
            yield return null; // Keep moving smoothly
        }
    }

    void Update()
    {
        Shooting();
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
