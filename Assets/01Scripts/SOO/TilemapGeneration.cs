using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGeneration : MonoBehaviour
{
    private MapComponent[] maps;
    public readonly int distXBetwnStages = 50;

    public List<int> XPositions { get; private set; } = new List<int>();

    public float MapYSize { get; private set; }
    public float EndYPosition { get; private set; }

    public int roomInStage;
    public int levelInStage;

    private float cellSize;

    public Tile[] tile;
    private Grid grid;
    private Tilemap tilemap;

    private GameObject monsterSpawner;

    public void Awake()
    {
        monsterSpawner = Resources.Load<GameObject>("Prefabs/MonsterSpawnPoint");

        maps =  new MapGenerator(5, 6).Map;
        GameObject _mapParent = this.gameObject;
        grid = _mapParent.transform.GetChild(0).GetComponent<Grid>();
        tilemap = grid.transform.GetChild(0).GetComponent<Tilemap>();
        MapYSize = grid.cellSize.y * MapGenerator.MapHeightTile;
        EndYPosition = (MapYSize * roomInStage) - (MapYSize / 2);
        MapGeneration(0);
    }

    private void MapGeneration(int startX)
    {
        for(int stageNum = 1; stageNum <= MapGenerator.Stage; stageNum++)
        {
            for(int stageRepeat = 0; stageRepeat < levelInStage; stageRepeat++)
            {
                XPositions.Add(startX);
                LevelGeneration(startX, stageNum);
                startX += distXBetwnStages + MapGenerator.MapWidthTile;
            }
        }
    }


    private void LevelGeneration(int startX, int nowStage)
    {
        int i;
        for(i = 0; i < roomInStage - 1; i++)
        {
            Generation(
                new Vector2Int(startX, i * MapGenerator.MapHeightTile), 
                maps[
                    Random.Range(
                        (nowStage - 1) * MapGenerator.RoomsInStage, 
                        nowStage * MapGenerator.RoomsInStage - 1)]);
        }
        Generation(
            new Vector2Int(startX, i * MapGenerator.MapHeightTile), 
            maps[nowStage * MapGenerator.RoomsInStage - 1]);
    }

    private void Generation(Vector2Int startPosition, MapComponent map)
    {
        int height;
        int width;
        for (int i = MapGenerator.MapWidthTile * MapGenerator.MapHeightTile - 1; i >= 0; i--)
        {
            width = (i / MapGenerator.MapHeightTile);
            height = i % MapGenerator.MapHeightTile;
            tilemap.SetTile(new Vector3Int(startPosition.x + width, startPosition.y + MapGenerator.MapHeightTile - height, 0), 
                map.tiles[height, width] == 0 ? null : tile[map.tiles[height, width]]);
        }
    }
}
