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
        // smt
    }
}

public class Dash: SpecialState
{
    public override void OnEnter()
    {
        Debug.Log("Enter dash mode");
    }

    public override void OnExit()
    {
        //Debug.Log("Exit Special mode");
    }
    public override void Move()
    {
        base.Move();
    }
    public override void StateChange() { 
        base.StateChange();
    } 
}
