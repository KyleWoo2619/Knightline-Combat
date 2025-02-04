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

    public UnityEvent OnShoot = new UnityEvent(); //allows us to assign three different events to the player game asset
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();
    private bool shielded;
    

    private void Awake() //tells game which camera to reffer to in reference to the player
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
            shielded = false;
    }
    void Start()
    {
        shielded = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetBodyMovement();
        GetTurretMovement();
        GetShootingInput();
        CheckShield();
    }

    private void GetShootingInput() //player shoots bullets with spacebar
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

    void CheckShield()
    {
        if (Input.GetKey(KeyCode.E)&&!shielded)
        {
            shield.SetActive(true);
            shielded = true;
            //code for turning off shield
            Invoke("NoShield", 3f);
        }
    }

    void NoShield() 
    {
        shield.SetActive(false);
        shielded = false;
    }

}
