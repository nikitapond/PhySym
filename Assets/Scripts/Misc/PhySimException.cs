using UnityEngine;
using UnityEditor;

public class NumericalException : System.Exception
{
    public NumericalException(string log): base(log)
    {

    }
}
public class ArgumentException : System.Exception
{
    public ArgumentException(string log) : base(log)
    {

    }
}