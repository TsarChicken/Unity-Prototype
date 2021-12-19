using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//Input Manager is based on the Old Input System.
//The class is yet to be remade according to New System demands -
//Input Manager should be based on Events and accumulate other scripts' methods.
public class InputManager : MonoBehaviour, IInputManager
{

    public static InputManager instance = null;
    //Joysticks used
    public float horizontal { get; private set; }
    public (float x, float y) aim { get; private set; }

    //Front buttons used
    public bool jumpPressed { get;  set; }
    public bool interactPressed { get; private set; }
    public bool flipPhysicsPressed { get; private set; }
    public bool meleePressed { get; private set; }

    //Manipulate Physics

    public bool characterPhysPressed { get; private set; }
    public bool environmentPhysPressed { get; private set; }


    //Triggers pressed
    //Camera manipulation is yet to be done
    public bool distantCameraPressed { get; private set; }
    public bool shootPressed { get; private set; }


    private bool readyToClear;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if(instance == this)
        {
            Destroy(gameObject);
        }
        ClearInputs();
    }

    // Update is called once per frame
    void Update()
    {

        ClearInputs();

        ProcessInputs();
    }

    private void FixedUpdate()
    {
        readyToClear = true;
    }
    public void ClearInputs()
    {
        //If not ready to clear input, exit
        if (!readyToClear)
            return;
        //Reset all inputs
        //jumpPressed = false;
        //interactPressed = false;
        //flipPhysicsPressed = false;
        //shootPressed = false;
        meleePressed = false;
        characterPhysPressed = false;
        environmentPhysPressed = false;
        distantCameraPressed = false;
        readyToClear = false;
    }
    public void ProcessInputs()
    {
        //Accumulate joysticks input

        //Accumulate button inputs
        //jumpPressed = jumpPressed || Input.GetButtonDown(InputInfo.JUMP);
        //interactPressed = Input.GetButtonDown(InputInfo.INTERACT);
        //flipPhysicsPressed = Input.GetButtonDown(InputInfo.FLIP_GRAVITY);
        meleePressed = Input.GetButtonDown(InputInfo.MELEE);

        //Accumulate physics inputs
        characterPhysPressed =Input.GetButtonDown(InputInfo.PLAYER_PHYS);
        environmentPhysPressed = Input.GetButtonDown(InputInfo.ENVIRONMENT_PHYS);


        distantCameraPressed = distantCameraPressed || Input.GetButton(InputInfo.CAMERA_DISTANT);
        //shootPressed = Input.GetButtonDown(InputInfo.SHOOT);


    }

    //Input events are yet to be done right
    public void OnMove(InputAction.CallbackContext context)
    {
        horizontal = Mathf.Clamp(context.ReadValue<Vector2>().x, -1, 1);
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        aim = ( context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        shootPressed = context.performed;
       
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpPressed = context.performed;
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interactPressed = context.performed;
        if (interactPressed)
            print("Interact");
    }

    public void OnSwitchGravity(InputAction.CallbackContext context)
    {
        flipPhysicsPressed = context.performed;
    }
}
