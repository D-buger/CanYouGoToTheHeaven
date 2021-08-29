using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public Point(Vector2 dir, Vector2 pos, int reflect)
    {
        Direction = dir;
        PointPosition = pos;
        maxReflection = reflect;
    }
    public Point(Vector2 dir, Vector2 pos, int reflect, float _speed) : this(dir, pos, reflect)
    {
        speed = _speed;
    }

    public Vector2 Direction { get; set; }
    public Vector2 PointPosition { get; set; }
    private float speed = 0.5f;

    public int maxReflection { get; set; }

    public void PointMove()
    {
        PointPosition = Vector2.MoveTowards(PointPosition, PointPosition + Direction, 1 * speed);
    }

}