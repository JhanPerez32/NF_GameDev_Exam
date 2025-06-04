using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DuplicateMoveEditor : EditorWindow
{
    private Vector3 moveOffset = new Vector3(0f, 0f, 5f);
    private List<GameObject> prefabList = new List<GameObject>();

    [MenuItem("Tools/Duplicate Prefab and Move %#d")] // Ctrl+Shift+D
    public static void ShowWindow()
    {
        GetWindow<DuplicateMoveEditor>("Duplicate & Move");
    }

    void OnGUI()
    {
        GUILayout.Label("Ground Generator Tool", EditorStyles.boldLabel);

        // Move offset field
        moveOffset = EditorGUILayout.Vector3Field("Move Offset", moveOffset);

        GUILayout.Space(10);
        GUILayout.Label("Prefab List", EditorStyles.boldLabel);

        // Draw prefab list
        for (int i = 0; i < prefabList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            prefabList[i] = (GameObject)EditorGUILayout.ObjectField(prefabList[i], typeof(GameObject), false);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                prefabList.RemoveAt(i);
                i--;
            }
            EditorGUILayout.EndHorizontal();
        }

        // Add new slot
        if (GUILayout.Button("+ Add Prefab"))
        {
            prefabList.Add(null);
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Duplicate Random Prefab"))
        {
            DuplicateRandomPrefab();
        }
    }

    private void DuplicateRandomPrefab()
    {
        if (prefabList.Count == 0)
        {
            Debug.LogWarning("No prefabs assigned.");
            return;
        }

        // Filter out nulls
        List<GameObject> validPrefabs = prefabList.FindAll(prefab => prefab != null);
        if (validPrefabs.Count == 0)
        {
            Debug.LogWarning("All prefab slots are empty.");
            return;
        }

        GameObject selectedPrefab = validPrefabs[Random.Range(0, validPrefabs.Count)];
        GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(selectedPrefab);

        newObject.transform.position = GetLastPlacedPosition() + moveOffset;
        newObject.name = GetUniqueName(selectedPrefab.name);

        Undo.RegisterCreatedObjectUndo(newObject, "Duplicate Ground Piece");
        Selection.activeGameObject = newObject;
    }

    private Vector3 GetLastPlacedPosition()
    {
        return Selection.activeGameObject != null
            ? Selection.activeGameObject.transform.position
            : Vector3.zero;
    }

    private static string GetUniqueName(string baseName)
    {
        string cleanBaseName = System.Text.RegularExpressions.Regex.Replace(baseName, @"\(\d+\)$", "");
        int count = 1;
        string newName = $"{cleanBaseName}({count})";

        while (GameObject.Find(newName) != null)
        {
            count++;
            newName = $"{cleanBaseName}({count})";
        }
        return newName;
    }
}
