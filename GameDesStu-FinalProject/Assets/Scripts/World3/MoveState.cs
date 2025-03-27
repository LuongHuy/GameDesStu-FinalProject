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
    public abstract void StateChange();
}

public class BasicMoveState: MoveState
{
    public override void OnEnter()
    {
        Debug.Log("Enter Basic mode");
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
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        // if press action then dash
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("To dash");
            player.Dash(moveInput);
            player.TransitTo(new Dash());
            return;
        }
    }
}

public class Idle: BasicMoveState
{
    public override void OnEnter()
    {
        Debug.Log("Enter Idle");
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

        // if in the air, transit to jump state
        if (!player.CheckIsGround())
        {
            player.TransitTo(new Jump());
            return;
        }

        // if the player press move, then transit to run state
        if (moveInput.x != 0)
        {
            player.TransitTo(new Run());
            return;
        }

        // if the player jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
            player.TransitTo(new Jump());
            return;
        }
    }
}
public class Run: BasicMoveState
{
    public override void OnEnter()
    {
        Debug.Log("Enter Run");
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
        base .StateChange();

        // if in the air, transit to jump state
        if (!player.CheckIsGround())
        {
            player.TransitTo(new Jump());
            return;
        }

        // if the player jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
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
    public override void OnEnter()
    {
        Debug.Log("Enter jump");
    }
    public override void OnExit()
    {
        //Debug.Log("Exit Jump");
    }
    public override void Move()
    {
        base.Move();



    }

    public override void StateChange()
    {
        base.StateChange();

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

