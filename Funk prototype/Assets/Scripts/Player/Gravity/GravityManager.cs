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
    private bool player, environment;
    public bool Player { private set {
            player = value;
        } get
        {
            return player;
        }
    }
    public bool Environment{
        private set {
            environment = value;
        } get
        {
            return environment;
        }
    }

    [SerializeField] CameraScript camera;

    void Start()
    {
        input = GetComponent<PlayerEvents>();
        movement = GetComponent<MovementManager>();
        info = GetComponent<PhysicsInfo>();
        checker = GetComponent<EventsChecker>();
        Player = true;
        Environment = true;
        input.onGravitySwitch.AddListener(SwitchGravity);
        input.onPlayerGravity.AddListener(FlipCharacterPhys);
        input.onEnvironmentGravity.AddListener(FlipEnvironmentPhys);
    }

  

    public void FlipCharacterPhys()
    {
        Player = !Player;
        MakeAmends();
       

    }

    public void FlipEnvironmentPhys()
    {
        Environment = !Environment;
        MakeAmends();
    }

    private void MakeAmends()
    {
        if (!Player && !Environment)
        {
            Player = true;
            Environment = true;
        }
        GravityLights.instance.PlayerView();
        GravityLights.instance.EnvironmentView();
    }
    public void SwitchGravity() {
     
        if (Environment || Player)
        {
            if (Environment && !Player)
            {
                SwitchPhysics();
                info.FlipMultipliers();

            }
            if (!Environment && Player)
            {

                SwitchPlayerPhysics();
                SwitchPlayerController();
            }
            if (Environment && player)
            {
                SwitchPhysics();
                SwitchPlayerController();
                info.FlipStandartGravity();
                info.FlipJump();
            }
        }

        StartCoroutine(ClearCanSwitch());

    }
    public void SetCamera(CameraScript cam)
    {
        camera = cam;
    }
    private IEnumerator ClearCanSwitch()
    {
        yield return new WaitForSeconds(0.15f);
        checker.CanSwitchGravity = false;

    }


    public void SwitchPhysics() {
        PhysicsDataManager.instance.FlipGravity();
        //Physics2D.gravity = new Vector2(Physics2D.gravity.x,
        //    -Physics2D.gravity.y);
    }
    public void SwitchPlayerPhysics()
    {
        movement.FlipAirPhys();
    }

    public void SwitchPlayerController()
    {
        //camera.RotateCamera();

        movement.RotateCharacter();
        
        //if(weapon!=null)
        //    weapon.FlipDirection();
    }

}
