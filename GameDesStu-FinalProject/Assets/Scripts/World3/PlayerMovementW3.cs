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
    [SerializeField] float jumpHeight = 1.5f;
    [SerializeField] float coyoteMax = 1;
    [SerializeField] float jumpBufferMax = 1;
    [SerializeField] float jumpTimeMin = 0.1f;

    [Header("Dash Parameter")]
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
        rd.AddForce(direction * dashVelocity, ForceMode2D.Impulse);
    }
  
    // return true if is on the ground
    public bool CheckIsGround()
    {
        bool isGround = Physics2D.OverlapAreaAll(groundCheckCollider.bounds.min, groundCheckCollider.bounds.max, groundMask).Length > 0;
        return isGround;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyWeakpoint"))
        {
            Attack(collision.gameObject);
        }
    }

    // Attack an enemy, will change in the future
    public void Attack(GameObject enemy)
    {
        Destroy(enemy);
    }

}
