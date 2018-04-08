using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    //single ton
    static InputManager _instance = null;
    public static InputManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new InputManager();
                _instance.Init();
            }
            return _instance;
        }
    }
    void Init()
    {
        _buttonStateList = new List<eButtonState>();
        for (int i = 0; i < (int)eButtonSort.NONE; i++)
        {
            _buttonStateList.Add(eButtonState.UP);
        }
    }
    public void Update()
    {
        for (int i = 0; i < (int)eButtonSort.NONE; i++)
        {
            if (Input.GetMouseButton(i))
            {
                if (eButtonState.UP == _buttonStateList[i])
                    ButtonDown((eButtonSort)i);
                else if (eButtonState.DOWN == _buttonStateList[i])
                    ButtonHold((eButtonSort)i);
            }
            if (Input.GetMouseButtonUp(i))
            {
                ButtonUp((eButtonSort)i);
            }
        }
    }

    //mouse Input
    public enum eButtonSort
    {
        LEFT_BUTTON,
        RIGHT_BUTTON,
        NONE,
    }
    enum eButtonState
    {
        DOWN,
        HOLD,
        UP,
    }
    //eButtonState _buttonState = eButtonState.UP;
    List<eButtonState> _buttonStateList;
    void ButtonDown(eButtonSort buttonSort)
    {
        _buttonStateList[(int)buttonSort] = eButtonState.DOWN;
    }
    void ButtonUp(eButtonSort buttonSort)
    {
        _buttonStateList[(int)buttonSort] = eButtonState.UP;
    }
    void ButtonHold(eButtonSort buttonSort)
    {
        _buttonStateList[(int)buttonSort] = eButtonState.HOLD;
    }

    public bool IsMouseDown(eButtonSort buttonSort)
    {
        return (eButtonState.DOWN == _buttonStateList[(int)buttonSort]);
    }
    public bool IsMouseHold(eButtonSort buttonSort)
    {
        return (eButtonState.HOLD == _buttonStateList[(int)buttonSort]);
    }

    public Vector3 GetCursorPosition()
    {
        return Input.mousePosition;
    }

    public bool IsAttackButtonDown()
    {
        return IsMouseDown(eButtonSort.RIGHT_BUTTON);
    }
}