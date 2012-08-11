using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class UVSprite : MonoBehaviour {
	// Mehs ref used to store the mesh, used in most UVSet calls.
	private Mesh 	mesh;
	
	// Cell horizontal and vertical size
	// Plus starting cells and if the sprite should be mirrored or not
	public int 		cellHorizontalSize = 0,
					cellVerticalSize = 0,
					startingHorizontalCell = 0,
					startingVerticalCell = 0;
	public bool 	startMirrored = false;
	
	// Store animations to play during runtime
	public UVAnimation[] animations = new UVAnimation[0];
	
	// Use this for initialization
	void Awake () {
		// Store the actual mesh and set the starting image when the game starts
		mesh = GetComponent<MeshFilter>().mesh;
		UVFunctions.SetUVByPixelSizes(new Vector2(startingHorizontalCell, startingVerticalCell), new Vector2(cellHorizontalSize, cellVerticalSize), startMirrored, mesh, GetComponent<MeshRenderer>().material.mainTexture);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(new Vector2(cellHorizontalSize, cellVerticalSize));
		Debug.Log(mesh.uv[0]);
		Debug.Log(mesh.uv[1]);
		Debug.Log(mesh.uv[2]);
		Debug.Log(mesh.uv[3]);
		Debug.Log(GetComponent<MeshRenderer>().material.mainTexture.width);
		Debug.Log(GetComponent<MeshRenderer>().material.mainTexture.height);
	}
	
	// This class defines animations, stored using the inspector (or class constructor at runtime) and called back during runtime.
	[Serializable]
	public class UVAnimation {
		// Name is the name used to retrieve animation
		// NextAnimation store an animation to play after this one. Use "loop" or the same name for looping purposes.
		// Starting horizontal and vertical cells define where the animation should start
		// Frames defines how many frame the animation should last
		// Frame duration how much time each frame should last
		public string 	name = "",
						nextAnimation = "";
		public int	startingHorizontalCell = 0,
					srartingVerticalCell = 0,
					frames = 0;
		public float frameDuration = 0;
		
		public UVAnimation() {}
		
		public UVAnimation(string animationName, string nextAnim, int startHorCell, int startVerCell, int numberOfFrames, float frameTimeDuration) {
			name = animationName;
			nextAnimation = nextAnim;
			startingHorizontalCell = startHorCell;
			srartingVerticalCell = startVerCell;
			frames = numberOfFrames;
			frameDuration = frameTimeDuration;
		}
	}
	
}
