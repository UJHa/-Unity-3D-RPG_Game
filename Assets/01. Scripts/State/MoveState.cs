using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    Vector3 _destination;
    Vector3 _velocity = Vector3.zero;
    override public	void Start ()
    {
        base.Start();
        _character.SetAnimationTrigger("move");
        _destination = _character.GetTargetPosition();
    }

    override public void Update ()
    {
        base.Update();
        if (_character.IsSetMovePosition())
        {
            _destination = _character.GetTargetPosition();
        }
        _destination.y = _character.GetPosition().y;
        Vector3 direction = (_destination - _character.GetPosition()).normalized;
        _velocity = direction * 6.0f;

        Vector3 snapGround = Vector3.zero;

        if (_character.IsGrounded())
            snapGround = Vector3.down;
        // 목적지 설정 되있으면 >> 목적지, 현재 위치가 일정 거리 이상이면 -> 이동
        float distance = Vector3.Distance(_destination, _character.GetPosition());
        if (0.5f < distance)
        {
            _character.Move(_velocity * Time.deltaTime + snapGround);
            _character.Rotate(direction);
        }
        else
        {
            _character.ArriveDestination();
        }
    }
}
