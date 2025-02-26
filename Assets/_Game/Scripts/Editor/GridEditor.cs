using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridGenerator))]
public class GridGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridGenerator generator = (GridGenerator)target;

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