using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteImporter))]
 public class SpriteImporterEditor : Editor {

	public override void OnInspectorGUI() {

		SpriteImporter myScript = (SpriteImporter)target;
		if (GUILayout.Button("Generate Cube Sprite")) {
			myScript.GenerateCubeSprite();
		}


		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Dimensions:", new GUILayoutOption[] { GUILayout.Width(80) });
		myScript.circleRadius = EditorGUILayout.IntField(myScript.circleRadius, new GUILayoutOption[] { });
		if (GUILayout.Button("Create Circle")) {
			myScript.GenerateCircle(myScript.circleRadius);
		}
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Dimensions:", new GUILayoutOption[] { GUILayout.Width(80) });
		myScript.squareDims = EditorGUILayout.Vector2IntField("", myScript.squareDims, new GUILayoutOption[] { });
		if (GUILayout.Button("Create Square")) {
			myScript.GenerateSquare(myScript.squareDims.x, myScript.squareDims.y);
		}
		EditorGUILayout.EndHorizontal();

		base.OnInspectorGUI();
	}
}
