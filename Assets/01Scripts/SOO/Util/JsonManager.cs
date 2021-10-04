using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public static class JsonManager
{
    public static void Save(Json_GameData toSaveData)
    {
        JsonMapper.RegisterImporter<int, float>((int value) => (float)value);

        string data = JsonMapper.ToJson(toSaveData);

        File.WriteAllText(SOO.Util.StringBuilder(
            Application.dataPath, "/Resources/Data.json"), data);
    }

    public static Json_GameData Load()
    {
        string json =  
            File.ReadAllText(SOO.Util.StringBuilder(
            Application.dataPath, "/Resources/Data.json"));
        JsonData data = JsonMapper.ToObject(json);

        Json_GameData achieveData =  new Json_GameData();
        achieveData.monsterKill = int.Parse(data["monsterKill"].ToString());
        achieveData.achivementClear = int.Parse(data["achivementClear"].ToString());
        achieveData.clearCount = int.Parse(data["clearCount"].ToString());
        achieveData.gameOverCount = int.Parse(data["gameOverCount"].ToString());
        achieveData.goldMonsterKill = int.Parse(data["goldMonsterKill"].ToString());
        achieveData.gottenItemCount = int.Parse(data["gottenItemCount"].ToString());
        achieveData.refillCount = int.Parse(data["refillCount"].ToString());
        achieveData.shortestTime = double.Parse(data["shortestTime"].ToString());

        return achieveData;
    }


}
