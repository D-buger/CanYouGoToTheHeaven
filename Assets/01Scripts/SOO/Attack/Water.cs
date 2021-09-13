using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Water : MonoBehaviour
{
    private float pointDist = 0.01f;
    private int waterReflected = 1;

    private string reflectTag = TagManager.WallTag;
    private string removeTag = TagManager.EnemyTag; 
    
    private LineRenderer line;
    
    private bool isActive = false;

    private List<Point> points = new List<Point>();

    private int damage = 1;

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
    
    public void SetFirst(int damage)
    {
        isActive = true;
        this.damage = damage;
        SetLineRenderer();
    }

    public bool VertexSet(Vector2 nowPos, Vector2 angle, float time)
    {
        if (points.Count == 0 || Vector2.Distance(points[points.Count - 1].PointPosition, nowPos) >= pointDist)
        {
            points.Add(new Point(angle, nowPos, waterReflected, time));

            return true;
        }
        return false;
    }

    //frame Update
    private void WaterUpdate()
    {
        for (int i = 0;i < points.Count; i++)
        {
            PointUpdate(points[i]);
        }

        SetLineRenderer();
    }

    RaycastHit2D ray;
    private void Raycast(Point point)
    {
        ray = Physics2D.Raycast(point.PointPosition, point.Direction, 0.01f);
        if(ray)
        {
            CheckTags(ray, point);
        }
    }

    private void CheckTags(RaycastHit2D ray, Point point)
    {
        if (ray.transform.CompareTag(reflectTag))
        {
            Reflect(point);
        }
        else if (ray.transform.CompareTag(removeTag))
        {
            RemovePoint(point);
        }
    }

    private void Reflect(Point point)
    {
        if (point.maxReflection-- > 0)
        {
            Vector2 inNormal = Vector2.right;
            point.Direction = Vector2.Reflect(point.Direction, inNormal);
        }
        else
        {
            RemovePoint(point);
        }
    }

    private void RemovePoint(Point point)
    {
        List<Vector2> pointsVec = points.ConvertAll<Vector2>((Point p) => p.PointPosition);
        int index = points.IndexOf(point);

        if (index == 0 || index == points.Count)
        {
            points.Remove(point);
        }
        else
        {
            List<Point> newList = points.GetRange(0, index);
            points.RemoveRange(0, index);

            GameObject obj = ObjectPoolManager.Inst.pool.Pop();
            obj.GetComponent<Water>().Duplicate(newList);
        }
        SetLineRenderer();
    }

    public void Duplicate(List<Point> point)
    {
        points = point;

        isActive = true;
        SetLineRenderer();
    }

    private void PointUpdate(Point point)
    {
        if (!point.PointMove())
            RemovePoint(point);
        else
            Raycast(point);
    }

    private void SetLineRenderer()
    {
        line.SetVertexCount(points.Count);
        
        line.SetPositions(points.ConvertAll<Vector3>((Point p) => p.PointPosition).ToArray());
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