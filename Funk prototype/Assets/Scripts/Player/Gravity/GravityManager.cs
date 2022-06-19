using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GravityManager : IGravityChanger
{
    private MovementManager _movement;
    private PhysicsInfo _info;
    private PlayerEvents _input;
    private EventsChecker _checker;

    private Light2D playerLight;

    public IWeapon weapon { set; private get; }

    private GlobalGravity _globalPhysics;

    [SerializeField]   
    private bool player, environment;

    void Awake()
    {
        _input = GetComponent<PlayerEvents>();

        _movement = GetComponent<MovementManager>();
        _info = GetComponent<PhysicsInfo>();
        _checker = GetComponent<EventsChecker>();

        playerLight = GetComponentInChildren<Light2D>();


    }
    public override void OnEnable()
    {
        base.OnEnable();

        _input.onGravitySwitch.AddListener(SwitchGravity);
        _input.onPlayerGravity.AddListener(FlipCharacterPhys);
        _input.onEnvironmentGravity.AddListener(FlipEnvironmentPhys);

        _globalPhysics = GlobalGravity.instance;
        _globalPhysics.onCharUpdate.AddListener(PlayerView);


    }

    public override void OnDisable()
    {
        base.OnDisable();

        _input.onGravitySwitch.RemoveListener(SwitchGravity);
        _input.onPlayerGravity.RemoveListener(FlipCharacterPhys);
        _input.onEnvironmentGravity.RemoveListener(FlipEnvironmentPhys);

        _globalPhysics.onCharUpdate.RemoveListener(PlayerView);

    }

    public void PlayerView(bool isOn)
    {
        playerLight.enabled = isOn;

    }

    public void FlipCharacterPhys()
    {
        
        _globalPhysics.ShouldFlipCharPhys = !_globalPhysics.ShouldFlipCharPhys;


    }

    public void FlipEnvironmentPhys()
    {
        
        _globalPhysics.ShouldFlipEnvirPhys = !_globalPhysics.ShouldFlipEnvirPhys;
    }


    public void SwitchGravity() {
        _globalPhysics.onGravitySwitch.Invoke();
        
        StartCoroutine(ClearCanSwitch());

    }
   
    private IEnumerator ClearCanSwitch()
    {
        yield return new WaitForSeconds(0.15f);
        _checker.CanSwitchGravity = false;

    }


    public override void SetGravityPositive()
    {
        _info.SetPositiveMultipliers();
    }

    public override void SetGravityNegative()
    {
        _info.SetNegativeMultipliers();
    }

    public override void SetParamsPositive()
    {
        _info.SetJumpAndGravityPositive();
    }

    public override void SetParamsNegative()
    {
        _info.SetJumpAndGravityNegative();
    }
}
