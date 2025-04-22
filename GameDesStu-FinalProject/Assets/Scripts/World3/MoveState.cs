using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class MoveState
{
    protected PlayerMovementW3 player;

    // Move input from player;
    protected Vector2 moveInput;

    public void SetPlayerMovement(PlayerMovementW3 player)
    {
        this.player = player;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Move();
    public virtual void StateChange()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
    }
}

public class BasicMoveState: MoveState
{
    public override void OnEnter()
    {
        //Debug.Log("Enter Basic mode");
    }

    public override void OnExit()
    {
        //Debug.Log("Exit Basic mode");
    }

    public override void Move()
    {
        player.HorizontalMove(moveInput);
    }

    public override void StateChange()
    {
        base.StateChange();

        // if press action then dash
        if (Input.GetButtonDown("Action1")||Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.Dash(moveInput);
            player.TransitTo(new Dash());
            return;
        }

    }
}

public class Idle: BasicMoveState
{
    float _coyote;
    public override void OnEnter()
    {
        //Debug.Log("Enter Idle");
        _coyote = 0;
        player.PlayAnimation("Idle");
    }
    public override void OnExit()
    {
        //Debug.Log("Exit Idle");
    }
    public override void Move()
    {
        base.Move();
    }
    public override void StateChange()
    {
        base.StateChange();

        // if in the air, transit to different state
        if (!player.CheckIsGround())
        {
            _coyote += Time.deltaTime;
            // if within the time, and the player press jump, still allow to jump
            if (player.checkCoyote(_coyote) && Input.GetButtonDown("Jump"))
            {
                player.TransitTo(new Jump());
            }
            // if the player do not press jump in time, and the time run out, then transit to fall
            else if (!player.checkCoyote(_coyote))
            {
                player.TransitTo(new Fall());
            }
            return;
        }

        // if the player press move, then transit to run state
        if (moveInput.x != 0)
        {
            player.TransitTo(new Run());
            return;
        }

        // if the player jump
        if (Input.GetButtonDown("Jump"))
        {
            player.TransitTo(new Jump());
            return;
        }
    }
}
public class Run: BasicMoveState
{
    float _coyote;
    public override void OnEnter()
    {
        //Debug.Log("Enter Run");
        _coyote = 0;
        player.PlayAnimation("Moving");
    }
    public override void OnExit()
    {
        //Debug.Log("Exit Run");
    }
    public override void Move()
    {
        base.Move();
    }

    public override void StateChange()
    {
        base.StateChange();

        // if in the air, transit to different state
        if (!player.CheckIsGround())
        {
            _coyote += Time.deltaTime;
            // if within the time, and the player press jump, still allow to jump
            if (player.checkCoyote(_coyote) && Input.GetButtonDown("Jump"))
            {
                player.TransitTo(new Jump());
            }
            // if the player do not press jump in time, and the time run out, then transit to fall
            else if (!player.checkCoyote(_coyote))
            {
                player.TransitTo(new Fall());
            }
            return;
        }

        // if the player jump
        if (Input.GetButtonDown("Jump"))
        {
            player.TransitTo(new Jump());
            return;
        }

        // if the player stop moving
        if (moveInput.x == 0) { 
            player.TransitTo(new Idle());
            return;
        }

    }
}
public class Jump: BasicMoveState
{
    float _jumptime;
    public override void OnEnter()
    {
        //Debug.Log("Enter jump");
        player.Jump();
        _jumptime = 0;
        player.PlayAnimation("Jumping");
    }
    public override void OnExit()
    {
        //Debug.Log("Exit Jump");
        //Debug.Log("Jump time: "+_jumptime);
    }

    public override void StateChange()
    {
        base.StateChange();

        _jumptime += Time.deltaTime;

        // if the time of the jump is smaller than the minimum time jump, then do not transit to other state
        if (!player.CheckTimeJump(_jumptime))
        {
            return;
        }

        // if release the jump button, immediately switch to falling
        if (!Input.GetButton("Jump"))
        {
            player.TransitTo(new Fall());
            return ;
        }

        // if start falling, then switch to falling.
        if (player.CheckFalling() )
        {
            player.TransitTo(new Fall());
            return;
        }

        // if jump on a ground, switch to corresponding position.
        if (player.CheckIsGround())
        {
            if (moveInput.x != 0)
            {
                player.TransitTo(new Run());
            }
            else
            {
                player.TransitTo(new Idle());
            }
            return;
        }
    }
}
public class Bounch: BasicMoveState
{
    float _jumptime;
    public override void OnEnter()
    {
        //Debug.Log("Enter bounch");
        player.Bounch();
        _jumptime = 0;
        player.PlayAnimation("Jumping");
    }
    public override void OnExit()
    {
        //Debug.Log("Exit Jump");
        //Debug.Log("Bounce time: " + _jumptime);
    }

    public override void StateChange()
    {
        base.StateChange();

        _jumptime += Time.deltaTime;

        // if the time of the jump is smaller than the minimum time jump, then do not transit to other state
        if (!player.CheckTimeJump(_jumptime))
        {
            return;
        }

        // if start falling, then switch to falling.
        if (player.CheckFalling() )
        {
            player.TransitTo(new Fall());
            return;
        }

        // if jump on a ground, switch to corresponding position.
        if (player.CheckIsGround())
        {
            if (moveInput.x != 0)
            {
                player.TransitTo(new Run());
            }
            else
            {
                player.TransitTo(new Idle());
            }
            return;
        }
    }
}
public class Fall: BasicMoveState
{
    float _jumptime;
    float _buffer;
    bool _pressJump;
    float _enemyJumpBuffer;

    public override void OnEnter()
    {
        //Debug.Log("Enter Fall");
        player.UpdateGravityScale(2);
        _jumptime = 0;
        _pressJump = false;
        player.PlayAnimation("Jumping");
    }
    public override void OnExit()
    {
        //Debug.Log("Exit Fall");
        player.UpdateGravityScale(1);
        //Debug.Log("Fall time: "+ _jumptime);
    }
    public override void Move()
    {
        base.Move();
    }
    public override void StateChange()
    {
        base.StateChange();

        _jumptime += Time.deltaTime;
        _buffer += Time.deltaTime;
        _enemyJumpBuffer += Time.deltaTime;

        if (Input.GetButtonDown("Jump") && !_pressJump)
        {
            _pressJump = true;
            _buffer = 0;
            _enemyJumpBuffer = 0;
        }

        if (player.CheckStepOnEnemy())
        {
            // buffer enemy jump.
            // If they touch the enemy on the head within buffer time, they jump
            if (_pressJump && player.CheckJumpBuffer(_enemyJumpBuffer))
            {
                player.TransitTo(new Jump());
                return;
            }
            else
            {
                player.TransitTo(new Jump());
                return;
            }
        }

        if (player.CheckIsGround())
        {
            // buffer jump.
            // If they touch the ground within buffer time, they jump even if they press in the air
            if (_pressJump && player.CheckJumpBuffer(_buffer))
            {
                player.TransitTo(new Jump());
            }

            if (moveInput.x != 0)
            {
                player.TransitTo(new Run());
            }
            else
            {
                player.TransitTo(new Idle());
            }
            return;
        }
    }
}

