using UnityEngine;
using UnityEditor;

public struct ComplexNumber
{

    public float Real;
    public float Imag;

    public ComplexNumber(float real, float imag)
    {
        Real = real;
        Imag = imag;
    }

    public override string ToString()
    {
        return string.Format("{0} {1}i", Real, Imag);
    }

    public static ComplexNumber operator +(ComplexNumber c1, ComplexNumber c2)
    {
        return new ComplexNumber(c1.Real + c2.Real, c1.Imag + c2.Imag);
    }
    public static ComplexNumber operator -(ComplexNumber c1, ComplexNumber c2)
    {
        return new ComplexNumber(c1.Real - c2.Real, c1.Imag - c2.Imag);
    }

    public static ComplexNumber operator *(ComplexNumber c1, ComplexNumber c2)
    {
        //When multiplying 2 complex numbers, we have the form:
        //(a+bi)*(c+di) = ac + adi + bci - bd
        //              = (ac - bd) + (ad+bc)i
        return new ComplexNumber(c1.Real * c2.Real - c1.Imag * c2.Imag, c1.Real * c2.Imag + c1.Imag * c2.Real);
    }
}

public struct ComplexExp
{
    public ComplexNumber Exp;
    public float R; //The coefficient in front of the exponential

    public ComplexExp(float r, ComplexNumber exp)
    {
        R = r;
        Exp = exp;
    }
}
public static class ComplexNumberHelper
{
    public static ComplexNumber Comjugate(this ComplexNumber num)
    {
        return new ComplexNumber(num.Real, -num.Imag);
    }
    public static ComplexExp Comjugate(this ComplexExp exp)
    {
        return new ComplexExp(exp.R, exp.Exp.Comjugate());
    }

}