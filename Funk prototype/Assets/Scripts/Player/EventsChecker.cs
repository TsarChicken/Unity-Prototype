using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsChecker : MonoBehaviour
{

   
    private PlayerInfo player;

    private void Awake()
    {
       
        player = GetComponent<PlayerInfo>();
    }

    
    public bool CanFire {
        get
        {
            
            return player.Weapons.CanCurrentWeaponFire;
        }
    }

    public bool CanJump
    {
        get
        {
            return player.PlayerPhysics.SuitsJump();
        }
    }

    public bool CanInteract
    {
        get
        {
            return player.Interactions.HasInteractions();
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
            return canSwitchGravity && LevelManager.instance.currentRoom.CanSwitchGravity;
        }

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
            return player.Weapons.HasWeapon();
        }

    }
   
    public bool CanFireMode
    {
        get
        {
            return player.Weapons.HasActiveWeapon();
        }

    }

    public bool CanMelee
    {
        get
        {
            return player.PlayerPhysics.isOnGround;
        }

    }
    public bool CanTrajectory
    {
        get
        {
            return player.Weapons.HasActiveWeapon();
        }
    }

}
