using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour, IGravityManager
{
    private MovementManager movement;
    private InputManager input;
    
    public IWeapon weapon { set; private get; }

    [SerializeField]   
    private bool player, environment, canSwitch;
    [SerializeField] Camera camera;

    void Start()
    {
        movement = GetComponent<MovementManager>();
        input = InputManager.instance;
    }

    void Update()
    {
        if (movement.IsOnGround())
            canSwitch = true;

        if (input.characterPhysPressed)
        {
            player = !player;
        }
        if (input.environmentPhysPressed)
        {
            environment = !environment;
        }

        if(!player && !environment)
        {
            player = true;
            environment = true;
        }

        if (input.flipPhysicsPressed)
        {
            SwitchGravity();
        }


    }

    public void SwitchGravity() {
        if (!canSwitch)
            return;
        if (environment || player)
        {
            if (environment && !player)
            {
                SwitchPhysics();
                movement.FlipMultipliers();

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
                movement.FlipStandartGravity();
                movement.FlipJump();
            }
        }

        StartCoroutine(ClearCanSwitch());

    }
    private IEnumerator ClearCanSwitch()
    {
        yield return new WaitForSeconds(0.15f);
        canSwitch = false;

    }

    IEnumerator AlertGravityChange()
    {
        yield return new WaitForSeconds(0.1f);
        movement.isChangingGravity = true;

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
