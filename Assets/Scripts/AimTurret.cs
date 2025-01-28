using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aiming the Turret with the location of the player's mouse
public class AimTurret : MonoBehaviour
{
    public float turretRotationSpeed = 80;
    public float rotationSpeed = 50;

    public void Aim(Vector2 inputPointerPosition)
    {
        var turretDirection = (Vector3)inputPointerPosition - transform.position;
        //make the transform of the turret call from the location of the mouse pointer

        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;
        //calculates the desired angle of rotation in degrees

        /* var RotationStep = turretRotationSpeed * Time.deltaTime; */
        //makes the tank rotate with a small delay, adds to both skill ceiling and retro enviroment

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, desiredAngle - 90), rotationSpeed /*, RotationStep */);
        //maps the correct direction of the turret's rotation
    }
}
