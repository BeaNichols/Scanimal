using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foliage
{
    public enum typeOfFoliage
    {
        Tree,
        Rock,
        Flower
    }

    public typeOfFoliage CurrentType;

    public float X;
    public float Y;
    public float Z;

    public float[] pos = {0,0,0};

    public Foliage(typeOfFoliage type, float X, float Y, float Z)
    {
        CurrentType = type;
        pos[0] = X;
        pos[1] = Y;
        pos[2] = Z;
    }
}
