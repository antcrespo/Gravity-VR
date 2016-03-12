using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircleVolumetric))]
public class CircleVolumetricBuilderEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CircleVolumetric myScript = (CircleVolumetric)target;


        if (GUILayout.Button("Build Circle"))
        {
            myScript.BuildCircle();
        } else if (GUILayout.Button("Build X"))
        {
            myScript.BuildX();
        }
    }
}