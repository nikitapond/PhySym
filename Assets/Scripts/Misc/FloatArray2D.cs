using UnityEngine;
using UnityEditor;


public struct FloatArray2D
{
   
    public float[,] Vals;

    public int Dimension0 { get { return Vals == null ? -1 : Vals.GetLength(0); } }
    public int Dimension1 { get { return Vals == null ? -1 : Vals.GetLength(1); } }

    public FloatArray2D(float[,] vals)
    {
        Vals = vals;
    }
    public float this[int i, int j] { 
        get {
            if (i < 0)
            {
                i %= Dimension0;
                i += Dimension0;
            }
            else if (i >= Dimension0)
                i %= Dimension0;
            if (j < 0)
            {
                j %= Dimension1;
                j += Dimension1;
            }
            else if (j >= Dimension0)
                j %= Dimension0;
            return Vals[i, j]; 
        } 
        set {
            if (i < 0)
            {
                i %= Dimension0;
                i += Dimension0;
            }
            else if (i >= Dimension0)
                i %= Dimension0;
            if (j < 0)
            {
                j %= Dimension1;
                j += Dimension1;
            }
            else if (j >= Dimension0)
                j %= Dimension0;
            Vals[i, j] = value;
        } }

    public static FloatArray2D Roll(FloatArray2D f1, int roll, int axis)
    {

        if(!(axis == 0 || axis == 1))
        {
            throw new ArgumentException("Axis can only be 0 or 1");
        }
        float[,] rolled = new float[f1.Dimension0, f1.Dimension1];
        for(int x=0; x<f1.Dimension0; x++)
        {
            for(int y=0; y<f1.Dimension1; y++)
            {
                int tx = axis == 0 ? x : x + roll;
                int ty = axis == 1 ? y : y + roll;

                if (tx < 0)
                    tx += f1.Dimension0;
                else if (tx >= f1.Dimension0)
                    tx -= f1.Dimension0;

                if (ty < 0)
                    ty += f1.Dimension1;
                else if (ty >= f1.Dimension1)
                    ty -= f1.Dimension1;

                rolled[tx, ty] = f1.Vals[x, y];

            }
        }

        return new FloatArray2D(rolled);

    }




    public static implicit operator float[,](FloatArray2D fa) => fa.Vals;
    public static explicit operator FloatArray2D(float[,] vals) => new FloatArray2D(vals);

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Vec2i)) return false;
        FloatArray2D v = (FloatArray2D)obj;
        return v == this;
    }

    public static bool operator ==(FloatArray2D f1, FloatArray2D f2)
    {
        //If both are unspecified, then they are equal
        if (f1.Vals == null && f2.Vals == null)
            return true;
        //If any dimensions are not equal, then they cannot be equal
        if (f1.Dimension0 != f2.Dimension0 || f1.Dimension1 != f2.Dimension1)
            return false;
        for (int i = 0; i < f1.Dimension0; i++)
        {
            for (int j = 0; j < f1.Dimension1; j++)
            {
                if (f1.Vals[i, j] != f2.Vals[i, j]) return false;
            }
        }
        return true;

    }
    public static bool operator !=(FloatArray2D f1, FloatArray2D f2)
    {
        return !(f1 == f2);

    }
    public static FloatArray2D operator *(FloatArray2D f1, FloatArray2D f2)
    {
        if (f1.Dimension0 != f2.Dimension0 || f1.Dimension1 != f2.Dimension1)
            throw new NumericalException("Float arrays must be of same dimension to multiply");

        float[,] vals = new float[f1.Dimension0, f1.Dimension1];
        for (int i = 0; i < f1.Dimension0; i++)
        {
            for (int j = 0; j < f1.Dimension1; j++)
            {
                vals[i, j] = f1.Vals[i, j] * f2.Vals[i, j];
            }
        }
        return new FloatArray2D(vals);
    }
    public static FloatArray2D operator *(FloatArray2D f1, float scal)
    {
        float[,] vals = new float[f1.Dimension0, f1.Dimension1];
        for (int i = 0; i < f1.Dimension0; i++)
        {
            for (int j = 0; j < f1.Dimension1; j++)
            {
                vals[i, j] = f1.Vals[i, j] * scal;
            }
        }
        return new FloatArray2D(vals);
    }

    public static FloatArray2D operator /(FloatArray2D f1, FloatArray2D f2)
    {
        if (f1.Dimension0 != f2.Dimension0 || f1.Dimension1 != f2.Dimension1)
            throw new NumericalException("Float arrays must be of same dimension to multiply");

        float[,] vals = new float[f1.Dimension0, f1.Dimension1];
        for (int i = 0; i < f1.Dimension0; i++)
        {
            for (int j = 0; j < f1.Dimension1; j++)
            {
                vals[i, j] = f1.Vals[i, j] / f2.Vals[i, j];
            }
        }
        return new FloatArray2D(vals);
    }
    public static FloatArray2D operator /(FloatArray2D f1, float scal)
    {
        float[,] vals = new float[f1.Dimension0, f1.Dimension1];
        for (int i = 0; i < f1.Dimension0; i++)
        {
            for (int j = 0; j < f1.Dimension1; j++)
            {
                vals[i, j] = f1.Vals[i, j] / scal;
            }
        }
        return new FloatArray2D(vals);
    }

    public static FloatArray2D operator +(FloatArray2D f1, FloatArray2D f2)
    {
        if (f1.Dimension0 != f2.Dimension0 || f1.Dimension1 != f2.Dimension1)
            throw new NumericalException("Float arrays must be of same dimension to add");

        float[,] vals = new float[f1.Dimension0, f1.Dimension1];
        for(int i = 0; i < f1.Dimension0; i++)
        {
            for (int j = 0; j < f1.Dimension1; j++)
            {
                vals[i, j] = f1.Vals[i, j] + f2.Vals[i,j];
            }
        }
        return new FloatArray2D(vals);
    }

    public static FloatArray2D operator -(FloatArray2D f1, FloatArray2D f2)
    {
        if (f1.Dimension0 != f2.Dimension0 || f1.Dimension1 != f2.Dimension1)
            throw new NumericalException("Float arrays must be of same dimension to add");

        float[,] vals = new float[f1.Dimension0, f1.Dimension1];
        for (int i = 0; i < f1.Dimension0; i++)
        {
            for (int j = 0; j < f1.Dimension1; j++)
            {
                vals[i, j] = f1.Vals[i, j] - f2.Vals[i, j];
            }
        }
        return new FloatArray2D(vals);
    }

}