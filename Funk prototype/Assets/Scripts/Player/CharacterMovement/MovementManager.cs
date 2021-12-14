using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class MovementManager : MonoBehaviour, IMovementManager
{
    private InputManager input;
    private Rigidbody2D rb;
    private IRotator rotator;

    [SerializeField] 
    private LayerMask groundLayer;



    [Header("Ground Movement Variables")]
    [SerializeField] private float moveSpeed = 16f;
    [SerializeField] private float acceleration = 7f;
    [SerializeField] private float decceleration = 7f;
    [SerializeField] private float velPower = 0.9f;
    [SerializeField] private float linearDrag;

    [Header("MidAir Movement Variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float standartGravity = 1f;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpFallMultiplier;
    [SerializeField] private float airLinearDrag;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpBufferTime;

    private float coyoteCounter;
    private float jumpBufferCounter;


    [Header("Status Flags")]
    [SerializeField] private bool isOnGround;
    [SerializeField] private bool isHeadBlocked;


    private float horizontalDirection;

    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f)
        || (rb.velocity.x < 0f && horizontalDirection > 0f);

    public bool isChangingGravity { private get; set; }


    public float Multiplier { 
        get
        {
            return fallMultiplier;
        } 
    }


    void Awake()
    {
        input = InputManager.instance;
        rb = GetComponent<Rigidbody2D>();
        rotator = new Rotator(transform, 0f, groundLayer);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckPhysics();

        GroundMovement();
        MidAirMovement();


    }
    public void CheckPhysics()
    {
        if (isOnGround && isChangingGravity)
        {
            isChangingGravity = false;
        }
    }

    public void MoveCharacter()
    {
        float targetSpeed = input.horizontal * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
        horizontalDirection = input.horizontal;
    }
    public void RotateCharacter()
    {

        StartCoroutine(rotator.Rotate(fallMultiplier));

    }

    #region ground
    public void GroundMovement()
    {
        MoveCharacter();

        if (isOnGround)
        {
            ApplyLinearDrag();
            coyoteCounter = coyoteTime;
        } else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }
    private void ApplyLinearDrag()
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
        if (input.jumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        } else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if(jumpBufferCounter > 0f && (isOnGround || coyoteCounter > 0f))
        {

            Jump();
        }
        if (!isOnGround && !isChangingGravity)
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
        if (isChangingGravity)
            coyoteCounter = 0f;
    }

    private void FallMultiplier()
    {
        if (standartGravity > 0)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !input.jumpPressed)
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
            else if (rb.velocity.y < 0 && !input.jumpPressed)
            {
                rb.gravityScale = lowJumpFallMultiplier;
            }
            else
            {
                rb.gravityScale = standartGravity;
            }
        }
    }

    private void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isOnGround = false;
        coyoteCounter = 0f;
        jumpBufferCounter = 0f;
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

    public void FlipGroundPhys()
    {
        moveSpeed *= -1;
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
    public IRotator GetRotator()
    {
        return rotator;
    }


    public Transform GetTransform()
    {
        return transform;
    }
    #endregion

}
