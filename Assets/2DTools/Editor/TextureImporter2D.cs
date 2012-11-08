/// Used to import textures with pre-selected settings
/// Feel free to delete this file if you don't need it.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class TextureImporter2D : AssetPostprocessor {
	
	//Override texture importing
	void OnPostprocessTexture(Texture2D texture)
	{		
	    TextureImporter importer = (TextureImporter) assetImporter;

		Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture2D));
	    if (asset)
	    {
	        EditorUtility.SetDirty(asset);
	    }
		else
		{
			importer.anisoLevel = 0;
	    	importer.filterMode = FilterMode.Point;
			importer.wrapMode = TextureWrapMode.Clamp;
	    	importer.textureType = TextureImporterType.Advanced;
			importer.mipmapEnabled = false;
			importer.maxTextureSize = TextureSetMaxSize(texture.width, texture.height);
			importer.textureFormat = TextureImporterFormat.RGBA32;
			texture.anisoLevel = 0;
	    	texture.filterMode = FilterMode.Point;
			texture.wrapMode = TextureWrapMode.Clamp;
		}
		
	}
	
	//Used to round maxTextureSize to the next power of two
 	private int TextureSetMaxSize(int width, int height){
		int tmpInt = width;
		if (height > width) { tmpInt = height; }
		
		tmpInt--;
		tmpInt |= (tmpInt >> 1);
		tmpInt |= (tmpInt >> 2);
		tmpInt |= (tmpInt >> 4);
		tmpInt |= (tmpInt >> 8);
		tmpInt |= (tmpInt >> 16);
		tmpInt |= (tmpInt >> 32);
		tmpInt |= (tmpInt >> 64);
		tmpInt |= (tmpInt >> 128);
		tmpInt |= (tmpInt >> 256);
		tmpInt |= (tmpInt >> 512);
		tmpInt |= (tmpInt >> 1024);
		tmpInt |= (tmpInt >> 2048);
		tmpInt |= (tmpInt >> 4096);
		tmpInt++;
		
		return tmpInt;
	}
}