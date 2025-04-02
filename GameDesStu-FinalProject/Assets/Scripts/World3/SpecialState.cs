using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialState : MoveState
{
    public override void OnEnter()
    {
        Debug.Log("Enter Special mode");
    }

    public override void OnExit()
    {
        //Debug.Log("Exit Special mode");
    }

    public override void Move()
    {
        // smt
    }

    public override void StateChange()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        // smt
    }
}

public class Dash: SpecialState
{
    float _coyote;

    float _dashTime;
    bool _groundDash;
    public override void OnEnter()
    {
        Debug.Log("Enter dash mode");
        _dashTime = 0;
        _coyote = 0;
        _groundDash = player.CheckIsGround();
    }

    public override void OnExit()
    {
        //Debug.Log("Exit Special mode");
        player.resetVelocity();
        //Debug.Log(_dashTime);
    }
    public override void Move()
    {
        base.Move();
    }
    public override void StateChange() { 
        base.StateChange();

        _dashTime += Time.deltaTime;

        // if within the time, and the player press jump, still allow to jump
        if (player.CheckIsGround())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.TransitTo(new JumpNoDash());
                return;
            }
        }
        else
        {
            _coyote += Time.deltaTime;
            // if within the time, and the player press jump, still allow to jump
            if (player.checkCoyote(_coyote) && Input.GetKeyDown(KeyCode.Space))
            {
                player.TransitTo(new JumpNoDash());
                return;
            }
        }

        // dash time is over, or the player release dash button
        if (!player.CheckDashTime(_dashTime) || !Input.GetKey(KeyCode.F))
        {
            // if the player is on the ground
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
            }
            else
            {
                player.TransitTo(new FallNoDash());
            }
        }
    } 

}

public class JumpNoDash: SpecialState
{
    float _jumptime;
    public override void OnEnter()
    {
        Debug.Log("Enter jump but no dash mode");
        player.Jump();
    }

    public override void OnExit()
    {
        //Debug.Log("Exit Special mode");
    }
    public override void Move()
    {
        base.Move();
        player.HorizontalMove(moveInput);
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
        if (!Input.GetKey(KeyCode.Space))
        {
            player.TransitTo(new FallNoDash());
            return;
        }

        // if start falling, then switch to falling.
        if (player.CheckFalling())
        {
            player.TransitTo(new FallNoDash());
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

public class FallNoDash : SpecialState
{
    float _buffer;
    bool _pressJump;
    public override void OnEnter()
    {
        Debug.Log("Enter fall but no dash mode");
        player.UpdateGravityScale(2);
    }

    public override void OnExit()
    {
        //Debug.Log("Exit Special mode");

        player.UpdateGravityScale(1);
    }
    public override void Move()
    {
        base.Move();
        player.HorizontalMove(moveInput);
    }
    public override void StateChange()
    {
        base.StateChange();

        _buffer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && !_pressJump)
        {
            _pressJump = true;
            _buffer = 0;
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
                return;
            }
            else
            {
                player.TransitTo(new Idle());
            }
        }
    }
}