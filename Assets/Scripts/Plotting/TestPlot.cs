using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlot : MonoBehaviour
{

    private VectorFieldPlotter Plotter;

    struct Charge
    {
        public Vector3 Position;
        public float Magnitude;
        public Charge(Vector3 pos, float mag)
        {
            Position = pos;
            Magnitude = mag;
        }
    }

    private Charge[] Charges;
    private FieldRange Range;
    private void Awake()
    {
        Plotter = GetComponent<VectorFieldPlotter>();

        Charges = new Charge[] { 
            new Charge(new Vector3(5, 0, 0), 1),
            new Charge(new Vector3(0, 0, 0), 1),
            new Charge(new Vector3(5, 0, 5), 1),
            new Charge(new Vector3(0, 0, 5), 1),
        };

        Range = new FieldRange(new Vector3(-1, -1, -1), new Vector3(6, 1, 6));

    }

    void Start()
    {
        Plotter.SetFunction(FieldFunction);
        Plotter.SetRangeAndResolution(Range, 0.5f);
    }
    

    public Vector3 FieldFunction(float x, float y, float z)
    {
        Vector3 pos = new Vector3(x, y, z);
        Vector3 value = Vector3.zero;

        foreach(Charge c in Charges)
        {
            Vector3 disp = c.Position - pos;
            value += disp * c.Magnitude / disp.sqrMagnitude;

        }

        return value;


    }


}
