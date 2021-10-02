using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonBehavior<StageManager>
{
    public CameraManager CameraManager { get; private set; }
    public ItemManager ItemManager { get; private set; }
    public TilemapGeneration MapManager { get; private set; }

    public PlayerStats Stat { get; private set; }
    public GameObject Player { get; private set; }

    public Shop Shop { get; private set; }

    private int playerRoom = 1;

    protected override void OnAwake()
    {
        ItemManager = new ItemManager();
        Player = GameObject.FindGameObjectWithTag("Player");
        Stat = Player.GetComponent<Player>().stats;
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Wall"))
        {
            if (obj.name == nameof(MapManager))
                MapManager = obj.GetComponent<TilemapGeneration>();
        }

        Shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
        CameraManager = Camera.main.gameObject.GetComponent<CameraManager>();
    }

    private void Update()
    {
        if (PlayerInStage)
        {
            if (CameraManager.Screen2World(GameManager.ScreenSize).y
                > MapManager.EndYPosition)
            {
                CameraManager.CameraLock = true;
            }

            if (Player.transform.position.y
                >= MapManager.EndYPosition)
            {
                PlayerTeleportToShop();
            }
        }
    }

    public static bool PlayerInStage { get; private set; } = true;
    
    private void PlayerTeleportToShop()
    {
        Shop.enabled = true;
        PlayerInStage = false;
        Player.transform.position = Shop.DoorPosition;
        playerRoom++;

        CameraManager.CamPositionChange(Shop.ShopPosition);
    }

    public void PlayerTeleportToStage()
    {
        Shop.enabled = false;
        //Vector3 playerTeleportPosition = StageGenerator.EdgePositions[playerRoom];
        //playerTeleportPosition.y -= 10; 
        //Player.transform.position = StageGenerator.EdgePositions[playerRoom];
        PlayerInStage = true;
        
        //CameraManager.CamPositionChange(StageGenerator.EdgePositions[playerRoom++]);
        CameraManager.CameraLock = false;
    }
}
