using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LevelGeneration
{
    public GameObject[] rooms;
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

    public GameObject[] Generation(Vector2 gnratrPos, int _rmInStg, int _lvelInStg, Transform parent)
    {
        FirstSetting();

        GameObject[] firstRoom = new GameObject[_lvelInStg];
        do
        {
            int random;
            GameObject room;
            float ySize;
            Vector2 nextPos = gnratrPos;

            for (int i = 0; i < _rmInStg - 1; i++)
            {
                random = Random.Range(0, rooms.Length);
                room = GameObject.Instantiate(rooms[random], nextPos, Quaternion.identity, parent);
                ySize = tilemaps[random].size.y * cellSize;
                nextPos = room.transform.GetChild(0).position;
                nextPos.y += ySize;
            }

            room = GameObject.Instantiate(rooms[rooms.Length - 1], nextPos, Quaternion.identity, parent);

            gnratrPos.x += distXBetwnStages;
        } while (_lvelInStg-- > 0);

        return firstRoom;
    }
}
