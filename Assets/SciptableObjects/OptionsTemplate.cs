using System;
using UnityEngine;

public static class OptionsTemplate
{
    public static bool doll = true;

    public static bool Doll
    {
        get { return doll; }
        set { doll = value; }
    }
}
