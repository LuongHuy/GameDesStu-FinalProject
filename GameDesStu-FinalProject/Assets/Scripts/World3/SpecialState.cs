using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialState : MoveState
{
    public override void OnEnter()
    {
        //Debug.Log("Enter Special mode");

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
        base.StateChange();
    }
}

public class Dash: SpecialState
{
    float _coyote;

    float _dashTime;
    bool _groundDash;
    public override void OnEnter()
    {
        //Debug.Log("Enter dash mode");
        _dashTime = 0;
        _coyote = 0;
        _groundDash = player.CheckIsGround();
        player.UpdateGravityScale(0);
        player.PlayAnimation("Dashing");
        player.ActivateTrail();
    }

    public override void OnExit()
    {
        //Debug.Log("Exit Special mode");
        player.resetVelocity();
        //Debug.Log(_dashTime);
        player.UpdateGravityScale(1);
        player.DeactivateTrail();
    }

    public override void StateChange() { 
        base.StateChange();

        _dashTime += Time.deltaTime;

        // if below minimum dash, skip
        if (player.CheckMinDash(_dashTime))
        {
            return;
        }


        // if within the time, and the player press jump, still allow to jump
        if (player.CheckIsGround())
        {
            if (Input.GetButtonDown("Jump"))
            {
                player.TransitTo(new JumpNoDash());
                return;
            }
        }
        else
        {
            _coyote += Time.deltaTime;
            // if within the time, and the player press jump, still allow to jump
            if (player.checkCoyote(_coyote) && Input.GetButtonDown("Jump"))
            {
                player.TransitTo(new JumpNoDash());
                return;
            }
        }

        // dash time is over, or the player release dash button
        if (player.CheckDashTime(_dashTime) || !(Input.GetButton("Action1")||Input.GetKey(KeyCode.LeftShift)))
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
        //Debug.Log("Enter jump but no dash mode");
        player.Jump();
        player.PlayAnimation("Jumping");
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
        if (!Input.GetButton("Jump"))
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
    float _enemyJumpBuffer;
    public override void OnEnter()
    {
        //Debug.Log("Enter fall but no dash mode");
        player.UpdateGravityScale(2);
        player.PlayAnimation("Jumping");
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
                player.TransitTo(new Bounch());
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
                return;
            }
            else
            {
                player.TransitTo(new Idle());
            }
        }
    }
}