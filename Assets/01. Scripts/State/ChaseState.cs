using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    Vector3 _velocity = Vector3.zero;
    override public void Start()
    {
        base.Start();
        _character.SetAnimationTrigger("move");
    }
    override public void Update()
    {
        base.Update();
        if (_character.IsSetMovePosition())
        {
            _character.ChangeState(Character.eState.MOVE);
            return;
        }
        if (null == _character.GetTargetObject())
        {
            _character.StopChase();
            return;
        }

        Vector3 destination = _character.GetTargetPosition();
        if (null != _character.GetTargetObject())
            destination = _character.GetTargetObject().transform.position;
        destination.y = _character.GetPosition().y;
        Vector3 direction = (destination - _character.GetPosition()).normalized;
        _velocity = direction * 3.0f;

        Vector3 snapGround = Vector3.zero;

        if (_character.IsGrounded())
            snapGround = Vector3.down;
        // 목적지 설정 되있으면 >> 목적지, 현재 위치가 일정 거리 이상이면 -> 이동
        float distance = Vector3.Distance(destination, _character.GetPosition());

        if (distance > _character.GetAttackRange())
        {
            _character.Move(_velocity * Time.deltaTime + snapGround);
            _character.Rotate(direction);
            if (_character.IsSearchRange(distance))
            {
                _character.SetTargetObject(null);
            }
        }
        else
        {
            _character.ChangeState(Character.eState.ATTACK);
        }
    }
    public override void Stop()
    {
        base.Stop();

    }
}