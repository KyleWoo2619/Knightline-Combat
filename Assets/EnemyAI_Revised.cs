using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyAI_Revised : MonoBehaviour
{

    public Transform player; //getting the player location
    public Transform bulletSpawn;//where cannon is shot from
    public GameObject bulletPrefab;//bullet
    public float movespeed; //enemy AI speed
    public float shootingRate; //enemy shooting rate
    private float rotationSpeed;
    public bool canMove = true; //turret mode or tank mode

    private float currentSpeed = 0f;
    private float acceleration = 2f;
    private float deceleration = 5f;
    private float nextShootTime = 0f;
    private bool isRotating = false;

    void Update()
    {
        Shooting();
        Rotation();
        Movement();

    }
    void Movement()
    {
        if (canMove)
        {
            //locating the player
            Vector3 direction = player.position - transform.position;

            //consistent movement to the player
            direction.Normalize();

            if (currentSpeed < movespeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
                currentSpeed = Mathf.Clamp(currentSpeed, 0, movespeed);
            }

            transform.position = Vector3.MoveTowards(transform.position, player.position, movespeed);

            isRotating = false;
        }

        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, deceleration * Time.deltaTime);

            isRotating = true;
        }
    }

    void Rotation()
    {
        if (isRotating && !canMove)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Shooting()
    {

        if (Time.time >= nextShootTime)
        {
            ShootBullet();
            nextShootTime = Time.time; + shootingRate;
        }
    }

    void ShootBullet()
    {

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation); //create the bullet prefab in the scene
        bullet.GetComponent<Bullet>().Initialize();
    }
}
