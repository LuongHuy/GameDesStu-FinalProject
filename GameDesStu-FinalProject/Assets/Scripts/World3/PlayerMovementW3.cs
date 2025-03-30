using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PlayerMovementW3 : MonoBehaviour
{
    [Header("Import component")]
    // import component
    [SerializeField] private Rigidbody2D rd;
    [SerializeField] private BoxCollider2D groundCheckCollider;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private SpriteRenderer sr;

    // movement parameter
    [Header("Horizontal movement")]
    [SerializeField] float maxVelocity = 7f;
    [SerializeField] float time = 0.2f;
    [Range(0f, 1f)]
    [SerializeField] float friction = 0.6f;

    [Header("Jump Parameter")]
    // jump parameter
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float coyoteMax = 0.2f;
    [SerializeField] float jumpBufferMax = 0.2f;
    [SerializeField] float jumpTimeMin = 0.1f;
    [SerializeField] float jumpTimeMax = 0.5f;
    // special, for controlling falling speed
    [SerializeField] float gravityFallingScale = 2f;

    [Header("Dash Parameter")]
    // Dash movement
    [SerializeField] float dashVelocity = 30f;
    public float dashTime = 0.2f;

    // private parameter
    float curr_velocity;
    bool facingRight = true;

    // constant
    float JUMPFORCE;
    float DASHFORCE;
    float GRAVITYSCALE;

    // state_machine
    MoveState currMoveState = null;

    // Start is called before the first frame update
    void Start()
    {
        // Start at idle state
        TransitTo(new Idle());

        // Can not fully replace the gravity, because it is universal in Unity, though it is really slow.
        GRAVITYSCALE = (4 * jumpHeight / (jumpTimeMax * jumpTimeMax)) / Mathf.Abs(Physics2D.gravity.y);
        UpdateGravityScale(1);

        // To beter control the time of the jump: (1) g_scale*g = g_actual. (2) g_actual = 4*h/t^2
        JUMPFORCE = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * GRAVITYSCALE) * -2) * rd.mass;
    }

    // Update is called once per frame
    void Update()
    {
        currMoveState.StateChange();
    }

    // for controlling movement
    private void FixedUpdate()
    {
        currMoveState.Move();
    }

    // Go to state
    public void TransitTo(MoveState state)
    {
        // if the new state does not exist
        if (state == null)
        {
            return;
        }
        // Execute on exit
        if (currMoveState != null)
        {
            currMoveState.OnExit();
        }
        // Switch state
        state.SetPlayerMovement(this);
        currMoveState = state;
        // execute on enter
        currMoveState.OnEnter();
    }

    public void HorizontalMove(Vector2 moveInput)
    {
        //float horizontalForce = moveInput.x * maxVelocity / time * rd.mass;
        // scale movement force by different between max velocity and current velocity, but if greater then keep current velocity
        float speedDif = maxVelocity - Mathf.Abs(rd.velocity.x);
        speedDif = speedDif > 0 ? speedDif : 0 ;

        float horizontalForce = speedDif / time * rd.mass;

        rd.AddForce(new Vector2(horizontalForce * moveInput.x, rd.velocity.y), ForceMode2D.Force);

        // If the player do not press direction button, or move against the current direction, then add friction
        if (Mathf.Abs(moveInput.x) <=0.1f || moveInput.x * rd.velocity.x < 0)
        {
            // this is for friction ground only. Realistic, but harder to control jump.
            //if (CheckIsGround())
            //{
            //    rd.velocity = new Vector2(rd.velocity.x * friction, rd.velocity.y);
            //}

            // this is for friction on air and ground. Unrealistic, but let the player easier to control jump.
            rd.velocity = new Vector2(rd.velocity.x * friction, rd.velocity.y);
        }

        if (moveInput.x > 0)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
    }

    public void Jump()
    {
        rd.AddForce(Vector2.up * JUMPFORCE, ForceMode2D.Impulse);
    }

    public void Dash(Vector2 moveInput)
    {
        //before dash, reset the player speed. This allows dash to overcome momemtum
        rd.velocity = Vector2.zero;

        // Activate dash
        // default position right
        Vector2 direction = moveInput==Vector2.zero ? Vector2.right : moveInput.normalized;
        // apply force
        rd.AddForce(direction * dashVelocity, ForceMode2D.Impulse);
    }

    public void resetVelocity()
    {
        Vector2 resetVelocity;
        // bound velocity x from -maxVelocity to maxVelocity
        resetVelocity.x = Mathf.Clamp(rd.velocity.x, -maxVelocity, maxVelocity);
        // if velocity y is greater than 0, reset it to 0.
        resetVelocity.y = Mathf.Min(rd.velocity.y, 0);
        rd.velocity = resetVelocity;
    }

    public void UpdateGravityScale(float mode)
    {

        if (mode == 1)
        {
            rd.gravityScale = GRAVITYSCALE;
        }else if (mode == 2)
        {
            rd.gravityScale = GRAVITYSCALE * gravityFallingScale;
        }
    }

    // return true if is on the ground
    public bool CheckIsGround()
    {
        bool isGround = Physics2D.OverlapAreaAll(groundCheckCollider.bounds.min, groundCheckCollider.bounds.max, groundMask).Length > 0;
        return isGround;
    }
    // return true if is on the ground
    public bool CheckFalling()
    {
        return rd.velocity.y<=0;
    }

    public bool CheckTimeJump(float time)
    {
        return time>jumpTimeMin;
    }

    public bool checkCoyote(float time)
    {
        return time < coyoteMax;
    }

    public bool CheckJumpBuffer(float time)
    {
        return time < jumpBufferMax;
    }
}
