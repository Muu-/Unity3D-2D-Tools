using UnityEngine;
using System.Collections;

public class SpeedTests : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
			RayCastTests();
		
		if (Input.GetKeyDown(KeyCode.S))
			VectorSum();
		if (Input.GetKeyDown(KeyCode.A))
			VectorSumAvg();
	}
	
	void RayCastTests() {
		//Checks time required to raycast at different distances	
		float time;
		Ray ray;
		int repeat = 10000000;
		
		Debug.Log("Single Line Rays: ");
		time = Time.realtimeSinceStartup;
		ray = new Ray(new Vector3(0,0,900000), new Vector3(0,0,-1));
		for (int i = 0; i<repeat; i++){
			Physics.Raycast(ray, 1000000);
		}
		
		Debug.Log("Raycasting " + repeat + " rays x 1Mm: " + (Time.realtimeSinceStartup - time));
		time = Time.realtimeSinceStartup;
		ray = new Ray(new Vector3(0,0,80), new Vector3(0,0,-1));
		for (int i = 0; i<repeat; i++){
			Physics.Raycast(ray, 100);
		}
		Debug.Log("Raycasting " + repeat + " rays x 100m: " + (Time.realtimeSinceStartup - time));
		
		Debug.Log("Oblique Line Rays: ");
		time = Time.realtimeSinceStartup;
		ray = new Ray(new Vector3(0,0,900000), new Vector3(1,1,-1));
		for (int i = 0; i<repeat; i++){
			Physics.Raycast(ray, 1000000);
		}
		
		Debug.Log("Raycasting " + repeat + " rays x 1Mm: " + (Time.realtimeSinceStartup - time));
		time = Time.realtimeSinceStartup;
		ray = new Ray(new Vector3(0,0,80), new Vector3(1,1,-1));
		for (int i = 0; i<repeat; i++){
			Physics.Raycast(ray, 100);
		}
		Debug.Log("Raycasting " + repeat + " rays x 100m: " + (Time.realtimeSinceStartup - time));
	}
	
	void VectorSum() {
		float time;
		int repeat = 10000000;
		
		Vector3 v3a = new Vector3(Random.value, Random.value, Random.value);
		Vector3 v3b = new Vector3(Random.value, Random.value, Random.value);
		Vector3 v3c = Vector3.zero;
		
		time = Time.realtimeSinceStartup;
		for (int i = 0; i<repeat; i++){
			v3c = v3a + v3b;
		}
		Debug.Log("Sum of " + repeat + " vector3: " + (Time.realtimeSinceStartup - time));
		
		time = Time.realtimeSinceStartup;
		for (int i = 0; i<repeat; i++){
			v3c = new Vector3((v3a.x + v3b.x), (v3a.y + v3b.y), 0);
		}
		Debug.Log("Trying with " + repeat + " vector3 (x,y,0): " + (Time.realtimeSinceStartup - time));
		
		//comment this out
		Debug.Log(v3c);
	}
	
	void VectorSumAvg() {
		float time;
		int repeat = 10000000;
		
		Vector3 v3a = new Vector3(Random.value, Random.value, Random.value);
		Vector3 v3b = new Vector3(Random.value, Random.value, Random.value);
		Vector3 v3c = Vector3.zero;
		
		time = Time.realtimeSinceStartup;
		for (int i = 0; i<repeat; i++){
			v3c = (v3a + v3b)/2;
		}
		Debug.Log("Sum of " + repeat + " vector3: " + (Time.realtimeSinceStartup - time));
		
		time = Time.realtimeSinceStartup;
		for (int i = 0; i<repeat; i++){
			v3c = new Vector3((v3a.x + v3b.x)/2, (v3a.y + v3b.y)/2, 0);
		}
		Debug.Log("Trying with " + repeat + " vector3 (x,y,0): " + (Time.realtimeSinceStartup - time));
		
		//comment this out
		Debug.Log(v3c);
	}
}
