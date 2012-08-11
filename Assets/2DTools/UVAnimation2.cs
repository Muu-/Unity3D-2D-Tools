/// Takes care of simple UV animation. Designed to be easy and fast to use.
/// How to use:
/// Call SetAnimation() from another script with the following params:
/// 	Vector2	start		lower left coordinates (in UV sizes) of the first frame
/// 	Vector2 cellSize	UV size of each cell
/// 	float 	emptyspace	UV size of empty space
/// 	int 	aLenght		number of frames used in the animation, starting with 0 = 1 frame, 1 = 2 frames etc. 
/// 	bool 	loop		should the animation be looping?
/// 	bool	mirrorHor	mirror horizontally? eg. a player looking left or right
/// 	float	speed		How long should a frame last
/// 	string	name		A custom name, you can use any string (see GetAnimationName())
/// 
/// Or call SetPxAnimation() with the following params:
/// 	Vector2 pxStart		Pixel coordinate of the lower left pixel of the first frame.
/// 	other params		look in the above list
/// 	(Remember to set cellPXSize and cellPxEmptySpace in onbject inspector to use this function)
/// 
/// Call GetAnimationName() from another script to ask the script which animation is it playing.
/// Il returns a string containing the animation name you set with SetAnimation()
/// Useful for checking if an animation is already playing.
/// 
/// Notes on calculating UV sizes from Pixel sizes. The calc is really simple:
/// For example if you have a 128x128 px sprite, your cell is 32x32 px and the empty space is 4 px
/// Cell size is 32/128 	= 	0.25
/// Empty space is 4/128 	=	0.03125
/// 
/// Final notes:
/// - Unity texture compression prefers images that have power of 2 dimensions. (eg 32x32, 64x64, 128x128 ecc)
/// - UV textels work in 4x4 groups, frames and empty spaces should have multiple of 4 dimensions
/// They are not limited to squared (32x32, 64x64 ecc), you can use any dimension divisible by 4(24x32, 64x128 ecc)
/// 
/// Author: Andrea Giorgio "Muu?" Cerioli
/// Website: www.lanoiadimuu.it
/// License: 100% Freedom. You can edit it, use it in both free and commercial project

using UnityEngine;
using System.Collections;

public class UVAnimation2 : MonoBehaviour {
	// VAR				TYPE		USE
	// cellPxSize		Vector2		Store pixel size of the cell
	// cellPxEmptySpace	int			Store empty pixel size between each cell
	public Vector2 cellPXSize;
	public float cellPxEmptySpace;
	
	// VAR				TYPE		USE
	// currAnim			Vector2[]	Storing lower left position of each frame
	// uvSize			Vector2		UV size of each cell
	// currAnimStep		int			Tells the script wich frame to load
	// timer			float		Used to determine when next frame will be drawed
	// framespeed		float		Tells the script how much time each frame should last
	// mirror			bool		Tells the script to flip horizontally the frame
	// looping			bool		Loop the script if it reaches its end
	// mesh				Mesh		Used for easilly reaching mesh.uv
	
	private string currAninName;
	private Vector2[] currAnim = new Vector2[0];
	private Vector2 uvSize;
	private int currAnimStep;
	private float timer;
	private float framespeed;
	private bool looping;
	private bool mirror;
	private Mesh mesh;
	
	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
	}
	
	void Update () {
		//If we have more frames, play animation
		if (currAnimStep < (currAnim.Length-1))
			PlayAnimation();
		
	}
	
	
	private void PlayAnimation() {
		//Increase time
		timer += Time.deltaTime;
		
		//If timer > frame, set newUV and reset the timer
		if (timer >= framespeed)
		{
			currAnimStep++;
			SetUVs(currAnim[currAnimStep]);
			timer = 0;
			
			//Check if the animation needs looping, and reset it
			if ((looping) && (currAnimStep == (currAnim.Length-1)))
			{
				currAnimStep = -1;
			}
		}
	} //end PlayAnimation
	
	//Set the object's sprite
	//See script header for usage
	public void SetAnimation (Vector2 start, Vector2 cellSize, float emptyspace, int aLenght, bool loop, bool mirrorHor, float speed, string name) {
		aLenght++;
		Vector2[] frames = new Vector2[aLenght];
		frames[0] = start; //set start frame at position 0
		uvSize = cellSize;
		
		//Loop aLenght to set other frames
		for (int i = 0; i < aLenght; ++i)
		{
			float offset = i * cellSize.x + i * emptyspace;
			frames[i].x = start.x + offset;
			frames[i].y = start.y;
		}
		
		currAninName = name;
		currAnim = frames;
		mirror = mirrorHor;
		looping = loop;
		currAnimStep = 0;
		SetUVs(currAnim[currAnimStep]);
		timer = 0;
		framespeed = speed;
	} //end SetAnimation
	
	// Set UVs is called when the script needs to update the object's UV mapping
	private void SetUVs(Vector2 start) {
		// Create a new vector2 array, use to store new UVs
		Vector2[] nUVs = new Vector2[4];
		
		if (!mirror) {
			nUVs[0] = start + uvSize;//UR
			nUVs[1] = start + Vector2.right * uvSize.x;//BR
			nUVs[2] = start + Vector2.up * uvSize.y;//UL
			nUVs[3] = start;//BL
		} else {
			//Mirrored
			nUVs[0] = start + Vector2.up * uvSize.y;//UL
			nUVs[1] = start;//BL
			nUVs[2] = start + uvSize;//UR
			nUVs[3] = start + Vector2.right * uvSize.x;//BR	
			
		}
		
		// Stores the new UV mapping
		mesh.uv = nUVs;
	} //end SetUvs
	
	//Returns animation name
	public string GetAnimationName() {
		if (currAninName == null) {
			return System.String.Empty;
		}
		return currAninName;
	}
	
	//Sets Animation by pixels
	public void SetPxAnimation(Vector2 pxStart, int aLenght, bool loop, bool mirrorHor, float speed, string name) {
		Texture texture = GetComponent<MeshRenderer>().material.mainTexture;
		
		Vector2 tmpStart = new Vector2((pxStart.x / texture.width), ((texture.width-pxStart.y-1)/texture.width));
		Vector2 tmpCellSize = new Vector2((cellPXSize.x / texture.width),(cellPXSize.y / texture.height));
		SetAnimation(tmpStart, tmpCellSize, cellPxEmptySpace/texture.width, aLenght, loop, mirrorHor, speed, name);
	}
}
