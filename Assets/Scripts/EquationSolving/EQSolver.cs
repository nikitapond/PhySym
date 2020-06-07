using UnityEngine;
using UnityEditor;

public class EQSolver
{

    public delegate float Function1D(float x);

    public delegate float[] FunctionND(float[] args);


    public float[] RK4(Function1D func, float initial, float dx, int N)
    {
        float[] sol = new float[N];
        sol[0] = initial;
        for (int i=0; i<N-1; i++)
        {
            float k1 = dx * func(sol[i]);
            float k2 = dx * func(sol[i] + 0.5f * k1);
            float k3 = dx * func(sol[i] + 0.5f * k2);
            float k4 = dx * func(sol[i] +        k3);
            sol[i + 1] = sol[i] + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
        }

        return sol;        
    }
    public float[][] RK4(FunctionND func, float[] initial, float d, int N)
    {
        int dim = initial.Length;

        float[][] sol = new float[N][];
        sol[0] = initial;

        for (int i=0; i<N-1; i++)
        {
            sol[i + 1] = new float[initial.Length];

            for(int j=0; j<dim; j++)
            {
                float[] k1 = func(sol[i]).Mult(d);
                float[] k2 = func(sol[i].Add(k1.Mult(0.5f))).Mult(d);
                float[] k3 = func(sol[i].Add(k2.Mult(0.5f))).Mult(d);
                float[] k4 = func(sol[i].Add(k3)).Mult(d);
                sol[i + 1] = sol[i] + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
            }
        }

    }
}
public static class FloatArrayHelper{
    public static float[] Mult(this float[] a, float b)
    {
        float[] result = new float[a.Length];
        for (int i = 0; i < a.Length; i++)
            result[i] = a[i] * b;
        return result;
    }
    public static string ToString(this float[] a)
    {
        string res = "[";
        for (int i = 0; i < a.Length - 1; i++)
            res += a[i] + ",";
        res += a[a.Length - 1] + "]";
        return res;
    }
    public static float[] SumArrays(float[][] arrays)
    {
        float[] sol = arrays.GetLength(0);

        for(int i=0; i<sol.Length; i++)
        {
            for(int )
        }
    }
    public static float[] Add(this float[] a, float[] b)
    {
        if(b.Length != a.Length)
        {
            throw new NumericalException(string.Format("Arrays must be of same length : \n\tlen(a)={0}\n\tlen(b)={1}", a.Length, b.Length));
        }

        float[] result = new float[a.Length];
        for (int i = 0; i < a.Length; i++)
            result[i] = a[i] + b[i];
        return result;
    }
}