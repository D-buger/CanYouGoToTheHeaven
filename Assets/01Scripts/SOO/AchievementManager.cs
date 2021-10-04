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
    /// Can go heeaven (ó�� Ŭ����)
    /// </summary>
    public static void CanGoHeaven()
        =>  Social.ReportProgress(GPGSIds.achievement_can_go_heaven, 100f, AchivementClear);

    /// <summary>
    /// ��¦��¦? (Ȳ�ݸ��� ����óġ)
    /// </summary>
    public static void Achivement_1()
        => Social.ReportProgress(GPGSIds.achievement, 100f, AchivementClear);

    /// <summary>
    /// ��½��½! (Ȳ�ݸ��� 3 óġ �� Ŭ����)
    /// </summary>
    public static void Achivement_2()
    {
        if(StageManager.data.GoldMonsterKill >= 3)
            Social.ReportProgress(GPGSIds.achievement_2, 100f, AchivementClear);
    }

    /// <summary>
    /// �� ĵ�� �и����� (100ȸ ���ӿ���) 
    /// </summary>
    /// <param name="gameOverCount"></param>
    public static void Achivement_3(int gameOverCount)
        => Social.ReportProgress(GPGSIds.achievement_3, gameOverCount, AchivementClear);

    /// <summary>
    /// ������ (������ 8�� ȹ�� �� Ŭ����)
    /// </summary>
    public static void Achivement_4()
    { 
        if(StageManager.data.GottenItemCount >= 8)
            Social.ReportProgress(GPGSIds.achievement_4, 100f, AchivementClear);
    }

    /// <summary>
    /// ��ȭ�۾� (��� ���� óġ �� Ŭ����)
    /// </summary>
    public static void Achivement_5()
    { 
        if(StageManager.data.MonsterKill >= StageManager.Instance.monsterCount)
        Social.ReportProgress(GPGSIds.achievement_5, 100f, AchivementClear);
    }

    /// <summary>
    /// �Ҹſ (���� ������ ���� ���� �ʰ� Ŭ����)
    /// </summary>
    public static void Achivement_6()
    { 
        if(!StageManager.data.isBuyInShop)
            Social.ReportProgress(GPGSIds.achievement_6, 100f, AchivementClear);
    }

    /// <summary>
    /// ������ ���� (���� ���� 1ȸ ���� �� Ŭ����)
    /// </summary>
    public static void Achivement_7()
    { 
        if(StageManager.data.RefillCount <= 1)
            Social.ReportProgress(GPGSIds.achievement_7, 100f, AchivementClear);
    }

    /// <summary>
    /// �������� (���� �������� �ʰ� Ŭ����)
    /// </summary>
    public static void Achivement_8()
    { 
        if(StageManager.data.RefillCount == 0)
            
Social.ReportProgress(GPGSIds.achievement_8, 100f, AchivementClear);
    }

    /// <summary>
    /// �ܼ� (���� �Һ����� �ʰ� Ŭ����)
    /// </summary>
    public static void Achivement_9()
    { 
        if(StageManager.data.RefillCount == 0 
            && StageManager.Instance.Stat.watergun.Model.WaterAmount 
            == StageManager.Instance.Stat.watergun.Model.MaxWaterAmount)
            Social.ReportProgress(GPGSIds.achievement_9, 100f, AchivementClear);
    }

    /// <summary>
    /// ��ȭ ������ (���� óġ�� 0���� Ŭ����)
    /// </summary>
    public static void Achivement_10()
    { 
        if(!StageManager.data.isKillMonster)
            Social.ReportProgress(GPGSIds.achievement_10, 100f, AchivementClear);
    }

    /// <summary>
    /// Can master HEAVEN!!! (��� ���� �޼�)
    /// </summary>
    public static void CanMasterHEAVEN()
        => Social.ReportProgress(GPGSIds.achievement_can_master_heaven, 100f, AchivementClear);

}
