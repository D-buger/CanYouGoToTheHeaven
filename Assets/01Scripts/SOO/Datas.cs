using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StageAchivementData
{
    public StageAchivementData(Timer _timer)
    {
        timer = _timer;
        monsterKill = 0;
        goldMonsterKill = 0;
        refillCount = 0;
        gottenItemCount = 0;
        isBuyInShop = false;
        isKillMonster = false;
    }

    public Timer timer;

    public bool isKillMonster;

    private int monsterKill;
    public int MonsterKill
    {
        get => monsterKill;
        set
        {
            monsterKill = value;
            SumDatas();
        }
    }

    private int goldMonsterKill;
    public int GoldMonsterKill
    {
        get => goldMonsterKill;
        set
        {
            goldMonsterKill = value;
            if (GameManager.Data.GoldMonsterKill == 0
                && goldMonsterKill == 1)
                AchievementManager.Achivement_1();
            
            SumDatas();
        }
    }

    private int refillCount;
    public int RefillCount
    {
        get => refillCount;
        set
        {
            refillCount = value;
            SumDatas();
        }
    }

    public bool isBuyInShop;

    private int gottenItemCount;
    public int GottenItemCount
    {
        get => gottenItemCount;
        set
        {
            gottenItemCount = value;
            SumDatas();
        }
    }

    public void SumDatas()
    {
        GameManager.Data.SumDatas(this);
    }
}