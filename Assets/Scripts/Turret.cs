using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turret : MonoBehaviour
{

    public List<Transform> turretBarrels; //can have multiple turrets to fire more than one bullet
    public GameObject bulletPrefab;
    public float reloadDelay = 1;

    private bool canShoot = true; //blocks the shooting mechanic if waiting for reload
    private Collider2D[] tankColliders; //ensures that bulllets dont affect player collider
    private float currentDelay = 0;

    private void Awake() //call reference to our ridgid body rb2d
    {
        tankColliders = GetComponentsInParent<Collider2D>();
    }

    private void Update() //behavior of bullet when reloading
    {
        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }
    }
    
    public void Shoot() //shooting method for bullet
    {
        Debug.Log("Shooting");


        if (canShoot)
        {
            canShoot = false;
            currentDelay = reloadDelay;

            foreach (var barrel in turretBarrels)
            {
                GameObject bullet = Instantiate(bulletPrefab); //create the bullet prefab in the scene
                bullet.transform.position = barrel.position; //position of bullet matches position of the barrel
                bullet.transform.localRotation = barrel.rotation; //rotation of bullet matches rotation of the barrel
                bullet.GetComponent<Bullet>().Initialize();

                foreach (var collider in tankColliders) //bullet ignores player tank collider
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
        }
    }

}
