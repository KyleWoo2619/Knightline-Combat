using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Revised : MonoBehaviour
{

    public Transform player; //getting the player location
    public float movespeed; //enemy AI speed
    public bool canMove = true;

    void Update()
    {

        Movement();
        Shooting();

    }
    void Movement()
    {
        if (canMove)
        {
            //locating the player
            Vector3 direction = player.position - transform.position;

            //consistent movement to the player
            direction.Normalize();

            transform.position = Vector3.MoveTowards(transform.position, player.position, movespeed);
        }
    }

    void Shooting()
    {
        
    }
}
