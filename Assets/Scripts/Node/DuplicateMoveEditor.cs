using UnityEditor;
using UnityEngine;

public class DuplicateMoveEditor : EditorWindow
{
    private static Vector3 moveOffset = new Vector3(5f, 0f, 0f); // Adjust as needed

    [MenuItem("Tools/Duplicate Prefab and Move %#d")] // Ctrl+Shift+D
    static void DuplicateAndMove()
    {
        if (Selection.activeGameObject == null)
        {
            Debug.LogWarning("No GameObject selected to duplicate.");
            return;
        }

        GameObject selectedObject = Selection.activeGameObject;

        // Check if it's a prefab instance
        if (PrefabUtility.IsPartOfPrefabInstance(selectedObject))
        {
            GameObject duplicatedObject = (GameObject)PrefabUtility.InstantiatePrefab(PrefabUtility.GetCorrespondingObjectFromSource(selectedObject));
            duplicatedObject.transform.position = selectedObject.transform.position + moveOffset;

            // Set unique name without nested parentheses
            duplicatedObject.name = GetUniqueName(selectedObject.name);

            Undo.RegisterCreatedObjectUndo(duplicatedObject, "Duplicate and Move Prefab");
            Selection.activeGameObject = duplicatedObject;
        }
        else
        {
            Debug.LogWarning("Selected object is not a turretModel instance.");
        }
    }

    static string GetUniqueName(string baseName)
    {
        // Remove any existing number suffix like "(1)"
        string cleanBaseName = System.Text.RegularExpressions.Regex.Replace(baseName, @"\(\d+\)$", "");

        int count = 1;
        string newName = $"{cleanBaseName}({count})";

        // Ensure no duplicates in the scene
        while (GameObject.Find(newName) != null)
        {
            count++;
            newName = $"{cleanBaseName}({count})";
        }
        return newName;
    }
}
