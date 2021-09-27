using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public Point(Vector2 dir, Vector2 pos, int reflect, float time)
    {
        Direction = dir;
        PointPosition = pos;
        maxReflection = reflect;
        timer = new Timer(time);
    }
    public Point(Vector2 dir, Vector2 pos, int reflect, float _speed, float time) : this(dir, pos, reflect, time)
    {
        speed = _speed;
    }

    public Vector2 Direction { get; set; }
    public Vector2 PointPosition { get; set; }
    private float speed = 0.3f;
    private Timer timer;

    public int maxReflection { get; set; }

    public bool PointMove()
    {
        if (timer.TimerUpdate())
            return false;
        PointPosition = Vector2.MoveTowards(PointPosition, PointPosition + Direction, 1 * speed);
        return true;
    }
}