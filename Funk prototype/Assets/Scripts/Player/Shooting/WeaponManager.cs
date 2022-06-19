using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour, IEventObservable
{
    public BottleHolder Holder;
    private PlayerEvents _input;
    private IWeapon _mainWeapon;
    private IWeapon _currentWeapon;
    public readonly GameEvent onDrawWeapon = new GameEvent();
    public readonly GameEvent onHideWeapon = new GameEvent();
    private bool _isShowingTrajectory = false;
    public bool CanCurrentWeaponFire
    {
        get
        {
            return HasActiveWeapon() && _currentWeapon.CanFire;
        }
    }
    private void Awake()
    {
        _input = GetComponent<PlayerEvents>();
       

    }

    public void OnEnable()
    {
        _input.onWeaponSwitch.AddListener(DrawWeapon);
        onDrawWeapon.AddListener(ActivateCurrentWeapon);
        onHideWeapon.AddListener(DeactivateCurrentWeapon);
        PlayerInfo.instance.Stun.onStun.AddListener(onHideWeapon.Invoke);
        PlayerInfo.instance.HP.onDie.AddListener(Die);

    }
    public void OnDisable()
    {
        _input.onWeaponSwitch.RemoveListener(DrawWeapon);
        onDrawWeapon.RemoveListener(ActivateCurrentWeapon);
        onHideWeapon.RemoveListener(DeactivateCurrentWeapon);
        PlayerInfo.instance.Stun.onStun.RemoveListener(onHideWeapon.Invoke);
        PlayerInfo.instance.HP.onDie.RemoveListener(Die);

    }

    private void Die()
    {
        onHideWeapon.Invoke();
        _currentWeapon?.gameObject.SetActive(false);
        _currentWeapon = null;
        _mainWeapon?.gameObject.SetActive(false);
        _mainWeapon = null;
    }
    public void FlipTrajectory()
    {
        SetTrajectoryShow( !_isShowingTrajectory);
    }

    public void SetTrajectoryShow(bool isShowing)
    {
        _isShowingTrajectory = isShowing;

    }
    public void UpdateMainWeapon(IWeapon weapon)
    {
        _mainWeapon = weapon;
        RestoreMainWeapon();
        onDrawWeapon.Invoke();
    }

    public void UpdateCurrentWeapon(IWeapon weapon)
    {
        
        DeactivateCurrentWeapon();
        _currentWeapon = weapon;
        onDrawWeapon.Invoke();
    }

    public void RestoreMainWeapon()
    {
        DeactivateCurrentWeapon();

        _currentWeapon = _mainWeapon;
        if (HasWeapon())
            ActivateCurrentWeapon();
        else
            onHideWeapon.Invoke();
    }

    public void DrawWeapon()
    {
      
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
            _currentWeapon.gameObject.SetActive(false);
            _input.onFireModeSwitch.RemoveListener(SwitchFireMode);
        }
    }
    private void ActivateCurrentWeapon()
    {
        if (HasWeapon())
        {
            _currentWeapon.gameObject.SetActive(true);
            _input.onFireModeSwitch.AddListener(SwitchFireMode);
        }
    }
    public void SwitchFireMode()
    {
        print("Fire Mode Switched");
    }

    public bool HasActiveWeapon()
    {
        return HasWeapon() && _currentWeapon.isActiveAndEnabled;
    }

    public bool HasWeapon()
    {
        return _currentWeapon != null;
    }
}
