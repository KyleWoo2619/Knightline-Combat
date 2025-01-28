using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxSpeed = 10;
    public float rotationSpeed = 50;
    private Vector2 movementVector;

    public float acceleration = 70;
    public float deacceleration = 50;
    public float currentSpeed = 0;
    public float currentDirection = 1;

    private void Awake() //call reference to our ridgid body rb2d
    {
        rb2d = GetComponentInParent<Rigidbody2D>(); //get component
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculateSpeed(movementVector); //give the player tank some "physics", when you let go of 
        if (movementVector.y > 0)      //'w' it will keep moving until it eventually stops itself
        {
            currentDirection = 1;
        } 
        else if (movementVector.y < 0)
        {
            currentDirection = 0;
        }

    }

    private void CalculateSpeed(Vector2 movementVector) //calculation of current speed
    {
        if (Mathf.Abs(movementVector.y) > 0) //adding to current speed
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else //subtracting from current speed
        {
            currentSpeed -= deacceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed); //keep the value between 0 and max speed
    }

    private void FixedUpdate() //modifying the ridgid body
    {
        rb2d.velocity = (Vector2)transform.up * currentSpeed * currentDirection * Time.fixedDeltaTime; //mapping to move player tank forward
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed *
            Time.fixedDeltaTime)); //mapping to rotate the player around by all directions
    }

}
