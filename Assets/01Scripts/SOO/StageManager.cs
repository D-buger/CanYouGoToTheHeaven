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

    public int playerRoom = 1; //현재 스테이지

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
        Shop.enabled = false;

        CameraManager = Camera.main.gameObject.GetComponent<CameraManager>();
        PlayerInStage = true;
    }

    private void Update()
    {
        if (PlayerInStage)
        {
            if (CameraManager.Screen2World(GameManager.ScreenSize).y
                > MapManager.EndYPosition ||
                CameraManager.Screen2World(GameManager.ScreenSize).y < 0)
            {
                CameraManager.CameraLock = true;
            }
            else if (CameraManager.Screen2World(GameManager.ScreenSize).y >= 0 && CameraManager.CameraLock)
            {
                CameraManager.CameraLock = false;
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

        CameraManager.CameraLock = true;
        CameraManager.CamPositionChange(Shop.ShopPosition);
    }

    public void PlayerTeleportToBonusRoom()
    {

    }

    public void PlayerTeleportToStage()
    {
        Shop.enabled = false;
        Vector3 stageStartPosition = new Vector3(MapManager.XPositions[playerRoom - 1] , -MapManager.MapYSize / 2, 0);
        Player.transform.position = stageStartPosition;
        PlayerInStage = true;

        stageStartPosition.y = 0;
        stageStartPosition.z = CameraManager.transform.position.z;
        CameraManager.CamPositionChange(stageStartPosition);
        CameraManager.CameraLock = false;
    }
}
