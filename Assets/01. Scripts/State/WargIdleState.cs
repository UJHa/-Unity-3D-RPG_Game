using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WargIdleState : State
{
    override public void Update ()
    {
        base.Update();
		if(_character.GetRefreshTime() <= _duration)
        {
            //_character.Patrol();
            _duration = 0.0f;
        }
        _duration += Time.deltaTime;
	}
    float _duration = 0.0f;
}
