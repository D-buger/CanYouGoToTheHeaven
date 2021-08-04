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
    private float pointDist = 1f;
    [SerializeField]
    private int waterReflected = 1;

    private bool isActive = false;

    private List<Point> points = new List<Point>();

    private void Awake()
    {
        if (line == null)
            line = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        if (points.Count == 0)
        {
            points.Add(new Point(Vector2.up, Vector2.zero));
        }
        else
        {
            points[0] = new Point(Vector2.up, transform.position);
        }

        SetLineRenderer();
    }

    public void UpdateWater()
    {
         if (!isActive)
             isActive = true;
         VertexSet();
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            WaterUpdate();
            Disable();
        }
    }
    

    public void VertexSet()
    {
        if (points[points.Count - 1].PointPosition.y - transform.position.y >= pointDist)
        {
            points.Add(new Point(Vector2.up, transform.position));
        }
    }

    private void WaterUpdate()
    {
        CheckTags();


        for (int i = 0;i < points.Count; i++)
        {
            PointUpdate(points[i]);  
        }

        SetLineRenderer();
    }

    private void CheckTags()
    {

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
    public Point(Vector2 dir, Vector2 pos)
    {
        Direction = dir;
        PointPosition = pos;
        ray = Physics2D.Raycast(PointPosition, Vector2.up, 0.001f);
    }

    public Vector2 Direction { get; set; }
    public Vector2 PointPosition { get; set; }
    public RaycastHit2D ray { get; set; }

    public void PointMove()
    {
        PointPosition = Vector2.MoveTowards(PointPosition, PointPosition + Direction, 0.1f);
        Debug.DrawRay(PointPosition, Vector2.up, Color.red, 0.001f);
    }


}