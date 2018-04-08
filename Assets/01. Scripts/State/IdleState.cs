using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    override public void Start()
    {
        base.Start();
        _character.SetAnimationTrigger("idle");
    }
    override public void Update()
    {
        base.Update();
        if(_character.IsSetMovePosition())
        {
            _character.ChangeState(Player.eState.MOVE);
        }
    }
}
