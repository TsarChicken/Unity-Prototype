using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager :IControllable
{
    private RestrictionManager _restrictor;

    private PlayerEvents _playerEvents;

    private EventsChecker _checker;

    private PlayerInput _input;


    private bool isBlocked = false;
    private void Start()
    {
        _restrictor = RestrictionManager.instance;
        _playerEvents = GetComponent<PlayerEvents>();
        _checker = GetComponent<EventsChecker>();
        _input = GetComponent<PlayerInput>();
    }

    //public override void TakeControl()
    //{
    //    playerEvents.enabled = false;
    //}

    public override void Block()
    {
        isBlocked = true;
        PlayerInfo.instance.IsStunned = isBlocked;
        
        PlayerInfo.instance?.Movement.StopHorizontalMovement();
    }

    public override void Unblock()
    {
        isBlocked = false;
        PlayerInfo.instance.IsStunned = isBlocked;
    }
    private void OnEnable()
    {
        Block();
        Unblock();
    }
 
    public void OnMove(InputAction.CallbackContext context)
    {
        if (_playerEvents.onMove.IsEmpty() || isBlocked)
        {
            return;
        }
        _playerEvents.onMove.Invoke(context.ReadValue<Vector2>());
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (_playerEvents.onAim.IsEmpty() || isBlocked )
        {
            return;
        }
        _playerEvents.onAim.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onFire.IsEmpty() || _checker.CanFire == false || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }

        _playerEvents.onFire.Invoke();

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if (_playerEvents.onJump.IsEmpty() || _checker.CanJump == false || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }

        _playerEvents.onJump.Invoke();
        

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onInteract.IsEmpty() || _checker.CanInteract == false || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }
        _playerEvents.onInteract.Invoke();
    }

    public void OnSwitchGravity(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onGravitySwitch.IsEmpty() || _checker.CanSwitchGravity == false || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }
        _playerEvents.onGravitySwitch.Invoke();
    }

    public void OnCharacterPhys(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onPlayerGravity.IsEmpty() || _checker.CanFlipGravParams == false || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }
        _playerEvents.onPlayerGravity.Invoke();
    }

    public void OnEnvironmentPhys(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onEnvironmentGravity.IsEmpty() || _checker.CanFlipGravParams == false || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }
        _playerEvents.onEnvironmentGravity.Invoke();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onCrouch.IsEmpty() || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }
        _playerEvents.onCrouch.Invoke();

    }

    public void OnHighlight(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onHighlight.IsEmpty() || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }
        _playerEvents.onHighlight.Invoke();
    }

    public void OnDrawWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onWeaponSwitch.IsEmpty() || _checker.CanDrawWeapon == false || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }
        _playerEvents.onWeaponSwitch.Invoke();
    }

    public void OnFireMode(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onFireModeSwitch.IsEmpty() || _checker.CanFireMode == false || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }
        _playerEvents.onFireModeSwitch.Invoke();
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onMelee.IsEmpty() || _checker.CanMelee == false || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }
        _playerEvents.onMelee.Invoke();
    }

    public void OnCameraDistant(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onTrajectory.IsEmpty() || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }
        _playerEvents.onTrajectory.Invoke();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (_playerEvents.onPause.IsEmpty() || isBlocked)
        {
            _restrictor.Restrict();
            return;
        }

        _playerEvents.onPause.Invoke();
    }
}