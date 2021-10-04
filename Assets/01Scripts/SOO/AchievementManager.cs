using UnityEngine;

public class AchievementManager
{
    public AchievementManager()
    {
        StageManager.clearEvent += Achivement_2;
        StageManager.clearEvent += Achivement_4;
        StageManager.clearEvent += Achivement_5;
        StageManager.clearEvent += Achivement_6;
        StageManager.clearEvent += Achivement_7;
        StageManager.clearEvent += Achivement_8;
        StageManager.clearEvent += Achivement_9;
        StageManager.clearEvent += Achivement_10;
    }

    ~AchievementManager()
    {
        StageManager.clearEvent -= Achivement_2;
        StageManager.clearEvent -= Achivement_4;
        StageManager.clearEvent -= Achivement_5;
        StageManager.clearEvent -= Achivement_6;
        StageManager.clearEvent -= Achivement_7;
        StageManager.clearEvent -= Achivement_8;
        StageManager.clearEvent -= Achivement_9;
        StageManager.clearEvent -= Achivement_10;
    }

    public void OnShowAchievement()
        => Social.ShowAchievementsUI();

    public static void AchivementClear(bool success)
     => GameManager.Data.AchivementClear = success ? 1 : 0;

    /// <summary>
    /// Can go heeaven (처음 클리어)
    /// </summary>
    public static void CanGoHeaven()
        =>  Social.ReportProgress(GPGSIds.achievement_can_go_heaven, 100f, AchivementClear);

    /// <summary>
    /// 반짝반짝? (황금몬스터 최초처치)
    /// </summary>
    public static void Achivement_1()
        => Social.ReportProgress(GPGSIds.achievement, 100f, AchivementClear);

    /// <summary>
    /// 번쩍번쩍! (황금몬스터 3 처치 후 클리어)
    /// </summary>
    public static void Achivement_2()
    {
        if(StageManager.data.GoldMonsterKill >= 3)
            Social.ReportProgress(GPGSIds.achievement_2, 100f, AchivementClear);
    }

    /// <summary>
    /// 빈 캔은 분리수거 (100회 게임오버) 
    /// </summary>
    /// <param name="gameOverCount"></param>
    public static void Achivement_3(int gameOverCount)
        => Social.ReportProgress(GPGSIds.achievement_3, gameOverCount, AchivementClear);

    /// <summary>
    /// 지름신 (아이템 8개 획득 후 클리어)
    /// </summary>
    public static void Achivement_4()
    { 
        if(StageManager.data.GottenItemCount >= 8)
            Social.ReportProgress(GPGSIds.achievement_4, 100f, AchivementClear);
    }

    /// <summary>
    /// 정화작업 (모든 몬스터 처치 후 클리어)
    /// </summary>
    public static void Achivement_5()
    { 
        if(StageManager.data.MonsterKill >= StageManager.Instance.monsterCount)
        Social.ReportProgress(GPGSIds.achievement_5, 100f, AchivementClear);
    }

    /// <summary>
    /// 불매운동 (상점 아이템 구매 하지 않고 클리어)
    /// </summary>
    public static void Achivement_6()
    { 
        if(!StageManager.data.isBuyInShop)
            Social.ReportProgress(GPGSIds.achievement_6, 100f, AchivementClear);
    }

    /// <summary>
    /// 리필은 셀프 (음료 리필 1회 이하 후 클리어)
    /// </summary>
    public static void Achivement_7()
    { 
        if(StageManager.data.RefillCount <= 1)
            Social.ReportProgress(GPGSIds.achievement_7, 100f, AchivementClear);
    }

    /// <summary>
    /// 절약정신 (음료 리필하지 않고 클리어)
    /// </summary>
    public static void Achivement_8()
    { 
        if(StageManager.data.RefillCount == 0)
            
Social.ReportProgress(GPGSIds.achievement_8, 100f, AchivementClear);
    }

    /// <summary>
    /// 단수 (음료 소비하지 않고 클리어)
    /// </summary>
    public static void Achivement_9()
    { 
        if(StageManager.data.RefillCount == 0 
            && StageManager.Instance.Stat.watergun.Model.WaterAmount 
            == StageManager.Instance.Stat.watergun.Model.MaxWaterAmount)
            Social.ReportProgress(GPGSIds.achievement_9, 100f, AchivementClear);
    }

    /// <summary>
    /// 평화 주의자 (몬스터 처치수 0으로 클리어)
    /// </summary>
    public static void Achivement_10()
    { 
        if(!StageManager.data.isKillMonster)
            Social.ReportProgress(GPGSIds.achievement_10, 100f, AchivementClear);
    }

    /// <summary>
    /// Can master HEAVEN!!! (모든 업적 달성)
    /// </summary>
    public static void CanMasterHEAVEN()
        => Social.ReportProgress(GPGSIds.achievement_can_master_heaven, 100f, AchivementClear);

}
