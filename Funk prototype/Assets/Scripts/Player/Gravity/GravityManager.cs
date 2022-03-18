using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour, IGravityManager
{
    private MovementManager movement;
    private PhysicsInfo info;
    private PlayerEvents input;
    private EventsChecker checker;
    public IWeapon weapon { set; private get; }

    [SerializeField]   
    private bool player, environment, canSwitch;
    [SerializeField] Camera camera;

    void Start()
    {
        input = GetComponent<PlayerEvents>();
        movement = GetComponent<MovementManager>();
        info = GetComponent<PhysicsInfo>();
        checker = GetComponent<EventsChecker>();
        player = true;
        environment = true;
        input.onGravitySwitch.AddListener(SwitchGravity);
        input.onPlayerGravity.AddListener(FlipCharacterPhys);
        input.onEnvironmentGravity.AddListener(FlipEnvironmentPhys);
    }

  

    public void FlipCharacterPhys()
    {
        player = !player;
        MakeAmends();
    }

    public void FlipEnvironmentPhys()
    {
        environment = !environment;
        MakeAmends();
    }

    private void MakeAmends()
    {
        if (!player && !environment)
        {
            player = true;
            environment = true;
        }
    }
    public void SwitchGravity() {
     
        if (environment || player)
        {
            if (environment && !player)
            {
                SwitchPhysics();
                info.FlipMultipliers();

            }
            if (!environment && player)
            {

                SwitchPlayerPhysics();
                SwitchPlayerController();
            }
            if (environment && player)
            {
                SwitchPhysics();
                SwitchPlayerController();
                info.FlipStandartGravity();
                info.FlipJump();
            }
        }

        StartCoroutine(ClearCanSwitch());

    }
    public void SetCamera(Camera cam)
    {
        camera = cam;
    }
    private IEnumerator ClearCanSwitch()
    {
        yield return new WaitForSeconds(0.15f);
        checker.CanSwitchGravity = false;

    }


    public void SwitchPhysics() {
        Physics2D.gravity = new Vector2(Physics2D.gravity.x,
            -Physics2D.gravity.y);
    }
    public void SwitchPlayerPhysics()
    {
        movement.FlipAirPhys();
    }

    public void SwitchPlayerController()
    {
        camera.RotateCamera();

        movement.RotateCharacter();
        
        if(weapon!=null)
            weapon.FlipDirection();
    }

}
