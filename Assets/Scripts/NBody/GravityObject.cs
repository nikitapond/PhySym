using UnityEngine;
using UnityEditor;

public class GravityObject : MonoBehaviour
{
    public float Mass = 1;
    public float Radius = 1;

    public Vector3 Velocity;

    public void SetInit(GravityObjectInitial init)
    {
        Velocity = init.StartVel;
        transform.localPosition = init.StartPos;
        Mass = init.Mass;
        Radius = init.Radius;

        transform.localScale = Vector3.one * Radius;
    }


    public void SetTickAcceleration(Vector3 acc, float dt)
    {
        Vector3 pos = transform.position + Velocity * dt + 0.5f * acc * dt * dt;
        Velocity += acc * dt;
        transform.position = pos;
    }

}
public struct GravityObjectInitial
{
    public float Mass, Radius;
    public Vector3 StartVel, StartPos;
}