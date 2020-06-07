using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorFieldPlotter : MonoBehaviour
{

    public delegate Vector3 Function(float x, float y, float z);

    private GameObject ArrowPrefab;

    private Function FieldFunction;

    private FieldRange Range;
    private float Resolution;
    private Vec3i DataSize;

    private Vector3[,,] Data;

    private void Awake()
    {
        ArrowPrefab = Resources.Load<GameObject>("Models/Arrow");
        Resolution = -1;
    }

    private void Start()
    {
        if (FieldFunction == null)
            throw new ArgumentException("Cannot plot field without field function");
        if (Resolution < 0)
            throw new ArgumentException("Must set range and resolution of system");
        if (Data == null)
            CalculateFieldValues();

        for (int x = 0; x < DataSize.x; x++)
        {
            for (int y = 0; y < DataSize.y; y++)
            {
                for (int z = 0; z < DataSize.z; z++)
                {
                    GameObject arrow = Instantiate(ArrowPrefab);
                    arrow.transform.parent = transform;
                    arrow.transform.localPosition = new Vector3(x, y, z) * Resolution + Range.Minimum;
                    arrow.transform.rotation = Quaternion.LookRotation(Data[x, y, z]);
                    arrow.transform.localScale = Vector3.one * Data[x, y, z].magnitude;
                }
            }
        }
    }

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
        if (resolution < 0)
            throw new ArgumentException("Resolution must be positive");
        Range = range;
        Resolution = resolution;
        int xSize = Mathf.CeilToInt(range.Size.x / resolution);
        int ySize = Mathf.CeilToInt(range.Size.y / resolution);
        int zSize = Mathf.CeilToInt(range.Size.z / resolution);
        DataSize = new Vec3i(xSize, ySize, zSize);
    }
    /// <summary>
    /// Calculates the field at each point within <see cref="Range"/>
    /// </summary>
    public void CalculateFieldValues()
    {
        //We initiate our data array
        Data = new Vector3[DataSize.x, DataSize.y, DataSize.z];
        for(int x=0; x<DataSize.x; x++)
        {
            for(int y=0; y<DataSize.y; y++)
            {
                for(int z=0; z<DataSize.z; z++)
                {
                    float x_ = Range.Maximum.x + Resolution * x;
                    float y_ = Range.Maximum.y + Resolution * y;
                    float z_ = Range.Maximum.z + Resolution * z;
                    Vector3 val = FieldFunction(x_, y_, z_);
                    Data[x, y, z] = val;
                }
            }
        }
    }

}
public struct FieldRange
{
    public Vector3 Minimum;
    public Vector3 Maximum;

    public Vector3 Size;


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
        Size = Maximum - Minimum;
    }
}