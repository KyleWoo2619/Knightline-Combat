using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehaviorA : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject shield;
    public GameObject ShieldPickup;

    public UnityEvent OnShoot = new UnityEvent(); //allows us to assign three different events to the player game asset
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();
    private bool shielded;

    public bool IsShielded => shielded;

    private void Awake() //tells game which camera to reffer to in reference to the player
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
            shielded = false;
            //shield.SetActive(false); //tells game to start with the shield gameobject toggled off
    }

    // Update is called once per frame
    void Update()
    {
        GetBodyMovement();
        GetTurretMovement();
        GetShootingInput();
    }

    private void GetShootingInput() //player shoots bullets with left click (spacebar)
    {
        if (Input.GetMouseButtonDown(0))//Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Shooting");
            OnShoot?.Invoke();
        }
    }

    private void GetTurretMovement() //calls the mouse position for the turret
    {
        OnMoveTurret?.Invoke(GetMousePosition());
    }

    private Vector2 GetMousePosition() //tells turret position to follow the movement of the mouse
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition; 
    }

    private void GetBodyMovement()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: ShieldPickup" + collision.gameObject.tag); //debug to check and see if shield is activating
        if (collision.CompareTag("ShieldPickup") && !shielded)
        {
            Debug.Log("Shield Activated!");
            ActivateShield();
            Destroy(collision.gameObject);
        }
        else if ((collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet")) && shielded)
        {
            DeactivateShield();
        }
    }
    void ActivateShield()
    {
        shield.SetActive(true);
        shielded = true;
    }

    void DeactivateShield()
    {
        shield.SetActive(false);
        shielded = false;
    }*/

}
