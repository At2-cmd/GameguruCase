using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridAreaEntity))]
public class GridGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridAreaEntity generator = (GridAreaEntity)target;

        if (GUILayout.Button("Generate Grid"))
        {
            generator.GenerateGrid();
        }

        if (GUILayout.Button("Clear Grid"))
        {
            generator.ClearGrid();
        }
    }
}