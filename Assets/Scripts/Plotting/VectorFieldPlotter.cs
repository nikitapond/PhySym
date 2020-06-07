using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorFieldPlotter : MonoBehaviour
{

    public delegate Vector3 Function(float x, float y, float z);

    private Function FieldFunction;

    private FieldRange Range;
    private Vector3[,,] Data;
    private 

    public void SetFunction(Function func)
    {
        FieldFunction = func;
    }
    /// <summary>
    /// Sets the total range over which the field should be shown.
    /// </summary>
    /// <param name="range">The minimum and maximum points of the cuboid of volume we wish to show the field within</param>
    /// <param name="resolution">The number of points per unit to display</param>
    public void SetRangeAndResolution(FieldRange range, float resolution)
    {
        Range = range;
    }
    /// <summary>
    /// Calculates the field at each point within <see cref="Range"/>
    /// </summary>
    public void CalculateFieldValues()
    {

    }

}
public struct FieldRange
{
    public Vector3 Minimum;
    public Vector3 Maximum;

    public FieldRange(Vector3 min, Vector3 max)
    {
        //We ensure that the minimum an maximum points are specified correctly
        float minX = Mathf.Min(min.x, max.x);
        float minY = Mathf.Min(min.y, max.y);
        float minZ = Mathf.Min(min.z, max.z);

        float maxX = Mathf.Max(min.x, max.x);
        float maxY = Mathf.Max(min.y, max.y);
        float maxZ = Mathf.Max(min.z, max.z);

        Minimum = new Vector3(minX, minY, minZ);
        Maximum = new Vector3(maxX, maxY, maxZ);
    }
}