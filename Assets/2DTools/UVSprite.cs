using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class UVSprite : MonoBehaviour {
	// Mesh reference used to store the mesh, used in most UVSet calls.
	private Mesh 	mesh;
	
	// Starting variables: Cell horizontal and vertical size and starting cells
	public int 		cellHorizontalSize = 0,
					cellVerticalSize = 0,
					startingHorizontalCell = 0,
					startingVerticalCell = 0;
	
	// Variables used during runtime
	public bool 	mirroredImage = false;					// This bool will be used at start and during runtime to check if the sprite should be mirrored or not. Please set or toggle it using setMirror() or setMirror(bool).
	public UVAnimation[] animations = new UVAnimation[0];	// Store animations to play during runtime
	private UVAnimation currentAnimation = null;			// Stores the currently playing animation.
	private float animationTimer = 0;						// Used to check when to draw next frame
	private int currentFrame = 0;							// Stores frame No
	
	// Use this for initialization
	void Awake () {
		// Store the actual mesh and set the starting image when the game starts
		mesh = GetComponent<MeshFilter>().mesh;
		setSingleImage(startingHorizontalCell, startingVerticalCell);
	}
	
	// Update is called once per frame
	void Update () {
		//If there's a currentAnimation stored, play it.
		if (currentAnimation != null)
			Play();
	}
	
	//Plays the animation.
	void Play() {
		animationTimer -= Time.deltaTime; // First off, reduce animationTimer to check if we should switch frames.
		
		if (animationTimer < 0) {												// If this timer gets under 0 secs
			currentFrame += 1; 													// Start by increasing currentFrame value
			if (currentFrame < currentAnimation.frames)							// Check if there are more frames after current one
			{
				setFramedImage();												// If yes, set the next frame image
				animationTimer = currentAnimation.frameDuration;				// and reset animationTimer to wait for next frame
			}
			else 																// If the current frame was the last one
			{				
				if (currentAnimation.nextAnimation == "starting"  || currentAnimation.nextAnimation == "none")					// Reset to starting frame
				{
					setSingleImage(startingHorizontalCell, startingVerticalCell);
				}
				else if (string.IsNullOrEmpty(currentAnimation.nextAnimation) || currentAnimation.nextAnimation == "stop")		// Stop the animation and keep it's last frame
				{
					currentAnimation = null;
				}
				else if (currentAnimation.nextAnimation == currentAnimation.name || currentAnimation.nextAnimation == "loop")	// Loop the same animation
				{
					resetAnimation();
				} 
				else 																											// Start another animation
				{
					setAnimation(currentAnimation.nextAnimation);
				}			
			}
		}
	}
	
	// Sets a single image by indicating its cells
	public void setSingleImage(int horCell, int verCell){
		UVFunctions.SetUVByPixelSizes(new Vector2(horCell, verCell), new Vector2(cellHorizontalSize, cellVerticalSize), mirroredImage, mesh, GetComponent<MeshRenderer>().sharedMaterial.mainTexture);
		currentAnimation = null;
	}
	
	// Called during animation play to set current image index.
	private void setFramedImage() {
		UVFunctions.SetUVByPixelSizes(new Vector2((currentAnimation.startingHorizontalCell+currentFrame), currentAnimation.startingVerticalCell), new Vector2(cellHorizontalSize, cellVerticalSize), mirroredImage, mesh, GetComponent<MeshRenderer>().sharedMaterial.mainTexture);	
			//Debug.Log("" + gameObject.name + " set frame " + (currentFrame+1).ToString() + " in <" + currentAnimation.name + "> as current image");
	
	}
	
	// Starts timer and set current frame to 1, allowing the animation to start (or to loop) from it's first frame.
	private void resetAnimation() {
		animationTimer = currentAnimation.frameDuration;
		currentFrame = 0;
		UVFunctions.SetUVByPixelSizes(new Vector2((currentAnimation.startingHorizontalCell), currentAnimation.startingVerticalCell), new Vector2(cellHorizontalSize, cellVerticalSize), mirroredImage, mesh, GetComponent<MeshRenderer>().sharedMaterial.mainTexture);	
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
		Debug.LogWarning("Warning: " + gameObject.name + " (Instance: " + gameObject.GetInstanceID() + ") was not able to find <" + animationName + "> animation", gameObject);
	}
	
	// Calling setMirror with no arguments will invert mirrored state.
	public bool setMirror() {
		bool mirrorStatus;
		switch (mirroredImage) {
		case true:
			mirrorStatus = setMirror(false);
			return mirrorStatus;
		case false:
			mirrorStatus = setMirror(true);
			return mirrorStatus;	
		}
		//This part of code should never be executed
		Debug.LogWarning("SetMirror() for " + gameObject.name + " (Instance: " + gameObject.GetInstanceID() + ") returned a null value. Returning False.", gameObject);
		return false;
		
	}
	
	// Set mirrored state to true or false.
	public bool setMirror(bool trueOrFalse) {
		mirroredImage = trueOrFalse;
		setFramedImage();
		return mirroredImage;
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
	} // UVAnimation sub class end
	
} //class end
