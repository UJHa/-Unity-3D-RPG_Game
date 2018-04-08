using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(Character character)
    {
        _character = character;
    }

    Character _character;
    public Character GetCharacter()
    {
        return _character;
    }
}
