using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadVibration : MonoBehaviour, IEventObservable
{
    private Rumbler rumbler;
    private PlayerEvents input;
    [SerializeField] private RumbleData jump;
    [SerializeField] private RumbleData shoot;
    [SerializeField] private RumbleData melee;
    [SerializeField] private RumbleData gravity;

    [SerializeField] private RumbleData hurt;
    [SerializeField] private RumbleData lowHealth;
    [SerializeField] private RumbleData die;

    void Awake()
    {
        input = GetComponent<PlayerEvents>();
        rumbler = GetComponent<Rumbler>();
    }

    public void OnEnable()
    {
        input.onJump.AddListener(Jump);
        input.onFire.AddListener(Shoot);
        input.onMelee.AddListener(Melee);
        input.onGravitySwitch.AddListener(Gravity);

    }

    public void OnDisable()
    {
        input.onJump.RemoveListener(Jump);
        input.onFire.RemoveListener(Shoot);
        input.onMelee.RemoveListener(Melee);
        input.onGravitySwitch.RemoveListener(Gravity);

    }


    public void Jump()
    {
        HandleRumbling(jump);
    }

    public void Shoot()
    {
        HandleRumbling(shoot);
    }
    public void Melee()
    {
        HandleRumbling(melee);
    }

    public void Gravity()
    {
        HandleRumbling(gravity);
    }


    private void HandleRumbling(RumbleData data)
    {
        rumbler.StopRumble();
        switch (data.pattern)
        {
            case RumblePattern.Constant:
                rumbler.RumbleConstant(data.lowStart, data.highStart, data.duration);
                break;
            case RumblePattern.Pulse:
                rumbler.RumblePulse(data.lowStart, data.highStart, data.burstTime, data.duration);
                break;
            case RumblePattern.Linear:
                rumbler.RumbleLinear(data.lowStart, data.lowEnd, data.highStart, data.lowEnd, data.duration);
                break;
            default:
                break;
        }
    }
}
