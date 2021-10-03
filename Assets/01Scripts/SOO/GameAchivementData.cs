using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/GameData", order =int.MaxValue)]
public class GameAchivementData : ScriptableObject
{
    public float shortestTime;

    private int monsterKill;
    public int MonsterKill
    {
        get => monsterKill;
        set
        {
            monsterKill = value;
            
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

    private int gottenItemCount;
    public int GottenItemCount
    {
        get => gottenItemCount;
        set
        {
            gottenItemCount = value;

        }
    }

    private int goldMonsterKill;
    public int GoldMonsterKill
    {
        get => goldMonsterKill;
        set
        {
            goldMonsterKill = value;
        }
    }

    private int clearCount;
    public int ClearCount
    {
        get => clearCount;
        set
        {
            clearCount = value;
            if (clearCount == 1)
                AchievementManager.CanGoHeaven();
        }
    }

    private int gameOverCount;
    public int GameOverCount
    {
        get => gameOverCount;
        set
        {
            gameOverCount = value;
            if (gameOverCount <= 100)
                AchievementManager.Achivement_3(gameOverCount);
        }
    }

    private int achivementClear;
    public int AchivementClear
    {
        get => achivementClear;
        set
        {
            achivementClear = value;
            if (achivementClear == 12)
                AchievementManager.CanMasterHEAVEN();
        }
    }

    public void SumDatas(StageAchivementData data)
    {
        shortestTime = shortestTime > data.timer.TimerTime ?
            data.timer.TimerTime : shortestTime;
        MonsterKill += data.MonsterKill;
        GoldMonsterKill += data.GoldMonsterKill;
        GottenItemCount += data.GottenItemCount;
        RefillCount += data.RefillCount;
    }
}