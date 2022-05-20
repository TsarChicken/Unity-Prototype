using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager :IControllable
{
    private RestrictionManager restrictor;

    private PlayerEvents playerEvents;

    private EventsChecker checker;

    private PlayerInput input;

    private bool isBlocked = false;
    private void Start()
    {
        restrictor = RestrictionManager.instance;
        playerEvents = GetComponent<PlayerEvents>();
        checker = GetComponent<EventsChecker>();
        input = GetComponent<PlayerInput>();
    }

    //public override void TakeControl()
    //{
    //    playerEvents.enabled = false;
    //}

    public override void Block()
    {
        isBlocked = true;
    }

    public override void Unblock()
    {
        isBlocked = false;
    }
    //private void OnEnable()
    //{
    //    input.enabled = true;
    //}
    //private void OnDisable()
    //{
    //    input.enabled = false;

    //}
    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerEvents.onMove.IsEmpty() || isBlocked)
        {
            return;
        }
        playerEvents.onMove.Invoke(context.ReadValue<Vector2>());
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (playerEvents.onAim.IsEmpty() || isBlocked )
        {
            return;
        }
        playerEvents.onAim.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onFire.IsEmpty() || checker.CanFire == false || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        
        playerEvents.onFire.Invoke();

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if (playerEvents.onJump.IsEmpty() ||checker.CanJump == false || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        
            playerEvents.onJump.Invoke();
        

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onInteract.IsEmpty() || checker.CanInteract == false || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onInteract.Invoke();
    }

    public void OnSwitchGravity(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onGravitySwitch.IsEmpty() || checker.CanSwitchGravity == false || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onGravitySwitch.Invoke();
    }

    public void OnCharacterPhys(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onPlayerGravity.IsEmpty() || checker.CanSwitchGravity == false || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onPlayerGravity.Invoke();
    }

    public void OnEnvironmentPhys(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onEnvironmentGravity.IsEmpty() || checker.CanSwitchGravity == false || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onEnvironmentGravity.Invoke();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onCrouch.IsEmpty() || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onCrouch.Invoke();

    }

    public void OnHighlight(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onHighlight.IsEmpty() || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onHighlight.Invoke();
    }

    public void OnDrawWeapon(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onWeaponSwitch.IsEmpty() || checker.CanDrawWeapon == false || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onWeaponSwitch.Invoke();
    }

    public void OnFireMode(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onFireModeSwitch.IsEmpty() || checker.CanFireMode == false || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onFireModeSwitch.Invoke();
    }

    public void OnMelee(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onMelee.IsEmpty() || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onMelee.Invoke();
    }

    public void OnCameraDistant(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onTrajectory.IsEmpty() || isBlocked)
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onTrajectory.Invoke();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        if (playerEvents.onPause.IsEmpty() || isBlocked)
        {
            restrictor.Restrict();
            return;
        }

        playerEvents.onPause.Invoke();
    }
}