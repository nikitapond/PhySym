using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (GravityObject), true)]
[CanEditMultipleObjects]
public class GravityObjectEditor : Editor
{
    public bool showDebugInfo;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        //EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
        showDebugInfo = EditorGUILayout.Foldout(showDebugInfo, "Debug info");
        if (showDebugInfo)
        {
            EditorGUILayout.LabelField("test");
            /*
            string[] gravityInfo = GetGravityInfo(gravityObject.transform.position, gravityObject as CelestialBody);
            for (int i = 0; i < gravityInfo.Length; i++)
            {
                EditorGUILayout.LabelField(gravityInfo[i]);
            }*/
        }
        GravityObject gObj = target as GravityObject;
        gObj.Mass = EditorGUILayout.FloatField("Mass", gObj.Mass);

        gObj.Velocity = EditorGUILayout.Vector3Field("Start Velocity", gObj.Velocity);
    }
}