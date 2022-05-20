using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsInfo : MonoBehaviour
{

    public EventsChecker checker;
    public Rigidbody2D rb;
    public Rotator rotator;
    public int controlDirection = 0;
    public int direction = 1;
    public float initialX;


    public BoxCollider2D collider;
   



    [SerializeField]
    public LayerMask groundLayer;



    [Header("Ground Movement Variables")]
    public float moveSpeed = 16f;
    [Range(0f, 1f)] public float crouchModifier = .6f;
    [Range(0f, 1f)] public float slowModifier = .5f;
    public float acceleration = 7f;
    public float decceleration = 7f;
    public float velPower = 0.9f;
    public float linearDrag;

    public float currentSpeed;

    [Header("MidAir Movement Variables")]
    [Range(0f, 1f)] public float airHorizontalSpeed = .7f;

    public float jumpForce;
    public float standartGravity = 1f;
    public float fallMultiplier;
    public float lowJumpFallMultiplier;
    public float airLinearDrag;
    public float coyoteTime;
    public float jumpBufferTime;


    public float coyoteCounter;
    public float jumpBufferCounter;


    [Header("Status Flags")]
    [SerializeField] public bool isOnGround;
    [SerializeField] public bool isCrouching;
    [SerializeField] public bool isHeadBlocked;


    public float horizontalDirection;

    public bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f)
        || (rb.velocity.x < 0f && horizontalDirection > 0f);

    public bool isChangingGravity {  get; set; }


  


    void Awake()
    {
        checker = GetComponent<EventsChecker>();
        rb = GetComponent<Rigidbody2D>();
        rotator = GetComponent<Rotator>();
        currentSpeed = moveSpeed;
        collider = GetComponent<BoxCollider2D>();
     
        initialX = transform.localScale.x;

    }

   
    // Update is called once per frame
    private void FixedUpdate()
    {
        CheckPhysics();

        GroundMovement();
        MidAirMovement();


    }



    public void CheckPhysics()
    {
        if (isOnGround == false)
            return;
        if (isChangingGravity)
        {
            isChangingGravity = false;
        }
        else
        {
            checker.CanSwitchGravity = true;
        }
    }

   
   

    #region ground
    public void GroundMovement()
    {

        if (isOnGround)
        {
            ApplyLinearDrag();
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }
    public void ApplyLinearDrag()
    {
        if (Mathf.Abs(horizontalDirection) < 0.4f || changingDirection)
        {
            rb.drag = linearDrag;
        }

    }
    #endregion

    #region air
    public void MidAirMovement()
    {

        if (jumpBufferCounter != Time.deltaTime)
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (jumpBufferCounter > 0f || (isOnGround || coyoteCounter > 0f))
        {
            SetJump();
        }

        if (!isOnGround && !isChangingGravity)
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
        if (isChangingGravity)
            coyoteCounter = 0f;
    }
    public void SetJump()
    {
        jumpBufferCounter = jumpBufferTime;
    }
    public bool SuitsJump()
    {
        return jumpBufferCounter > 0f || (isOnGround || coyoteCounter > 0f);
    }
    public void FallMultiplier()
    {
        if (standartGravity > 0)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallMultiplier;
            }
            else if (rb.velocity.y > 0)
            {
                rb.gravityScale = lowJumpFallMultiplier;
            }
            else
            {
                rb.gravityScale = standartGravity;
            }
        }
        else if (standartGravity < 0)
        {
            if (rb.velocity.y > 0)
            {
                rb.gravityScale = fallMultiplier;
            }
            else if (rb.velocity.y < 0)
            {
                rb.gravityScale = lowJumpFallMultiplier;
            }
            else
            {
                rb.gravityScale = standartGravity;
            }
        }
    }

    public void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
    }

   
    public void FlipAirPhys()
    {
        FlipMultipliers();
        FlipJump();
        FlipStandartGravity();
        coyoteCounter = 0f;
    }

    public void FlipStandartGravity()
    {
        standartGravity = -standartGravity;
    }
    public void FlipJump()
    {
        jumpForce = -jumpForce;
    }

    public void FlipMultipliers()
    {
        lowJumpFallMultiplier = -lowJumpFallMultiplier;
        fallMultiplier = -fallMultiplier;
        rb.gravityScale = -rb.gravityScale;
        coyoteCounter = 0f;
    }

    #endregion

    #region getters&setters
    public LayerMask GetGroundLayer()
    {
        return groundLayer;
    }

    public void SetOnGround(bool isGr)
    {
        isOnGround = isGr;
    }

    public bool IsOnGround()
    {
        return isOnGround;
    }

    public void SetHeadBlocked(bool headBl)
    {
        isHeadBlocked = headBl;
    }
    public Rotator GetRotator()
    {
        return rotator;
    }


    public Transform GetTransform()
    {
        return transform;
    }
    #endregion

}

