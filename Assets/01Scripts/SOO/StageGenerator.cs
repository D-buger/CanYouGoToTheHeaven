using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    private LevelGeneration levelGeneration;

    [SerializeField]
    private int roomInStage;
    [SerializeField]
    private int levelInStage;
    
    [SerializeField]
    private float distXBetwnStages;

    [SerializeField]
    private Vector2 firstStagePos;

    public List<Vector2> StartPositions { get; private set; }
    public List<Vector2> EdgePositions { get; private set; }

    private void Awake()
    {
        EdgePositions = new List<Vector2>();
        //levelGeneration = new LevelGeneration(this.gameObject);
        
    }
}
