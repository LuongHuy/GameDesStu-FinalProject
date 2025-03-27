using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PlayerMovementW3 : MonoBehaviour
{
    // import component
    [SerializeField] private Rigidbody2D rd;
    [SerializeField] private BoxCollider2D groundCheckcld;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private SpriteRenderer sr;

    // movement parameter
    [SerializeField] float maxVelocity = 7f;
    [SerializeField] float time = 0.2f;
    [Range(0f, 1f)]
    [SerializeField] float friction = 0.6f;
    // jump parameter
    [SerializeField] float jumpHeight = 1.5f;
    [SerializeField] float coyoteMax = 1;
    [SerializeField] float jumpBufferMax = 1;
    [SerializeField] float JumpTimeMin = 1;
    // Dash movement
    [SerializeField] float dashVelocity = 30f;
    public float dashTime = 0.1f;

    // private parameter
    float curr_velocity;
    float coyote;
    float jump_buffer;
    float jump_time;

    // state_machine
    MoveState currMoveState = null;

    // Start is called before the first frame update
    void Start()
    {
        // Start at idle state
        TransitTo(new Idle());
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
        float horizontalForce = moveInput.x * (maxVelocity - Mathf.Abs(rd.velocity.x)) / time * rd.mass;
        rd.AddForce(new Vector2(horizontalForce, rd.velocity.y), ForceMode2D.Force);

        if (CheckIsGround() && (moveInput.x == 0 || moveInput.x * rd.velocity.x < 0))
        {
            rd.velocity = new Vector2(rd.velocity.x*friction, rd.velocity.y);
        }
    }

    public void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rd.gravityScale) * -2) * rd.mass;
        rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Dash(Vector2 moveInput)
    {
        //before dash, reset the player speed. This allows dash to overcome momemtum
        rd.velocity = Vector2.zero;

        // Activate dash
        float dashForce =  dashVelocity / dashTime * rd.mass;
        Vector2 direction = moveInput==Vector2.zero ? Vector2.right : moveInput.normalized;
        Debug.Log(dashForce + " " + direction);
        rd.AddForce(direction * dashVelocity, ForceMode2D.Impulse);
    }
    //
    bool isGround;
    // return true if is on the ground
    public bool CheckIsGround()
    {
        isGround = Physics2D.OverlapAreaAll(groundCheckcld.bounds.min, groundCheckcld.bounds.max, groundMask).Length > 0;
        return isGround;
    }

    public void resetVelocity()
    {
        Vector2 resetVelocity;
        // bound velocity x from -maxVelocity to maxVelocity
        resetVelocity.x = Mathf.Clamp(rd.velocity.x, -maxVelocity, maxVelocity);
        resetVelocity.y = Mathf.Min(rd.velocity.y, 0);
        rd.velocity = resetVelocity;
    }

}
