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

    private int playerStage = 1;

    private void Awake()
    {
        for(int i = 0; i < levelGenerations.Count; i++)
        {
            firstStagePos.x += i * distXBetwnStages;
            levelGenerations[i].Generation(firstStagePos,roomInStage, levelInStage, transform);
        }
    }

    private void Update()
    {
        //TODO : stage 끝을 넘어갈 시 shop으로 이동, shop에서 나갔을 때 다음 스테이지로 플레이어 이동
    }


}
