using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveInterval = 0.5f; // Time between each move
    public float moveDistance = 1f; // Distance to move forward or backward
    private bool canMove = true;

    void Update()
    {
        if (canMove)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) // Move Forward
        {
            StartCoroutine(Move(Vector3.up));
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Move Backward
        {
            StartCoroutine(Move(Vector3.down));
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Rotate Left
        {
            Rotate(-45); // Rotate left by 45 degrees
        }
        else if (Input.GetKeyDown(KeyCode.A)) // Rotate Right
        {
            Rotate(45); // Rotate right by 45 degrees
        }
    }

    IEnumerator Move(Vector3 direction)
    {
        canMove = false;

        // Calculate the movement direction based on the player's rotation
        Vector3 moveDirection = transform.up * (direction == Vector3.up ? 1 : -1); // Forward or backward
        Vector3 targetPosition = transform.position + moveDirection * moveDistance;

        // "Teleport" the player to the new position
        transform.position = targetPosition;

        yield return new WaitForSeconds(moveInterval); // Wait before allowing another move
        canMove = true;
    }

    void Rotate(float angle)
    {
        // Rotate the player around the Z-axis for top-down 2D
        transform.Rotate(0, 0, angle);

        // Snap rotation to the nearest 45 degrees for alignment
        float currentAngle = transform.eulerAngles.z;
        float snappedAngle = Mathf.Round(currentAngle / 45f) * 45f;
        transform.rotation = Quaternion.Euler(0, 0, snappedAngle);
    }
}
