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

    public Shop Shop { get; private set; }

    private int playerRoom = 1;
    public int PlayerRoom() => playerRoom++;

    protected override void OnAwake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Stat = Player.GetComponent<Player>().stats;

        Shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
        CameraManager = Camera.main.gameObject.GetComponent<CameraManager>();
        StageGenerator = GetComponent<StageGenerator>();
        ItemManager = new ItemManager();
        UiManager = new UIManager();
    }

    private void Update()
    {
        //카메라 매니저에서 검사할지 스테이지 매니저에서 검사할지 고민해봐야함
        //작동되는 수식 자체가 틀려먹음 이것만 고치면 될듯
        if (Player.transform.position.y
            >= StageGenerator.EdgePositions[playerRoom].y
            - CameraManager.WorldToViewportPoint(0))
        {
            CameraManager.CameraLock = true;
        }

        if (PlayerInStage &&
            Player.transform.position.y 
            >= StageGenerator.EdgePositions[playerRoom].y)
        {
            PlayerTeleportToShop();
        }
        
    }

    private void PlayerTeleportToShop()
    {
        PlayerInStage = false;
        Player.transform.position = Shop.DoorPosition;
        CameraManager.CamPositionChange(Shop.ShopPosition);
        playerRoom++;
    }

    public bool PlayerInStage { get; private set; } = true;

    public void PlayerTeleportToStage()
    {
        Player.transform.position = StageGenerator.EdgePositions[playerRoom++];
        PlayerInStage = true;
    }
}
