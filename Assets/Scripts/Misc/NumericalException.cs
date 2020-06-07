using UnityEngine;
using UnityEditor;

public class NumericalException : System.Exception
{
    public NumericalException(string log): base(log)
    {

    }
}