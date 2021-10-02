using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGeneration : MonoBehaviour
{
    public MapComponent?[] maps = MapGenerator.Map;
    public float distXBetwnStages = 15;
    public float distYBetwnRooms;

    public int roomInStage;
    public int levelInStage;

    private float cellSize;

    public RuleTile ruleTile;
    private Grid grid;
    private Tilemap tilemap;

    private GameObject monsterSpawner;

    public void Awake()
    {
        GameObject _mapParent = this.gameObject;
        grid = _mapParent.transform.GetChild(0).GetComponent<Grid>();
        tilemap = grid.transform.GetChild(0).GetComponent<Tilemap>();

        distYBetwnRooms = MapGenerator.MapHeightTile * grid.cellSize.y;

        tilemap.SetTile(new Vector3Int(0, 0, 0) , ruleTile.m_Self);
    }

    public void Generation(Vector2 levelFirstPosition)
    {

    }


    //public Vector2[] Generation(Vector2 gnratrPos, int _rmInStg, int _lvelInStg, Transform parent)
    //{
    //    FirstSetting();

    //    Vector2[] pointPosition = new Vector2[_lvelInStg * 2];

    //    int random, index = 0;
    //    GameObject room;
    //    float ySize;
    //    Vector2 nextPos;
        
    //    do
    //    {
    //        nextPos = gnratrPos;
    //        pointPosition[index++] = nextPos;

    //        for (int i = 0; i < _rmInStg - 1; i++)
    //        {
    //            random = Random.Range(0, rooms.Length - 1);
    //            room = GameObject.Instantiate(rooms[random], nextPos, Quaternion.identity, parent);
    //            ySize = tilemaps[random].size.y * cellSize;
    //            nextPos = room.transform.GetChild(0).position;
    //            nextPos.y += ySize;
    //        }

    //        room = GameObject.Instantiate(rooms[rooms.Length - 1], nextPos, Quaternion.identity, parent);
    //        ySize = tilemaps[rooms.Length - 1].size.y * cellSize;
    //        nextPos.y += ySize / 2;
    //        pointPosition[index++] = nextPos;

    //        gnratrPos.x += distXBetwnStages;
    //    } while (--_lvelInStg > 0);
        
    //    return pointPosition;
    //}
}
