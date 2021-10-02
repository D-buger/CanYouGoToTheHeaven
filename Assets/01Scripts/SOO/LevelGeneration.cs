using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LevelGeneration
{
    public MapComponent?[] maps = MapGenerator.Map;
    public float distXBetwnStages;

    private Tilemap[] tilemaps;
    private float cellSize;

    public void FirstSetting()
    {
        tilemaps = new Tilemap[rooms.Length];
        for(int i = 0; i < rooms.Length; i++)
        {
            tilemaps[i] = rooms[i].transform.GetChild(0)
                .transform.GetChild(0).GetComponent<Tilemap>();
        }
        cellSize = rooms[0].transform.GetChild(0).GetComponent<Grid>().cellSize.y;  
    }

    public Vector2[] Generation(Vector2 gnratrPos, int _rmInStg, int _lvelInStg, Transform parent)
    {
        FirstSetting();

        Vector2[] pointPosition = new Vector2[_lvelInStg * 2];

        int random, index = 0;
        GameObject room;
        float ySize;
        Vector2 nextPos;
        
        do
        {
            nextPos = gnratrPos;
            pointPosition[index++] = nextPos;

            for (int i = 0; i < _rmInStg - 1; i++)
            {
                random = Random.Range(0, rooms.Length - 1);
                room = GameObject.Instantiate(rooms[random], nextPos, Quaternion.identity, parent);
                ySize = tilemaps[random].size.y * cellSize;
                nextPos = room.transform.GetChild(0).position;
                nextPos.y += ySize;
            }

            room = GameObject.Instantiate(rooms[rooms.Length - 1], nextPos, Quaternion.identity, parent);
            ySize = tilemaps[rooms.Length - 1].size.y * cellSize;
            nextPos.y += ySize / 2;
            pointPosition[index++] = nextPos;

            gnratrPos.x += distXBetwnStages;
        } while (--_lvelInStg > 0);
        
        return pointPosition;
    }
}
