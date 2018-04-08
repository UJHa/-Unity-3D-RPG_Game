using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Animation Play
    System.Action _beginCallbock = null;
    System.Action _preMidCallbock = null;
    System.Action _afterMidCallbock = null;
    System.Action _endCallbock = null;
    public void Play(string triggerName,
        System.Action beginCallback,
        System.Action preMidCallback,
        System.Action afterMidCallback,
        System.Action endCallback)
    {
        gameObject.GetComponent<Animator>().SetTrigger(triggerName);
        _beginCallbock = beginCallback;
        _preMidCallbock = preMidCallback;
        _afterMidCallbock = afterMidCallback;
        _endCallbock = endCallback;
    }

    public void OnBeginEvent()
    {
        if (_beginCallbock != null)
            _beginCallbock();
    }

    public void OnAfterMidEvent()
    {
        if (_afterMidCallbock != null)
            _afterMidCallbock();
    }

    public void OnEndEvent()
    {
        if (_endCallbock != null)
            _endCallbock();
    }
}