using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance = null;

    void MakeSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public GameObject[] EGradeMonster;
    public GameObject[] DGradeMonster;
    public GameObject[] CGradeMonster;
    public GameObject[] BGradeMonster;
    public GameObject[] AGradeMonster;
    public GameObject[] SGradeMonster;
    public GameObject[] EliteMonster;
    //Dictionary 활용해서 여러개 만들기?

    [HideInInspector] public GameObject player{ get; private set; }
    public GameObject TreasureRoomPortalPrefab;
    public GameObject goldenMonsterParticle;

    public void FindPlayer()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError($"{gameObject.name}: Warnning!! Player is null!");
        }
    }

    private void Start()
    {
        FindPlayer();
        CheckVariable();
    }

    void CheckVariable()
    {
        if (TreasureRoomPortalPrefab == null)
        {
            Debug.LogWarning($"{gameObject.name}: '보물방 포탈 프리팹'이 할당되어있지 않습니다!");
        }
        if (goldenMonsterParticle == null)
        {
            Debug.LogWarning($"{gameObject.name}: '황금 몬스터 파티클'이 할당되어있지 않습니다!");
        }
    }

    private void Awake()
    {
        MakeSingleton();
        FindPlayer();
    }
}
