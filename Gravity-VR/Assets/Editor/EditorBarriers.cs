using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BarrierEditor))]
public class BarrierBuilderEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BarrierEditor myScript = (BarrierEditor)target;

        
        if (GUILayout.Button("Build Object"))
        {
            myScript.BuildWall();
        }
    }
}

/*public class MyWindow : EditorWindow
{
    int segments = 4;
    float polarAngle;
    float elevationAngle;
    float radius = 20f;
    float height = 3f;
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/My Window")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MyWindow));
    }

    void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        //myString = EditorGUILayout.TextField("Text Field", myString);

        //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        //myBool = EditorGUILayout.Toggle("Toggle", myBool);
        polarAngle = EditorGUILayout.Slider("Polar Angle", polarAngle, -180, 180);
        elevationAngle = EditorGUILayout.Slider("Elevation Angle", elevationAngle, -90, 90);
        radius = EditorGUILayout.FloatField("Radius", radius);
        height = EditorGUILayout.FloatField("Height", height);
        segments = EditorGUILayout.IntField("Segments", segments);
        //EditorGUILayout.EndToggleGroup();
    }

    
}*/