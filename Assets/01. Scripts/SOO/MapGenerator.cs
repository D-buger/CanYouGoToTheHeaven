using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;

    private void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateMap();
        }
    }

    private void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 7; i++)
        {
            SmoothMap();
        }
    }

    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }
        
        //prng�� pseudoRandomNumberGenerator �� �����ε�, ��ǻ�ʹ� ������ �������� ���ؼ� �Ϲ������� ������ ���ؼ� seed�� �ʿ��ϴ�.
        //�̶� �� ������ ���� seed�� �����ϰ� �������ִ°� �ʿ��ѵ�, �Ϲ������� prng ��� �θ��µ��ϴ�.
        System.Random prng = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //�� �׵θ��� 1���� ���� �����.
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    //�����ۼ�Ʈ���� ������ 1�� ��ȯ�ϴµ�, �̴� 1���� �ǹ��ϰ� 0�� �� ������ �ǹ��Ѵ�.
                    map[x, y] = (prng.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiels = GetSurroundingWallCount(x, y);
                //�̿��� Ÿ���� n�� �ʰ��� �ڽŵ� ���̵ȴ�..
                if (neighbourWallTiels > 4)
                {
                    map[x, y] = 1;
                }
                //�̿��� Ÿ���� n�� �̸��̸� �ո� �����̵ȴ�..
                //�� �̿��� Ÿ���� �������� ���� ���������� ���̵ǰ�, �������� ���� ���������� ������� �Ǵ°�.
                else if (neighbourWallTiels < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        //�Ʒ� 2�� for���� gridX�� �ָ� 3*3�� �ִ� �̿��� ���� �����´�.
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                //�Ʒ� �������� ���湮�ε�. ���μ��ΰ� �̳����� �̿����� ����ϱ����� ���ǹ�
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    //�̿��� ���� �������� ���� �ڱ��ڽ��� ���� �����Ѵ�
                    //�ڱ� �ڽ��� ���� �̿��� 8���� Ÿ�Ͽ��� ���� ������ ī��Ʈ�մϴ�
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    //�̿����� ����Ҷ� width, height���� �������� �ʾ����Ƿ� �׵θ��� ��� ������ �����Ǹ� ī��Ʈ�ȴ�;
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

    private void OnDrawGizmos()
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (map[x, y] == 1) ? Color.black : (map[x, y] == 0)? Color.white : Color.gray;
                    Vector3 pos = new Vector3(-width / 2 + x + 0.5f, -height / 2 + y + 0.5f, 0);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }
}
    