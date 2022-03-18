using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementView : MonoBehaviour
{
    [SerializeField]
    private PlayerEvents input;
   
    public void ShowMove(Vector2 vec)
    {
        Debug.Log("Move " + vec);

    }
    public void ShowAim(Vector2 vec)
    {
        Debug.Log("Aim " + vec);
    }
    public void ShowGravSw()
    {
        Debug.Log("Gravity Switch ");

    }

    public void ShowInteraction()
    {
        Debug.Log("Interact ");
    }

    public void ShowJump()
    {
        Debug.Log("Jump ");
    }

    public void ShowCrouch()
    {
        Debug.Log("Crouch ");

    }

    

    public void ShowMelee()
    {
        Debug.Log("Melee ");

    }

    public void ShowEnvrPhys()
    {
        Debug.Log("Envr Phys ");

    }

    public void ShowCharPhys()
    {
        Debug.Log("Char Phys ");

    }

    public void ShowShoot()
    {
        Debug.Log("Shoot ");

    }

    public void ShowCamera()
    {
        Debug.Log("Camera ");

    }

    public void ShowDrawWeapon()
    {
        Debug.Log("Weapon Draw ");

    }

    public void ShowFireMode()
    {
        Debug.Log("Fire Mode");

    }

    public void ShowHighlight()
    {
        Debug.Log("Highlight ");

    }
    public void ShowPause()
    {
        Debug.Log("Pause ");

    }

    private void OnEnable()
    {
        input.onMove.AddListener(ShowMove);
        input.onAim.AddListener(ShowAim);
        input.onGravitySwitch.AddListener(ShowGravSw);
        input.onInteract.AddListener(ShowInteraction);
        input.onJump.AddListener(ShowJump);
        input.onCrouch.AddListener(ShowCrouch);
        input.onMelee.AddListener(ShowMelee);
        input.onEnvironmentGravity.AddListener(ShowEnvrPhys);
        input.onPlayerGravity.AddListener(ShowCharPhys);
        input.onFire.AddListener(ShowShoot);
        input.onTrajectory.AddListener(ShowCamera);
        input.onWeaponSwitch.AddListener(ShowDrawWeapon);
        input.onFireModeSwitch.AddListener(ShowFireMode);
        input.onPause.AddListener(ShowPause);
    }
    private void OnDisable()
    {
        input.onMove.RemoveListener(ShowMove);
        input.onAim.RemoveListener(ShowAim);
        input.onGravitySwitch.RemoveListener(ShowGravSw);
        input.onInteract.RemoveListener(ShowInteraction);
        input.onJump.RemoveListener(ShowJump);
        input.onCrouch.RemoveListener(ShowCrouch);
        input.onMelee.RemoveListener(ShowMelee);
        input.onEnvironmentGravity.RemoveListener(ShowEnvrPhys);
        input.onPlayerGravity.RemoveListener(ShowCharPhys);
        input.onFire.RemoveListener(ShowShoot);
        input.onTrajectory.RemoveListener(ShowCamera);
        input.onWeaponSwitch.RemoveListener(ShowDrawWeapon);
        input.onFireModeSwitch.RemoveListener(ShowFireMode);
        input.onPause.RemoveListener(ShowPause);
    }




}
