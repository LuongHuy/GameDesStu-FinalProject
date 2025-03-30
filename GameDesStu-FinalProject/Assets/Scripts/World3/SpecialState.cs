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
    float timer;
    public override void OnEnter()
    {
        Debug.Log("Enter dash mode");
        timer = player.dashTime;
    }

    public override void OnExit()
    {
        //Debug.Log("Exit Special mode");
    }
    public override void Move()
    {
        base.Move();
        timer -= Time.deltaTime;
    }
    public override void StateChange() { 
        base.StateChange();
        
        // dash time is over
        if (timer < 0||Input.GetKeyUp(KeyCode.C))
        {
            // if the player is on the ground
            if (player.CheckIsGround())
            {
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
            else
            {
                player.TransitTo(new DashDisable());
            }
        }
    } 

}

public class DashDisable: SpecialState
{
    public override void OnEnter()
    {
        Debug.Log("Enter no dash mode");
        //player.resetVelocity();
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

        if (player.CheckIsGround())
        {
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
