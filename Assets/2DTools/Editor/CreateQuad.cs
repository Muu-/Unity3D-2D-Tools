/// This class will create an 1x1 Quad, with centered pivot point.
/// How to use:
/// 	From Unity Editor
/// 		Game Object -> Create Other -> Create Quad
/// 		or use it's shortcut: CTRL+Q
/// 
/// Author: Andrea Giorgio "Muu?" Cerioli
/// Website: www.lanoiadimuu.it
/// License: 100% Freedom. You can edit it, use it in both free and commercial project

using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateQuad : ScriptableWizard {
	[MenuItem ("GameObject/Create Other/Create Quad %q")]
	static void CreateWizard() {
		
		//If there are visible scene views, get the last used camera.
		Camera cam = null;
		if (SceneView.sceneViews.Count > 0)
		{
			cam = SceneView.lastActiveSceneView.camera;
		}
		
		//Instantiate a new Quad and name it.
		GameObject quad = new GameObject();
		quad.name = "Quad";
		
		//Add a MeshRenderer and a MeshFilter.
		quad.AddComponent<MeshRenderer>();
		MeshFilter mf = (MeshFilter) quad.AddComponent<MeshFilter>();
		
		//Search Meshes folder if there's a quad already.
		Mesh mesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/2DTools/" + "Quad.asset", typeof(Mesh));
		
		//If there's no Quad, create it
		if (mesh == null)
		{
			mesh = MakeQuad("Quad");
			//Save the mesh for future use
            AssetDatabase.CreateAsset(mesh, "Assets/2DTools/" + "Quad.asset");
            AssetDatabase.SaveAssets();
        } //End IF (mesh creation)
		
		//Add Quad mesh on MeshFilter
		mf.sharedMesh = mesh;
		
		//We ask the editor to recalculate mesh bounds, to ensure their values are correct
		mesh.RecalculateBounds();
		
		//If we previously found a camera, move the quad to the camera center.
		if (cam != null)
			quad.transform.position = cam.transform.position + (cam.transform.forward * 5);
		
		//Select it in the editor view
		Selection.activeObject = quad;
			
	}
	
	public static Mesh MakeQuad (string name) {
		//Generate a new Mesh and name it
		Mesh mesh;
		mesh = new Mesh();
        mesh.name = name;
		
		//Create four vertices for our quad and apply them to the mesh
		Vector3[] vertices = new Vector3[]{
			new Vector3(0.5f,0.5f,0),	
			new Vector3(0.5f,-0.5f,0),
			new Vector3(-0.5f,0.5f,0),
			new Vector3(-0.5f,-0.5f,0),
		};
		
		mesh.vertices = vertices;
		
		//Generate uvs and apply it
		Vector2[] uvs = new Vector2[]{
			new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(0, 0),	
		};
		
        mesh.uv = uvs;
		
		//Generate triangles
		int[] triangles = new int[]
        {
            0, 1, 2,
            2, 1, 3,
        };
		
        mesh.triangles = triangles;
		
		//Since new generated mesh have no normals, recalculate it
        mesh.RecalculateNormals();

		return mesh;
	}
	
} //Class End