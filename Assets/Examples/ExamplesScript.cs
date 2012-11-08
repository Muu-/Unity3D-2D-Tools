using UnityEngine;
using System.Collections;

public class ExamplesScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UVSprite spr = transform.GetComponent<UVSprite>();
		switch (gameObject.name) {
		case "Loop":
			spr.setAnimation("loopexample");
			break;
		case "one-two":
			spr.setAnimation("one");
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
