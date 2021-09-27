using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StageGenerator))]
public class StageManager : SingletonBehavior<StageManager>
{
    public CameraManager CameraManager { get; private set; }
    public ItemManager ItemManager { get; private set; }

    public PlayerStats Stat { get; private set; }
    public GameObject Player { get; private set; }

    public StageGenerator StageGenerator { get; private set; }

    public Shop Shop { get; private set; }

    private int playerRoom = 1;
    public int PlayerRoom() => playerRoom++;

    protected override void OnAwake()
    {
        ItemManager = new ItemManager();
        Player = GameObject.FindGameObjectWithTag("Player");
        Stat = Player.GetComponent<Player>().stats;

        Shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
        CameraManager = Camera.main.gameObject.GetComponent<CameraManager>();
        StageGenerator = GetComponent<StageGenerator>();
    }

    private void Update()
    {
        if (PlayerInStage)
        {
            if (CameraManager.Screen2World(GameManager.ScreenSize).y
                > StageGenerator.EdgePositions[playerRoom].y)
            {
                CameraManager.CameraLock = true;
            }

            if (Player.transform.position.y
                >= StageGenerator.EdgePositions[playerRoom].y)
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
        Vector3 playerTeleportPosition = StageGenerator.EdgePositions[playerRoom];
        playerTeleportPosition.y -= 10; 
        Player.transform.position = StageGenerator.EdgePositions[playerRoom];
        PlayerInStage = true;
        
        CameraManager.CamPositionChange(StageGenerator.EdgePositions[playerRoom++]);
        CameraManager.CameraLock = false;
    }
}
