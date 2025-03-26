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

    // movement parameter
    [SerializeField] float max_velocity = 15;
    [SerializeField] float time = 1;
    [Range(0f, 1f)]
    [SerializeField] float friction = 1;
    // jump parameter
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
        //float horizontalForce = moveInput.x * max_velocity / time * rd.mass;
        float horizontalForce = moveInput.x * (max_velocity - Mathf.Abs(rd.velocity.x)) / time * rd.mass;
        rd.AddForce(new Vector2(horizontalForce, rd.velocity.y), ForceMode2D.Force);

        if (CheckIsGround() && (moveInput.x == 0 || moveInput.x * rd.velocity.x < 0))
        {
            Debug.Log("Apply friction");
            rd.velocity = new Vector2(rd.velocity.x*friction, rd.velocity.y);
        }
    }

    public void Jump()
    {
        float jumpForce = Mathf.Sqrt(jump_height * (Physics2D.gravity.y + rd.gravityScale) * -2) * rd.mass;
        rd.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }
    //
    bool isGround;
    // return true if is on the ground
    public bool CheckIsGround()
    {
        isGround = Physics2D.OverlapAreaAll(groundCheckcld.bounds.min, groundCheckcld.bounds.max, groundMask).Length > 0;
        return isGround;
    }
}
