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
    }

    public Timer timer;

    private int monsterKill;
    public int MonsterKill
    {
        get => monsterKill;
        set
        {
            monsterKill = value;
        }
    }

    private int goldMonsterKill;
    public int GoldMonsterKill
    {
        get => goldMonsterKill;
        set
        {
            if (GameManager.Data.GoldMonsterKill == 0
                && goldMonsterKill == 1)
                AchievementManager.Achivement_1();

            goldMonsterKill = value;
        }
    }

    private int refillCount;
    public int RefillCount
    {
        get => refillCount;
        set
        {
            refillCount = value;
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
        }
    }

}