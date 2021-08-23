using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject[] rooms;
    [SerializeField]
    private int roomInStage;
    [SerializeField]
    private Tilemap[] tilemaps;
    private float cellSize;

    private void Start()
    {
        tilemaps = new Tilemap[rooms.Length];
        for(int i = 0; i < rooms.Length; i++)
        {
            tilemaps[i] = rooms[i].transform.GetChild(0)
                .transform.GetChild(0).GetComponent<Tilemap>();
        }
        cellSize = rooms[0].transform.GetChild(0).GetComponent<Grid>().cellSize.y;

        Generation();
    }

    private void Generation()
    {
        int random;
        GameObject room;
        float ySize;
        Vector2 nextPos = transform.position;

        for (int i = 0; i < roomInStage; i++)
        {
            random = Random.Range(0, rooms.Length);
            room = Instantiate(rooms[random], nextPos, Quaternion.identity);
            ySize = tilemaps[random].size.y * cellSize;
            nextPos = room.transform.GetChild(0).position;
            nextPos.y += ySize;
        }
    }
}
