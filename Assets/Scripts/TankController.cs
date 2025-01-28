using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class TankController : MonoBehaviour
{
    public TankMover tankMover;
    public AimTurret aimTurret;
    public Turret[] turrets;

    private void Awake()
    {
        if (tankMover == null)
            tankMover =
                GetComponentInChildren<TankMover>();
        if (aimTurret == null)
            aimTurret =
                GetComponentInChildren<AimTurret>();
        if (turrets == null || turrets.Length == 0)
        {
            turrets = GetComponentsInChildren<Turret>();
        }

    }

    public void HandleShoot() //shooting method for bullet
    {
        Debug.Log("Shooting");
        
        foreach (var turret in turrets)
        {
            turret.Shoot();
        }

    }

    public void HandleBodyMovement(Vector2 movementVector) //calls the tank body to the main control system
    {
        tankMover.Move(movementVector);
    }

    public void HandleTurretMovement(Vector2 pointerPosition) //calls the turret position to the main control system
    {
        aimTurret.Aim(pointerPosition);
    }

}
