using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedItems
{
    public string prefabName;

    public float X;
    public float Y;
    public float Z;

    public float[] pos = { 0, 0, 0 };

    public PlacedItems(string name, float X, float Y, float Z)
    {
        prefabName = name;

        pos[0] = X;
        pos[1] = Y;
        pos[2] = Z;
    }


}
