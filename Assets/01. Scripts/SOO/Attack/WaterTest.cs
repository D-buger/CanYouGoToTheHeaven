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
    [SerializeField]
    private EdgeCollider2D coll;

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
        if (coll == null)
            coll = GetComponent<EdgeCollider2D>();
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
            Reflect(ray, point);
        }
    }

    private void Reflect(RaycastHit2D ray, Point point)
    {
        if (ray.transform.CompareTag(reflectTag) && point.maxReflection-- > 0)
        {
            Vector2 inNormal = Vector2.right;
            point.Direction = Vector2.Reflect(point.Direction, inNormal);
        }
    }

    private void PointUpdate(Point point)
    {
        point.PointMove();
    }

    private void SetLineRenderer()
    {
        line.SetVertexCount(points.Count);

        coll.SetPoints(points.ConvertAll<Vector2>((Point p) => p.PointPosition));
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

    private void RemovePoint(Collision2D collision)
    {
        Debug.Log("삭제");
        Debug.Log(collision.contactCount);
        List<Point> contacts = new List<Point>();
        List<Vector2> pointsVec = points.ConvertAll<Vector2>((Point p) => p.PointPosition);
        int index;

        if (pointsVec.Contains(collision.contacts[0].point))
        {
            index = pointsVec.IndexOf(collision.contacts[0].point);
        }
        else
        {

        }

        
        if (collision.contacts[0].point == points[0].PointPosition)
        {
            points.RemoveAt(0);
        }
        else if (collision.contacts[0].point == points[points.Count].PointPosition)
        {
            points.RemoveAt(points.Count - 1);
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

    //선형탐색이지만 정보를 버리지 않으므로 이진 탐색보다 빠르다.
    private void Sweeping()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(removeTag))
        {
            RemovePoint(collision);
        }
    }
}