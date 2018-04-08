using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    override public void Init()
    {
        base.Init();
        _characterType = eCharacterType.MONSTER;
    }
    override protected void InitState()
    {
        base.InitState();
        State idleState = new WargIdleState();
        idleState.Init(this);
        _stateDictionary[eState.IDLE] = idleState;
    }
    public List<WayPoint> _wayPointList;
    int _wayPointIndex = 0;
    override public void ArriveDestination()
    {
        //WayPoint
        WayPoint wayPoint = _wayPointList[_wayPointIndex];
        _wayPointIndex = (_wayPointIndex + 1) % _wayPointList.Count;
        _targetPosition = wayPoint.GetPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.NameToLayer("CharacterCtrl") == other.gameObject.layer)
        {
            Character character = other.gameObject.GetComponent<Character>();
            if(eCharacterType.PLAYER == character.GetCharacterType())
            {
                Debug.Log("플레이어 발견!");
            }
            _targetObject = other.gameObject;
            ChangeState(eState.CHASE);
        }
    }
    override public void StopChase()
    {
        ChangeState(eState.PATROL);
    }
    public override bool IsSearchRange(float distance)
    {
        if (5.0f < distance)
            return true;
        return false;
    }
}
