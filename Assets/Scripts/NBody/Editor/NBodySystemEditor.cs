using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(NBodySystem), true)]
[CanEditMultipleObjects]
public class NBodySystemEditor : Editor
{

    private Vector3 StartPosition = Vector3.zero;
    private Vector3 StartVelocity = Vector3.zero;
    private float Mass = 1;
    private float Radius = 1;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.Space();
        //EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

        StartPosition = EditorGUILayout.Vector3Field("Start Position", StartPosition);
        StartVelocity = EditorGUILayout.Vector3Field("Start Velocity", StartVelocity);

        Mass = EditorGUILayout.FloatField("Mass", Mass);
        Radius = EditorGUILayout.FloatField("Radius", Radius);


        if (GUILayout.Button("Create new Body"))
        {
            GravityObjectInitial init;
            init.Mass = Mass;
            init.Radius = Radius;
            init.StartVel = StartVelocity;
            init.StartPos = StartPosition;
            CreateBody((target as NBodySystem), init);
        }
    }

    private void CreateBody(NBodySystem system, GravityObjectInitial init)
    {

        GameObject obj = Instantiate(system.GravityObjectPrefab);
        obj.transform.parent = system.gameObject.transform;
        obj.transform.localPosition = init.StartPos;
        GravityObject gObj = obj.GetComponent<GravityObject>();
        gObj.SetInit(init);

    }
}