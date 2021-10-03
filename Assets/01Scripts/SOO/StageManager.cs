using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonBehavior<StageManager>
{
    public static StageAchivementData data
                        = new StageAchivementData(new Timer());

    public CameraManager CameraManager { get; private set; }
    public ItemManager ItemManager { get; private set; }
    public TilemapGeneration MapManager { get; private set; }

    public PlayerStats Stat { get; private set; }
    public GameObject Player { get; private set; }

    public Shop Shop { get; private set; }
    public GameObject BonusRoom { get; private set; }

    public int monsterCount;

    public static event System.Action<int> changeCurrentRoom;
    private int playerRoom = 1; //현재 스테이지안의 방
    public int PlayerRoom
    {
        get
        {
            return playerRoom;
        }
        private set
        {
            playerRoom = value;
            changeCurrentRoom?.Invoke(playerRoom);
        }
    }

    public static event System.Action clearEvent;

    protected override void OnAwake()
    {
        GameManager.Instance.input.Touch.ButtonExtent = 0.5f;
        data = new StageAchivementData(new Timer());
        clearEvent += () => GameManager.Data.SumDatas(data);
        clearEvent += () => GameManager.Data.ClearCount++;

        ItemManager = new ItemManager();
        Player = GameObject.FindGameObjectWithTag("Player");
        Stat = Player.GetComponent<Player>().stats;
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Wall"))
        {
            if (obj.name == nameof(MapManager))
                MapManager = obj.GetComponent<TilemapGeneration>();
        }

        Shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
        Shop.gameObject.SetActive(false);

        BonusRoom = GameObject.FindGameObjectWithTag("BonusRoom");
        BonusRoom.SetActive(false);

        CameraManager = Camera.main.gameObject.GetComponent<CameraManager>();
        PlayerInStage = true;
    }

    private void Update()
    {
        data.timer.TimerUpdate();
        if (PlayerInStage)
        {
            if (CameraManager.Screen2World(GameManager.ScreenSize).y
                > MapManager.EndYPosition)
            {
                CameraManager.CameraLock = true;
            }

            if (CameraManager.Screen2World(GameManager.ScreenSize).y < 0)
            {
                CameraManager.CameraLock = true;
                CameraManager.CamPositionChangeY(0);
            }
            if (CameraManager.TargetIsInRange() &&
                CameraManager.gameObject.transform.position.y == 0)
            {
                CameraManager.CameraLock = false;
            }

            if (Player.transform.position.y
                >= MapManager.EndYPosition)
            {
                if (PlayerRoom >= MapManager.levelInStage * MapManager.stageCount)
                    clearEvent.Invoke();
                else
                    PlayerTeleportToShop();
            }
        }
    }

    public static bool PlayerInStage { get; private set; } = true;
    private Vector3 nextMovePosition;
    
    private void PlayerTeleportToShop()
    {
        nextMovePosition = new Vector3(MapManager.XPositions[PlayerRoom++], -MapManager.MapYSize / 2, 0);
        Shop.gameObject.SetActive(true);
        PlayerInStage = false;
        Player.transform.position = Shop.DoorPosition;

        CameraManager.CameraLock = true;
        CameraManager.CamPositionChange(Shop.ShopPosition);
    }

    public void PlayerTeleportToBonusRoom(Vector3 originalPos)
    {
        nextMovePosition = originalPos;

        Player.transform.position = BonusRoom.transform.GetChild(2).position;
        BonusRoom.gameObject.SetActive(true);
        PlayerInStage = false;


        CameraManager.CameraLock = true;
        CameraManager.CamPositionChange(BonusRoom.transform.position);
    }

    public void PlayerTeleportToStage()
    {
        Shop.gameObject.SetActive(false);
        Player.transform.position = nextMovePosition;
        PlayerInStage = true;
        
        nextMovePosition.z = CameraManager.transform.position.z;
        CameraManager.CamPositionChange(nextMovePosition);
        CameraManager.CameraLock = false;
    }
}
