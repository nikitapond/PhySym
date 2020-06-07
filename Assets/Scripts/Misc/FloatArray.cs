using UnityEngine;
using UnityEditor;

public struct FloatArray
{

    public float[] Vals;

    public int Dimension { get { return Vals==null?-1:Vals.Length; } }

    public FloatArray(float[] vals)
    {
        Vals = vals;
    }

    public float this[int i] => Vals[i];

    public static implicit operator float[](FloatArray fa) => fa.Vals;
    public static explicit operator FloatArray(float[] vals) => new FloatArray(vals);


    public static bool operator ==(FloatArray f1, FloatArray f2)
    {
        //If both are unspecified, then they are equal
        if (f1.Dimension == -1 && f2.Dimension == -1)
            return true;
        //If they are not n=both null, but one of them is, then they cannot be equal
        if (f1.Dimension != f2.Dimension)
            return false;
        for(int i=0; i<f1.Dimension; i++)
        {
            if (f1.Vals[i] != f2.Vals[i])
                return false;
        }
        return true;      
            
    }
    public static bool operator !=(FloatArray f1, FloatArray f2)
    {
        return !(f1 == f2); 

    }
    public static FloatArray operator *(FloatArray f1, FloatArray f2)
    {
        if (f1.Dimension != f2.Dimension)
            throw new NumericalException("Float arrays must be of same dimension to multiply");

        float[] vals = new float[f1.Dimension];
        for(int i=0; i<f1.Dimension; i++)
        {
            vals[i] = f1.Vals[i] * f2.Vals[i];
        }
        return new FloatArray(vals);
    }
    public static FloatArray operator *(FloatArray f1, float scal)
    {     
        float[] vals = new float[f1.Dimension];
        for (int i = 0; i < f1.Dimension; i++)
        {
            vals[i] = f1.Vals[i] * scal;
        }
        return new FloatArray(vals);
    }

    public static FloatArray operator /(FloatArray f1, FloatArray f2)
    {
        if (f1.Dimension != f2.Dimension)
            throw new NumericalException("Float arrays must be of same dimension to multiply");

        float[] vals = new float[f1.Dimension];
        for (int i = 0; i < f1.Dimension; i++)
        {
            vals[i] = f1.Vals[i] / f2.Vals[i];
        }
        return new FloatArray(vals);
    }
    public static FloatArray operator /(FloatArray f1, float scal)
    {
        float[] vals = new float[f1.Dimension];
        for (int i = 0; i < f1.Dimension; i++)
        {
            vals[i] = f1.Vals[i] / scal;
        }
        return new FloatArray(vals);
    }

    public static FloatArray operator +(FloatArray f1, FloatArray f2)
    {
        if (f1.Dimension != f2.Dimension)
            throw new NumericalException("Float arrays must be of same dimension to multiply");

        float[] vals = new float[f1.Dimension];
        for (int i = 0; i < f1.Dimension; i++)
        {
            vals[i] = f1.Vals[i] + f2.Vals[i];
        }
        return new FloatArray(vals);
    }
    public static FloatArray operator -(FloatArray f1, FloatArray f2)
    {
        if (f1.Dimension != f2.Dimension)
            throw new NumericalException("Float arrays must be of same dimension to multiply");

        float[] vals = new float[f1.Dimension];
        for (int i = 0; i < f1.Dimension; i++)
        {
            vals[i] = f1.Vals[i] - f2.Vals[i];
        }
        return new FloatArray(vals);
    }

}