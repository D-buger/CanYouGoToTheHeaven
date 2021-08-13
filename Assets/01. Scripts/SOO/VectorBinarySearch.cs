using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VectorBinarySearch : IComparer<Vector2>
{
    Vector2 targetVector;

    public VectorBinarySearch(Vector2 vec )
    {
        targetVector = vec;
    }

    public int Compare(Vector2 x, Vector2 y)
    {
        return Vector2.Distance(x, targetVector).CompareTo(Vector2.Distance(y, targetVector));
    }
}
