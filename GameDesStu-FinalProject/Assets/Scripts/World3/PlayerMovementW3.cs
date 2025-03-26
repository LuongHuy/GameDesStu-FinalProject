using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovementW3 : MonoBehaviour
{
    // import component
    [SerializeField] private Rigidbody2D rd;

    // movement parameter
    [SerializeField] float max_velocity = 15;
    [SerializeField] float acceleration = 1;
    [SerializeField] float jump_height = 1;
    [SerializeField] float coyote_max = 1;
    [SerializeField] float jump_buffer_max = 1;
    [SerializeField] float jump_time_min = 1;

    // private parameter
    private Vector2 moveInput;
    float curr_velocity;

    float coyote;
    float jump_buffer;
    float jump_time;

    // state_machine
    MoveState currMoveState = null;

    // is on the ground
    bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        // Start at idle state
        TransitTo(new Idle());
    }

    // Update is called once per frame
    void Update()
    {
        //moveInput.x = Input.GetAxis("Horizontal");
        //moveInput.y = Input.GetAxis("Vertical");

        currMoveState.StateChange();
    }

    // for controlling movement
    private void FixedUpdate()
    {
        //HorizontalMove(moveInput);
        currMoveState.Move();
    }

    // Go to state
    public void TransitTo(MoveState state)
    {
        if (state == null)
        {
            return;
        }

        if (currMoveState != null)
        {
            this.currMoveState.OnExit();
        }
        state.SetPlayerMovement(this);
        currMoveState = state;

        this.currMoveState.OnEnter();
    }

    public void HorizontalMove(Vector2 moveInput)
    {
        Vector2 horizontalMovement = new Vector2(moveInput.x * max_velocity, rd.velocity.y);
        rd.AddForce(horizontalMovement, ForceMode2D.Force);
    }

    public void Jump()
    {
        float jumpForce = Mathf.Sqrt(jump_height * (Physics2D.gravity.y + rd.gravityScale) * -2) * rd.mass;
        rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // when collide with ground, then return ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
    // return true if is on the ground
    public bool CheckIsGround()
    {
        return isGround;
    }
}
