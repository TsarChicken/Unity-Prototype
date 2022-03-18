using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : IControllable
{
    private RestrictionManager restrictor;

    private PlayerEvents playerEvents;

    private EventsChecker checker;
    private void Start()
    {
        restrictor = RestrictionManager.Instance;
        playerEvents = GetComponent<PlayerEvents>();
        checker = GetComponent<EventsChecker>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (playerEvents.onMove.IsEmpty())
        {
            restrictor.Restrict();
            return;
        }
        playerEvents.onMove.Invoke(context.ReadValue<Vector2>());
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        if (playerEvents.onAim.IsEmpty() )
        {
            restrictor.Restrict();
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
        if (playerEvents.onFire.IsEmpty() || checker.CanFire == false)
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

        if (playerEvents.onJump.IsEmpty() ||checker.CanJump == false)
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
        if (playerEvents.onInteract.IsEmpty() || checker.CanInteract == false)
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
        if (playerEvents.onGravitySwitch.IsEmpty() || checker.CanSwitchGravity == false)
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
        if (playerEvents.onPlayerGravity.IsEmpty())
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
        if (playerEvents.onEnvironmentGravity.IsEmpty())
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
        if (playerEvents.onCrouch.IsEmpty() )
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
        if (playerEvents.onHighlight.IsEmpty())
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
        if (playerEvents.onWeaponSwitch.IsEmpty())
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
        if (playerEvents.onFireModeSwitch.IsEmpty() )
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
        if (playerEvents.onMelee.IsEmpty() )
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
        if (playerEvents.onTrajectory.IsEmpty())
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
        if (playerEvents.onPause.IsEmpty())
        {
            restrictor.Restrict();
            return;
        }

        playerEvents.onPause.Invoke();
    }
}