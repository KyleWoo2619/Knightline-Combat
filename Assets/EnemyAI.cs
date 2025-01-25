using UnityEngine;
using System.Collections; // Required for IEnumerator

public class EnemyController : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    public float moveInterval = 1f; // Time between moves
    public float tileSize = 1f; // Tile size for movement
    private Vector2 currentDirection = Vector2.up; // Default direction
    private bool canMove = true;

    void Start()
    {
        StartCoroutine(MoveTowardPlayer());
    }

    IEnumerator MoveTowardPlayer()
    {
        while (true)
        {
            if (canMove)
            {
                // Determine direction toward the player
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

                // Snap to closest 45-degree direction
                float snappedAngle = Mathf.Round(angleToPlayer / 45f) * 45f;
                currentDirection = new Vector2(Mathf.Cos(snappedAngle * Mathf.Deg2Rad), Mathf.Sin(snappedAngle * Mathf.Deg2Rad));

                // Rotate the enemy
                transform.rotation = Quaternion.Euler(0, 0, snappedAngle);

                // Move slightly toward the player
                Vector3 targetPosition = transform.position + (Vector3)currentDirection * tileSize;
                transform.position = targetPosition;

                yield return new WaitForSeconds(moveInterval);
            }
        }
    }
}
