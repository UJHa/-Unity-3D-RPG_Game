using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        SettingCameraTransform();
    }

    // Update is called once per frame
    void Update ()
    {
        UpdateCameraRotation();
        SettingCameraTransform();
    }

    void SettingCameraTransform()
    {
        if (null != _lookTarget)
        {
            Vector3 startLookPosition = _lookTarget.GetPosition() + _offset;

            //카메라가 캐릭터와 일정한 거리를 두고 따라가게 하는 연산
            //카메라 시작 위치 설정
            Vector3 relativePos = _cameraRotation * new Vector3(0, 0, - _distance);
            transform.position = startLookPosition + relativePos;
            //카메라가 바라보는 각도(비추는 대상) 설정
            Vector3 endLookPosition = _lookTarget.GetPosition() + _offset;
            transform.LookAt(endLookPosition);
        }
    }

    Vector3 _startMousePosition = Vector3.zero;
    Vector3 _changedMousePosition = Vector3.zero;
    Quaternion _cameraRotation = Quaternion.identity;
    void UpdateCameraRotation()
    {
        if (InputManager.Instance.IsMouseDown(InputManager.eButtonSort.RIGHT_BUTTON))
        {
            _startMousePosition = InputManager.Instance.GetCursorPosition();
        }
        if (InputManager.Instance.IsMouseHold(InputManager.eButtonSort.RIGHT_BUTTON))
        {
            _changedMousePosition = InputManager.Instance.GetCursorPosition() - _startMousePosition;
            if (_changedMousePosition.y < -30.0f)
            {
                _changedMousePosition.y = -30.0f;
            }
            else if (_changedMousePosition.y > 80.0f)
            {
                _changedMousePosition.y = 80.0f;
            }
            _cameraRotation = Quaternion.Euler(
                50.0f - _changedMousePosition.y,
                _changedMousePosition.x,
                _changedMousePosition.z);
            SettingCameraTransform();
            //Debug.Log(_changedMousePosition);
            //Debug.Log(transform.eulerAngles);
        }
    }

    //Camera
    public Player _lookTarget = null;
    Vector3 _offset = new Vector3(0.0f, 2.5f, 0.0f);
    
    public float _distance = 5.0f;
}