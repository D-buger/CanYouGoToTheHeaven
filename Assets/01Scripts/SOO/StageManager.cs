using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StageGenerator))]
public class StageManager : SingletonBehavior<StageManager>
{
    public CameraManager CameraManager { get; private set; }
    public UIManager UiManager { get; private set; }
    public ItemManager ItemManager { get; private set; } 

    public PlayerStats Stat { get; private set; }
    public GameObject Player { get; private set; }

    public StageGenerator StageGenerator { get; private set; }

    public Vector2 ShopPosition { get; private set; }
    public Vector2 ShopDoorPosition { get; private set; }

    private int playerRoom = 1;
    public int PlayerRoom() => playerRoom++;

    protected override void OnAwake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Stat = Player.GetComponent<Player>().stats;
        
        CameraManager = Camera.main.gameObject.GetComponent<CameraManager>();
        StageGenerator = GetComponent<StageGenerator>();
        ItemManager = new ItemManager();
        UiManager = new UIManager();
    }

    private void Update()
    {
        if (Player.transform.position.y 
            >= StageGenerator.EdgePositions[playerRoom].y)
        {
            Player.transform.position = ShopDoorPosition;
            playerRoom++;
        }

        if (Player.transform.position.y - (GameManager.Instance.windowSize.x / 2)
            >= StageGenerator.EdgePositions[playerRoom].y)
        {
            CameraManager.CameraLock = true;
        }
    }

    public void PlayerTeleportToStage()
    {
        Player.transform.position = StageGenerator.EdgePositions[playerRoom++];
    }
}
