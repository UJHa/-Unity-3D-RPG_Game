using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
 {
    public enum eCharacterType
    {
        PLAYER,
        MONSTER,
        NONE,
    }

    protected eCharacterType _characterType = eCharacterType.NONE;
    public eCharacterType GetCharacterType()
    {
        return _characterType;
    }

    // Use this for initialization
    void Start ()
    {
        Init();
    }

    virtual public void Init()
    {
        InitState();
        InitAttackInfo();
        InitDamageInfo();
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateCharacter();
    }
    virtual public void UpdateCharacter()
    {
        UpdateChangeState();
        _stateDictionary[_stateType].Update();
    }

    protected Vector3 _targetPosition = Vector3.zero;
    public Vector3 GetTargetPosition()
    {
        return _targetPosition;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public Quaternion GetRotation()
    {
        return transform.rotation;
    }
    public bool IsGrounded()
    {
        return gameObject.GetComponent<CharacterController>().isGrounded;
    }

    // State
    public enum eState
    {
        IDLE,
        MOVE,
        ATTACK,
        CHASE,
        PATROL,
    }
    protected eState _stateType = eState.IDLE;
    eState _nextStateType = eState.IDLE;

    protected Dictionary<eState, State> _stateDictionary = new Dictionary<eState, State>();
    virtual protected void InitState()
    {
        State idleState = new IdleState();
        idleState.Init(this);
        _stateDictionary.Add(eState.IDLE, idleState);

        State moveState = new MoveState();
        moveState.Init(this);
        _stateDictionary.Add(eState.MOVE, moveState);

        State attackState = new AttackState();
        attackState.Init(this);
        _stateDictionary.Add(eState.ATTACK, attackState);
        
        State chaseState = new ChaseState();
        chaseState.Init(this);
        _stateDictionary.Add(eState.CHASE, chaseState);

        State patrolState = new PatrolState();
        patrolState.Init(this);
        _stateDictionary.Add(eState.PATROL, patrolState);
    }
    public void ChangeState(eState stateType)
    {
        _nextStateType = stateType;
    }
    void UpdateChangeState()
    {
        if (_nextStateType != _stateType)
        {
            _stateDictionary[_stateType].Stop();
            _stateType = _nextStateType;
            if(_stateDictionary.ContainsKey(_stateType))
                _stateDictionary[_stateType].Start();
            else
                Debug.LogError("Can't find state " + _stateType + " of " + gameObject.name);
        }
    }
    //idle
    public float GetRefreshTime()
    {
        return 3.0f;
    }
    public void Patrol()
    {
        ChangeState(eState.PATROL);
    }
    protected bool _isSetMovePosition = false;
    public bool IsSetMovePosition()
    {
        return _isSetMovePosition;
    }
    public void SetMovePosition(bool setMovePosition)
    {
        _isSetMovePosition = setMovePosition;
    }

    //move
    public void Move(Vector3 velocity)
    {
        gameObject.GetComponent<CharacterController>().Move(velocity);
    }
    public void Rotate(Vector3 direction)
    {
        Quaternion characterTargetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, 540.0f * Time.deltaTime);
    }

    public GameObject _charactorVisual;
    public void SetAnimationTrigger(string triggerName)
    {
        _charactorVisual.GetComponent<Animator>().SetTrigger(triggerName);
    }

    public AnimationPlayer GetAnimationPlayer()
    {
        return _charactorVisual.GetComponent<AnimationPlayer>();
    }

    virtual public void ArriveDestination()
    {
        ChangeState(Player.eState.IDLE);
    }

    //Attack
    AttackArea[] _attackAreas;
    void InitAttackInfo()
    {
        _attackAreas = GetComponentsInChildren<AttackArea>();
    }
    public void AttackStart()
    {
        for (int i = 0; i < _attackAreas.Length; i++)
            _attackAreas[i].Enable();
    }
    public void AttackEnd()
    {
        for (int i = 0; i < _attackAreas.Length; i++)
            _attackAreas[i].Disable();
    }

    //Hit area
    void InitDamageInfo()
    {
        HitArea[] hitAreas = GetComponentsInChildren<HitArea>();
        for (int i = 0; i < hitAreas.Length; i++)
        {
            hitAreas[i].Init(this);
        }
    }

    //Chase
    protected GameObject _targetObject = null;
    public GameObject GetTargetObject()
    {
        return _targetObject;
    }

    public float GetAttackRange()
    {
        return 1.5f;
    }
    public void SetTargetObject(GameObject targetObject)
    {
        _targetObject = targetObject;
    }
    virtual public void StopChase()
    {
    }
    virtual public bool IsSearchRange(float distance)
    {
        return false;
    }
}
