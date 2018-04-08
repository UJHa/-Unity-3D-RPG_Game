using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    override public void Start()
    {
        base.Start();
        _character.GetAnimationPlayer().Play("attack", () =>
        {
            _character.AttackStart();
        }, () =>
        {

        }, () =>
        {
            _character.AttackEnd();
        }, () =>
        {
            _character.ChangeState(Player.eState.IDLE);
        });
    }
    override public void Update()
    {
        base.Update();
    }
}