using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [SerializeField]
    private List<LevelGeneration> levelGenerations;

    [SerializeField]
    private int roomInStage;
    [SerializeField]
    private int levelInStage;
    
    [SerializeField]
    private float distXBetwnStages;

    [SerializeField]
    private Vector2 firstStagePos;

    [SerializeField]
    private GameObject shopPrefab;

    private int playerStage = 0;

    public List<Vector2[]> Rooms { get; private set; }

    private void Awake()
    {
        Rooms = new List<Vector2[]>();
        for(int i = 0; i < levelGenerations.Count; i++)
        {
            firstStagePos.x += i * distXBetwnStages;
            Rooms.Add(levelGenerations[i].Generation(firstStagePos,roomInStage, levelInStage, transform));
        }
    }

    private void Update()
    {
        //TODO : stage ���� �Ѿ �� shop���� �̵�, shop���� ������ �� ���� ���������� �÷��̾� �̵�
    }


}