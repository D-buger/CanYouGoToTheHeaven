using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public Point(Vector2 dir, Vector2 pos)
    {
        Direction = dir;
        PointPosition = pos;
    }
    public Point(Vector2 dir, Vector2 pos, float _speed) : this(dir, pos)
    {
        speed = _speed;
    }

    public Vector2 Direction { get; set; }
    public Vector2 PointPosition { get; set; }
    private float speed = 2f;

    public int maxReflection { get; set; }

    public void PointMove()
    {
        PointPosition = Vector2.MoveTowards(PointPosition, PointPosition + Direction, 1 * speed);
    }

}