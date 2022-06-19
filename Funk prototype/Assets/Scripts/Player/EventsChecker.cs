using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsChecker : MonoBehaviour
{

   
    private PlayerInfo _player;

    private void Awake()
    {

        _player = GetComponent<PlayerInfo>();
    }

    
    public bool CanFire {
        get
        {
            
            return _player.Weapons.CanCurrentWeaponFire;
        }
    }

    public bool CanJump
    {
        get
        {
            return _player.PlayerPhysics.SuitsJump();
        }
    }

    public bool CanInteract
    {
        get
        {
            return _player.Interactions.HasInteractions();
        }
    }

    private bool canSwitchGravity;
    public bool CanSwitchGravity
    {
        set
        {
            canSwitchGravity = value;
        }
        get
        {
            return canSwitchGravity && BuildingMaster.instance.CurrentLocation.CanSwitchGravity;
        }

    }

    public bool CanFlipGravParams
    {
      
        get => canSwitchGravity && BuildingMaster.instance.CurrentLocation.CanUpdateGravityParams;
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
            return _player.Weapons.HasWeapon();
        }

    }
   
    public bool CanFireMode
    {
        get
        {
            return _player.Weapons.HasActiveWeapon();
        }

    }

    public bool CanMelee
    {
        get
        {
            return _player.PlayerPhysics.isOnGround && _player.PlayerMelee.CanFight;
        }

    }
    public bool CanTrajectory
    {
        get
        {
            return _player.Weapons.HasActiveWeapon();
        }
    }

}
