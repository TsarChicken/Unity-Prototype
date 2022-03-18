using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsChecker : MonoBehaviour
{

    private PhysicsInfo physics;
    private WeaponManager weapons;
    private InteractionManager interactions;


    private void Awake()
    {
        physics = GetComponent<PhysicsInfo>();
        weapons = GetComponent<WeaponManager>();
        interactions = GetComponent<InteractionManager>();
    }

    
    public bool CanFire {
        get
        {
            return weapons.HasActiveWeapon();
        }
    }

    public bool CanJump
    {
        get
        {
            return physics.SuitsJump();
        }
    }

    public bool CanInteract
    {
        get
        {
            return interactions.HasInteractions();
        }
    }

    public bool CanSwitchGravity
    {
        set; get;

    }

    public bool CanCrouch
    {
        set; get;

    }

    public bool CanStandUp
    {
        set; get;
    }

    public bool CanDrawWeapon
    {
        get
        {
            return weapons.HasWeapon();
        }

    }

    public bool CanFireMode
    {
        set; get;

    }

    public bool CanMelee
    {
        set; get;

    }
    public bool CanTrajectory
    {
        set; get;
    }

}
