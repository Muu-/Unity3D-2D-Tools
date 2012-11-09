/// Can be called from UVSprite and/or other scripts to set object's texture data.

using UnityEngine;
using System.Collections;

public static class UVFunctions {

	// SetUV sets a single image frame
	public static void SetUV(Vector2 start, Vector2 uvCellSize, bool mirrored, Mesh mesh) {
		// Create a new vector2 array, use to store new UVs
		Vector2[] newUV = new Vector2[4];
		
		if (!mirrored) {
			newUV[0] = start + uvCellSize;//UR
			newUV[1] = start + Vector2.right * uvCellSize.x;//BR
			newUV[2] = start + Vector2.up * uvCellSize.y;//UL
			newUV[3] = start;//BL
		} else {
			//Mirrored
			newUV[0] = start + Vector2.up * uvCellSize.y;//UL
			newUV[1] = start;//BL
			newUV[2] = start + uvCellSize;//UR
			newUV[3] = start + Vector2.right * uvCellSize.x;//BR	
		}
		
		// Stores the new UV mapping
		mesh.uv = newUV;
	} //end SetUvs
	
	// SetUVByPixelSizes automatically converts values from Pixel size to UV size and calls SetUV.
	public static void SetUVByPixelSizes(Vector2 startingCell, Vector2 cellPXSize, bool mirror, Mesh mesh, Texture texture) {
		
		// The math function automatically finds the starting pixel
		// For horizontal one: multiply cell n° by its size.
		// For vertical one: reverse cell position (because 0 UV is the lowest pixel) by
		// 		multiplying ((starting cell + 1) * cell size) and the subtract this value from texture height
		Vector2 tmpStart = new Vector2(((startingCell.x * cellPXSize.x) / texture.width), ((texture.height-((startingCell.y+1)*cellPXSize.y))/texture.height));
		Vector2 tmpCellSize = new Vector2((cellPXSize.x / texture.width),(cellPXSize.y / texture.height));
		SetUV(tmpStart, tmpCellSize, mirror, mesh);
	}
}
