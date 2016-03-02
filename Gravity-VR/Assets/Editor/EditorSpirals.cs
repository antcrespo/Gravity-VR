using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spiral))]
public class SpiralBuilderEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Spiral myScript = (Spiral)target;


        if (GUILayout.Button("Build Spiral"))
        {
            myScript.BuildSpiral();
        }
    }
}