using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WaterTest : MonoBehaviour
{
    [SerializeField, Space(10), Header("Tags")]
    private string reflectTag = "Wall";
    [SerializeField]
    private string removeTag = "Untagged"; 

    [SerializeField, Space(10), Header("Components")]
    private LineRenderer line;

    [SerializeField, Space(10), Header("Values")]
    private float pointDist = 0.01f;
    [SerializeField]
    private int waterReflected = 1;

    private bool isActive = false;

    private List<Point> points = new List<Point>();

    private void Awake()
    {
        if (line == null)
            line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (isActive)
        {
            WaterUpdate();
            Disable();
        }
    }

    public void SetFirst(Vector2 nowPos, Vector2 angle)
    {
        if (points.Count == 0)
        {
            points.Add(new Point(angle, Vector2.zero, 0.5f));
        }
        else if (points.Count == 1)
        {
            points[0] = new Point(angle, nowPos, 0.5f);
        }

        SetLineRenderer();
    }

    public void UpdateWater(Vector2 nowPos, Vector2 angle)
    {
         if (!isActive)
             isActive = true;
        VertexSet(nowPos, angle);
    }

    public void VertexSet(Vector2 nowPos, Vector2 angle)
    {
        if (points[points.Count - 1].PointPosition.y - nowPos.y >= pointDist)
        {
            points.Add(new Point(angle, nowPos, 0.5f));
        }
    }

    private void WaterUpdate()
    {
        for (int i = 0;i < points.Count; i++)
        {
            PointUpdate(points[i]);
            Raycast(points[i]);
        }

        SetLineRenderer();
    }

    RaycastHit2D ray;
    private void Raycast(Point point)
    {
        ray = Physics2D.Raycast(point.PointPosition, point.Direction, 0.001f);
        if(ray)
        {
            CheckTags(ray, point);
        }
    }

    private void CheckTags(RaycastHit2D ray, Point point)
    {
        if (ray.transform.CompareTag(reflectTag))
        {
            Debug.Log("반사");
        }
        if (ray.transform.CompareTag(removeTag))
        {
            Debug.Log("삭제");
        }
    }

    private void PointUpdate(Point point)
    {
        point.PointMove();
    }

    private void SetLineRenderer()
    {
        line.SetVertexCount(points.Count);

        for (int i = 0; i < points.Count; i++)
        {
            line.SetPosition(i, points[i].PointPosition);
        }
    }

    private void Disable()
    {
        if (line.positionCount <= 0)
        {
            isActive = false;
            ObjectPoolManager.Inst.pool.Push(this.gameObject);
        }
    }
}

public class Point
{
    public Point(Vector2 dir, Vector2 pos, float _speed)
    {
        Direction = dir;
        PointPosition = pos;
        speed = _speed;
    }

    public Vector2 Direction { get; set; }
    public Vector2 PointPosition { get; set; }
    private float speed;

    public void PointMove()
    {
        PointPosition = Vector2.MoveTowards(PointPosition, PointPosition + Direction, 1 * speed);
    }

}