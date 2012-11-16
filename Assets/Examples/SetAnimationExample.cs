using UnityEngine;
using System.Collections;

public class SetAnimationExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//This is how an animation should be called
		UVSprite mySprite = transform.GetComponent<UVSprite>();
		mySprite.SetAnimation("one");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			//This will change current mirroring status
			UVSprite mySprite = transform.GetComponent<UVSprite>();
			mySprite.SetMirror();
		}
	}
}
