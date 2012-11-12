using UnityEngine;
using System.Collections;

public class ExamplesScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UVSprite mySprite = transform.GetComponent<UVSprite>();
		switch (gameObject.name) {
		case "Loop":
			mySprite.setAnimation("loopexample");
			break;
		case "one-two":
			mySprite.setAnimation("one");
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			UVSprite mySprite = transform.GetComponent<UVSprite>();
			mySprite.setMirror();
		}
	}
}
