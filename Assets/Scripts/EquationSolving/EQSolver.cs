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

}