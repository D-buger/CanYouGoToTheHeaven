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
    
    public void Duplicate(List<Point> point)
    {
        points = point;

        isActive = true;
        SetLineRenderer();
    }

    public void SetFirst(Vector2 nowPos, Vector2 angle)
    {
        if (points.Count == 0)
        {
            points.Add(new Point(angle, Vector2.zero));
        }
        else if (points.Count == 1)
        {
            points[0] = new Point(angle, nowPos);
        }

        isActive = true;
        SetLineRenderer();
    }

    public void VertexSet(Vector2 nowPos, Vector2 angle)
    {
        if (Vector2.Distance(points[points.Count - 1].PointPosition, nowPos) >= pointDist)
        {
            points.Add(new Point(angle, nowPos));
        }
    }

    //frame Update
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
    }

    private void RemovePoint(Point point)
    {
        Debug.Log("ªË¡¶");
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
            obj.GetComponent<WaterTest>().Duplicate(newList);
        }
        SetLineRenderer();
    }

    private void PointUpdate(Point point)
    {
        point.PointMove();
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