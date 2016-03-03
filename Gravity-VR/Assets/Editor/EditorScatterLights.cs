using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScatterLights))]
public class ScatterLightsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ScatterLights myScript = (ScatterLights)target;


        if (GUILayout.Button("Scatter Lights"))
        {
            myScript.CreateLights();
        }
    }
}