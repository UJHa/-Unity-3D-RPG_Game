using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    override public void Init()
    {
        base.Init();
        _characterType = eCharacterType.PLAYER;
    }
    override public void UpdateCharacter ()
    {
        base.UpdateCharacter();
        UpdateInput();
    }
    void UpdateInput()
    {
        if (InputManager.Instance.IsMouseDown(InputManager.eButtonSort.LEFT_BUTTON))
        {
            Vector3 mousePosition = InputManager.Instance.GetCursorPosition();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100.0f, 1 << LayerMask.NameToLayer("Ground") 
                                                        | 1 << LayerMask.NameToLayer("Character")))
            {
                if(LayerMask.NameToLayer("Ground") == hitInfo.collider.gameObject.layer)
                {
                    _targetPosition = hitInfo.point;
                    _targetObject = null;
                    _isSetMovePosition = true;
                    //_stateDictionary[_stateType].UpdateInput();
                }
                if (LayerMask.NameToLayer("Character") == hitInfo.collider.gameObject.layer)
                {
                    //Character character = hitInfo.collider.gameObject.GetComponent<Character>();
                    HitArea hitArea = hitInfo.collider.GetComponent<HitArea>();
                    Character character = hitArea.GetCharacter();
                    switch (character.GetCharacterType())
                    {
                        case eCharacterType.MONSTER:
                            Debug.Log("몬스터다 깽깽이들아");
                            _targetPosition = hitInfo.collider.gameObject.transform.position;
                            _targetObject = hitInfo.collider.gameObject;
                            _isSetMovePosition = true;
                            ChangeState(eState.CHASE);
                            break;
                    }
                }
            }
        }

        //if (InputManager.Instance.IsAttackButtonDown())
        //{
        //    ChangeState(eState.ATTACK);
        //}
    }
    override public void StopChase()
    {
        ChangeState(eState.IDLE);
    }
    public override bool IsSearchRange(float distance)
    {
        return false;
    }
}