using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(UVSprite))]
public class UVSpriteInspector : Editor {
	private SerializedObject sprite;
	private SerializedProperty	cellHorizontalSize,
								cellVerticalSize,
								startingHorizontalCell,
								startingVerticalCell,
								startMirrored,
								animations;

	void OnEnable () {
		sprite = new SerializedObject(target);
		cellHorizontalSize = sprite.FindProperty("cellHorizontalSize");
		cellVerticalSize = sprite.FindProperty("cellVerticalSize");
		startingHorizontalCell = sprite.FindProperty("startingHorizontalCell");
		startingVerticalCell = sprite.FindProperty("startingVerticalCell");
		startMirrored = sprite.FindProperty("startMirrored");
		animations = sprite.FindProperty("animations");
	}

	
	
	// Draw on inspector GUI	
	public override void OnInspectorGUI() {
		sprite.Update();
		
		// Set 2D cell dimensions
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Set UV mappings for 2D sprites:");
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(cellHorizontalSize, new GUIContent("Cell hor. size"), true);
		EditorGUILayout.PropertyField(cellVerticalSize,new GUIContent("Cell ver. size"), true);
		EditorGUILayout.EndHorizontal();
		// Set starting spritesheet cell and if should be mirrored or not
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Starting Cell:");
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(startingHorizontalCell, new GUIContent("Staring hor. cell"), true);
		EditorGUILayout.PropertyField(startingVerticalCell, new GUIContent("Starting ver. cell"), true);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.PropertyField(startMirrored, new GUIContent("Mirrored"), true);
		// Set animations part
		// Draw an "add" button, used to increase array size
		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Animations:");
		if (GUILayout.Button("Add", EditorStyles.miniButton, GUILayout.MaxWidth(100)))
		{
			// If there's no array, increase its size to create it
			if (animations == null || animations.arraySize == 0) {
				animations.arraySize += 1; 
			} else {
				animations.InsertArrayElementAtIndex(animations.arraySize-1); 
			}
		}
		EditorGUILayout.EndHorizontal();
		// Loop for each element in the array
		for (int i = 0; i < animations.arraySize; i++)
		{
			// Get the element at index, draw a label + its index and a button to delete it
			SerializedProperty anim = animations.GetArrayElementAtIndex(i);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Animation " + i.ToString() + ": ");
			if (GUILayout.Button("Delete", EditorStyles.miniButton, GUILayout.MaxWidth(100)))	{
				animations.DeleteArrayElementAtIndex(i);
				// If it was the last element, break the script
				if (animations.arraySize == 0) {
					break;	
				}
			}
			EditorGUILayout.EndHorizontal();
			// Edit in this order: Animation Name, Next Animation, Starting Cell (both hor and ver), Number of frames, Frame duration.
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Name ");
			EditorGUILayout.PropertyField(anim.FindPropertyRelative("name"), GUIContent.none);
			EditorGUILayout.LabelField("Next Anim ");
			EditorGUILayout.PropertyField(anim.FindPropertyRelative("nextAnimation"), GUIContent.none);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Starting hor. cell ");
			EditorGUILayout.PropertyField(anim.FindPropertyRelative("startingHorizontalCell"), GUIContent.none);
			EditorGUILayout.LabelField("Starting ver. cell ");
			EditorGUILayout.PropertyField(anim.FindPropertyRelative("srartingVerticalCell"), GUIContent.none);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Lenght in frames");
			EditorGUILayout.PropertyField(anim.FindPropertyRelative("frames"), GUIContent.none);
			EditorGUILayout.LabelField("Frame duration");
			EditorGUILayout.PropertyField(anim.FindPropertyRelative("frameDuration"), GUIContent.none);
			EditorGUILayout.EndHorizontal();
		}
		
		//Lastly, apply stuff
		sprite.ApplyModifiedProperties();
	}
}
