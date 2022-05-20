using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour, IEventObservable
{
    private PlayerEvents input;
    private EventsChecker checker;
    private IWeapon mainWeapon;
    private IWeapon currentWeapon;
    public GameEvent onDrawWeapon = new GameEvent();
    public GameEvent onHideWeapon = new GameEvent();
    private bool isShowingTrajectory = false;
    public GhostBullet ghostBullet;
    public bool CanCurrentWeaponFire
    {
        get
        {
            return HasActiveWeapon() && currentWeapon.CanFire;
        }
    }
    private void Awake()
    {
        input = GetComponent<PlayerEvents>();
    }
    private void Update()
    {
        //if (HasActiveWeapon() && isShowingTrajectory)
        //{
        //    //print(currentWeapon.transform.right);

        //    Projection.instance.SimulateTrajectory(ghostBullet, currentWeapon.GetComponentInChildren<Transform>().position,
        //        currentWeapon.GetComponentInChildren<Transform>().rotation);
        //} else
        //{
        //    Projection.instance.ClearLine();
        //}

    }
    public void OnEnable()
    {
        input.onWeaponSwitch.AddListener(DrawWeapon);
        onDrawWeapon.AddListener(ActivateCurrentWeapon);
        onHideWeapon.AddListener(DeactivateCurrentWeapon);
        PlayerInfo.instance.Stun.onStun.AddListener(onHideWeapon.Invoke);
        //input.onTrajectory.AddListener(FlipTrajectory);
    }
    public void OnDisable()
    {
        input.onWeaponSwitch.RemoveListener(DrawWeapon);
        onDrawWeapon.RemoveListener(ActivateCurrentWeapon);
        onHideWeapon.RemoveListener(DeactivateCurrentWeapon);
        PlayerInfo.instance.Stun.onStun.RemoveListener(onHideWeapon.Invoke);
        //input.onTrajectory.RemoveListener(FlipTrajectory);

    }

    public void FlipTrajectory()
    {
        SetTrajectoryShow( !isShowingTrajectory);
    }

    public void SetTrajectoryShow(bool isShowing)
    {
        isShowingTrajectory = isShowing;

    }
    public void UpdateMainWeapon(IWeapon weapon)
    {
        mainWeapon = weapon;
        RestoreMainWeapon();
        onDrawWeapon.Invoke();
    }

    public void UpdateCurrentWeapon(IWeapon weapon)
    {
        
        DeactivateCurrentWeapon();
        currentWeapon = weapon;
        onDrawWeapon.Invoke();
    }

    public void RestoreMainWeapon()
    {
        DeactivateCurrentWeapon();
        
        currentWeapon = mainWeapon;
        ActivateCurrentWeapon();
    }

    public void DrawWeapon()
    {

        print("Drawing");
      
        if (HasActiveWeapon())
        {
          
            onHideWeapon.Invoke();
        }
        else
        {
           
            onDrawWeapon.Invoke();

        }
    }

    private void DeactivateCurrentWeapon()
    {
        if (HasActiveWeapon())
        {
            currentWeapon.gameObject.SetActive(false);
            input.onFireModeSwitch.RemoveListener(SwitchFireMode);
        }
    }
    private void ActivateCurrentWeapon()
    {
        if (HasWeapon())
        {
            currentWeapon.gameObject.SetActive(true);
            input.onFireModeSwitch.AddListener(SwitchFireMode);
        }
    }
    public void SwitchFireMode()
    {
        print("Fire Mode Switched");
    }

    public bool HasActiveWeapon()
    {
        return HasWeapon() && currentWeapon.isActiveAndEnabled;
    }

    public bool HasWeapon()
    {
        return currentWeapon != null;
    }
}
