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
	
	public UVAnimation[] animations = new UVAnimation[0];	// Store animations to play during runtime
	private UVAnimation currentAnimation = null;			// Stores the currently playing animation.
	private float animationTimer = 0;						// Used to check when to draw next frame
	private int currentFrame = 0;							// Stores frame No
	
	// Use this for initialization
	void Awake () {
		// Store the actual mesh and set the starting image when the game starts
		mesh = GetComponent<MeshFilter>().mesh;
		setStartingImage();
	}
	
	// Update is called once per frame
	void Update () {
		//If there's a currentAnimation stored, play it.
		if (currentAnimation != null)
			Play();
	}
	
	//Plays the animation.
	void Play() {
		animationTimer -= Time.deltaTime;
		
		if (animationTimer < 0) {
			currentFrame += 1;
			if (currentFrame < currentAnimation.frames)
			{
				setFramedImage();
				animationTimer = currentAnimation.frameDuration;
			}
			else
			{				
				if (string.IsNullOrEmpty(currentAnimation.nextAnimation) || currentAnimation.nextAnimation == "none")
				{
					setStartingImage();
					currentAnimation = null;

				}
				else if (currentAnimation.nextAnimation == currentAnimation.name || currentAnimation.nextAnimation == "loop")
				{
					resetAnimation();
				} 
				else
				{
					setAnimation(currentAnimation.nextAnimation);
				}			
			}
		}
	}
	
	// Sets starting image
	private void setStartingImage(){
		UVFunctions.SetUVByPixelSizes(new Vector2(startingHorizontalCell, startingVerticalCell), new Vector2(cellHorizontalSize, cellVerticalSize), startMirrored, mesh, GetComponent<MeshRenderer>().material.mainTexture);	
	}
	
	// Called during animation play to set current image index.
	private void setFramedImage() {
		UVFunctions.SetUVByPixelSizes(new Vector2((currentAnimation.startingHorizontalCell+currentFrame), currentAnimation.startingVerticalCell), new Vector2(cellHorizontalSize, cellVerticalSize), startMirrored, mesh, GetComponent<MeshRenderer>().material.mainTexture);	
			//Debug.Log("" + gameObject.name + " set frame " + (currentFrame+1).ToString() + " in <" + currentAnimation.name + "> as current image");
	
	}
	
	// Starts timer and set current frame to 1, allowing the animation to start (or to loop) from it's first frame.
	private void resetAnimation() {
		animationTimer = currentAnimation.frameDuration;
		currentFrame = 0;
		UVFunctions.SetUVByPixelSizes(new Vector2((currentAnimation.startingHorizontalCell), currentAnimation.startingVerticalCell), new Vector2(cellHorizontalSize, cellVerticalSize), startMirrored, mesh, GetComponent<MeshRenderer>().material.mainTexture);	
			//Debug.Log("" + gameObject.name + " started <" + currentAnimation.name + "> as current animation");
	}
	
	// Call this function to set current animation, it will automatically call reset function to start it.
	public void setAnimation (string animationName){
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i].name == animationName)
			{
				currentAnimation = animations[i];
				resetAnimation();
				return;
			}
		}
		Debug.LogWarning("Warning: " + gameObject.name + " was not able to find <" + animationName + "> animation");
	}
	
	// This class defines animations, stored using the inspector (or class constructor at runtime) and called back during runtime.
	[Serializable]
	public class UVAnimation {
		// Name is the name used to retrieve animation
		// NextAnimation store an animation to play after this one. Use "loop" or the same name for looping purposes. Use "none" to get back to orignal sprite at animation's end.
		// Starting horizontal and vertical cells define where the animation should start
		// Frames defines how many frames the animation should last
		// Frame duration how much time each frame should last
		public string 	name = "",
						nextAnimation = "";
		public int	startingHorizontalCell = 0,
					startingVerticalCell = 0,
					frames = 0;
		public float frameDuration = 0;
		
		public UVAnimation() {}
		
		public UVAnimation(string animationName, string nextAnim, int startHorCell, int startVerCell, int numberOfFrames, float frameTimeDuration) {
			name = animationName;
			nextAnimation = nextAnim;
			startingHorizontalCell = startHorCell;
			startingVerticalCell = startVerCell;
			frames = numberOfFrames;
			frameDuration = frameTimeDuration;
		}
	}
	
}
