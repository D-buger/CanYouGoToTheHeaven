using UnityEngine;

public class GameAchivementData
{
    public GameAchivementData(Json_GameData data)
    {
        gameData = data;
    }

    Json_GameData gameData;
    
    public int MonsterKill
    {
        get => gameData.monsterKill;
        set
        {
            gameData.monsterKill = value;
            
        }
    }
    
    public int RefillCount
    {
        get => gameData.refillCount;
        set
        {
            gameData.refillCount = value;
        }
    }
    
    public int GottenItemCount
    {
        get => gameData.gottenItemCount;
        set
        {
            gameData.gottenItemCount = value;

        }
    }
    
    public int GoldMonsterKill
    {
        get => gameData.goldMonsterKill;
        set
        {
            gameData.goldMonsterKill = value;
        }
    }
    
    public int ClearCount
    {
        get => gameData.clearCount;
        set
        {
            gameData.clearCount = value;
            if (gameData.clearCount == 1)
                AchievementManager.CanGoHeaven();
        }
    }
    
    public int GameOverCount
    {
        get => gameData.gameOverCount;
        set
        {
            gameData.gameOverCount = value;
            if (gameData.gameOverCount <= 100)
                AchievementManager.Achivement_3(gameData.gameOverCount);
        }
    }
    
    public int AchivementClear
    {
        get => gameData.achivementClear;
        set
        {
            gameData.achivementClear = value;
            if (gameData.achivementClear == 12)
                AchievementManager.CanMasterHEAVEN();
        }
    }

    public void SumDatas(StageAchivementData data)
    {
        gameData.shortestTime = gameData.shortestTime > data.timer.TimerTime ?
            data.timer.TimerTime : gameData.shortestTime;
        MonsterKill += data.MonsterKill;
        GoldMonsterKill += data.GoldMonsterKill;
        GottenItemCount += data.GottenItemCount;
        RefillCount += data.RefillCount;

        JsonManager.Save(gameData);
    }
}

public struct Json_GameData
{
    public double shortestTime;
    public int monsterKill;
    public int refillCount;
    public int gottenItemCount;
    public int goldMonsterKill;
    public int clearCount;
    public int gameOverCount;
    public int achivementClear;
}