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

    private void Awake()
    {
        for(int i = 0; i < levelGenerations.Count; i++)
        {
            levelGenerations[i].Generation(roomInStage, levelInStage);
        }
    }


}
