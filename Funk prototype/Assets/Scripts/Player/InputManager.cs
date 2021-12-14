using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{

    public static InputManager instance = null;
    //Joysticks used
    public float horizontal { get; private set; }
    public (float x, float y) aim { get; private set; }

    //Front buttons used
    public bool jumpPressed { get; private set; }
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

        if (horizontal > 0)
            horizontal = 1f;
        if(horizontal < 0)
        {
            horizontal = -1f;
        }

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
        aim = (0f, 0f);
        //Reset all inputs
        horizontal = 0f;
        jumpPressed = false;
        interactPressed = false;
        flipPhysicsPressed = false;
        shootPressed = false;
        meleePressed = false;
        characterPhysPressed = false;
        environmentPhysPressed = false;
        distantCameraPressed = false;
        readyToClear = false;
    }
    public void ProcessInputs()
    {
        //Accumulate joysticks input
        horizontal += Input.GetAxis(InputInfo.MOVE);
        aim = (Input.GetAxis(InputInfo.AIM_X), Input.GetAxis(InputInfo.AIM_Y));

        //Accumulate button inputs
        jumpPressed = jumpPressed || Input.GetButtonDown(InputInfo.JUMP);
        interactPressed =  Input.GetButtonDown(InputInfo.INTERACT);
        flipPhysicsPressed = Input.GetButtonDown(InputInfo.FLIP_GRAVITY);
        meleePressed = Input.GetButtonDown(InputInfo.MELEE);

        //Accumulate physics inputs
        characterPhysPressed =Input.GetButtonDown(InputInfo.PLAYER_PHYS);
        environmentPhysPressed = Input.GetButtonDown(InputInfo.ENVIRONMENT_PHYS);


        distantCameraPressed = distantCameraPressed || Input.GetButton(InputInfo.CAMERA_DISTANT);
        shootPressed =  Input.GetButtonDown(InputInfo.SHOOT);


    }
}
