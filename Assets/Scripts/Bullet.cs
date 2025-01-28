using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10; 
    public int damage = 5;
    public float maxDistance = 1; //max distance it will trave before destroyig itself

    private Vector2 startPosition; //bullet startig position
    private float conquaredDistance = 0; //if con.distance is greater than maxDistance, destroy game object
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize() //create bullet within scene
    {
        startPosition = transform.position;
        rb2d.velocity = transform.up * speed;
    }

    private void Update()
    {
        conquaredDistance = Vector2.Distance(transform.position, startPosition); //calculate the travelled distance by bullet
        if (conquaredDistance > maxDistance) //check if travelled distance is greater than maxDisance
        {
            DisableObject(); //if so, destroy bullet
        }

    }

    private void DisableObject() //tell bullet game object to destroy itself
    {
        rb2d.velocity = Vector2.zero; //if past maxDistance
        gameObject.SetActive(false); //status becomes false
    }

    private void OnTriggerEnter2D(Collider2D collision) //bullets destroy enemies
    {
        Debug.Log("Collided" + collision.name);
        DisableObject();
    }

}
