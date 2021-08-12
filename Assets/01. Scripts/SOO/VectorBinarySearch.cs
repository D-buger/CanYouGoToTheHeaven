using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VectorBinarySearch : IComparer<Vector2>
{
    public int Compare(Vector2 x, Vector2 y)
    {



        return x.y.CompareTo(y.y);
    }
}
