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

    public List<Vector2> EdgePositions { get; private set; }

    private void Awake()
    {
        EdgePositions = new List<Vector2>();
        for(int i = 0; i < levelGenerations.Count; i++)
        {
            firstStagePos.x += i * distXBetwnStages;
            EdgePositions.AddRange(levelGenerations[i].Generation(firstStagePos,roomInStage, levelInStage, transform));
        }
    }
}
