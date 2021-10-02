using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGeneration : MonoBehaviour
{
    public MapComponent[] maps;
    public float distXBetwnStages = 15;
    public float distYBetwnRooms;

    public int roomInStage;
    public int levelInStage;

    private float cellSize;

    public Tile[] tile;
    private Grid grid;
    private Tilemap tilemap;

    private GameObject monsterSpawner;

    public void Awake()
    {
        maps =  new MapGenerator(5, 6).Map;
        GameObject _mapParent = this.gameObject;
        grid = _mapParent.transform.GetChild(0).GetComponent<Grid>();
        tilemap = grid.transform.GetChild(0).GetComponent<Tilemap>();

        distYBetwnRooms = MapGenerator.MapHeightTile * grid.cellSize.y;
        LevelGeneration(transform.position, 1);
    }

    private void LevelGeneration(Vector2 levelFirstPosition, int nowStage)
    {
        int i;
        for(i = 0; i < roomInStage - 1; i++)
        {
            Generation(i * MapGenerator.MapHeightTile, 
                maps[Random.Range((nowStage - 1) * MapGenerator.RoomsInStage + 1, nowStage * MapGenerator.RoomsInStage - 1)]);
        }
        Debug.Log(nowStage * MapGenerator.RoomsInStage);
        Generation(i * MapGenerator.MapHeightTile, maps[nowStage * MapGenerator.RoomsInStage]);
    }

    private void Generation(int startY, MapComponent map)
    {
        int height;
        int width;
        for (int i = MapGenerator.MapWidthTile * MapGenerator.MapHeightTile - 1; i >= 0; i--)
        {
            width = (i / MapGenerator.MapHeightTile);
            height = i % MapGenerator.MapHeightTile;
            tilemap.SetTile(new Vector3Int(width, startY + MapGenerator.MapHeightTile - height, 0), 
                map.tiles[height, width] == 0 ? null : tile[map.tiles[height, width]]);
        }
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
