using UnityEngine;
using UnityEditor;

public class GenericEditorMethods :	EditorWindow
{
	[MenuItem("SelectedObject/Turn Off Shadows of Children")]
	private static void TurnOffShadowsOfChildren()
	{
		GameObject[] selectedObjects = Selection.gameObjects;
		int numMeshRenderers = 0;

		foreach (GameObject obj in selectedObjects)
		{
			MeshRenderer[] meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
			numMeshRenderers += meshRenderers.Length;

			foreach (MeshRenderer meshRenderer in meshRenderers)
			{
				meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			}
		}

		Debug.Log($"Turned off shadows for {numMeshRenderers} objects");
	}

	[MenuItem("SelectedObject/Turn On Shadows of Children")]
	private static void TurnOnShadowsOfChildren()
	{
		GameObject[] selectedObjects = Selection.gameObjects;
		int numMeshRenderers = 0;

		foreach (GameObject obj in selectedObjects)
		{
			MeshRenderer[] meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
			numMeshRenderers += meshRenderers.Length;

			foreach (MeshRenderer meshRenderer in meshRenderers)
			{
				meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			}
		}

		Debug.Log($"Turned on shadows for {numMeshRenderers} objects");
	}
}
