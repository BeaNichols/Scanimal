using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public enum type
    { 
        Water,
        Ground,
        Beach,
        Spawn
    }

    public type CurrentType;

    public Cell(type CellType)
    {
        CurrentType = CellType;
    }
}
