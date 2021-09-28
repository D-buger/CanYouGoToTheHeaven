using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watergun : MonoBehaviour
{
    public WaterGunModel Model { get; private set; }

    private Vector2 Angle => SOO.Util.AngleToVector(transform.rotation.eulerAngles.z);

    private List<Dictionary<string, float>> waterInfo
        = new List<Dictionary<string, float>>();

    private ParticleSystem spreadWater;
    
    private void Awake()
    {
        spreadWater = transform.GetChild(0).GetComponent<ParticleSystem>();
        spreadWater.Play();

        var element = new Dictionary<string, float>();
        foreach (var info in CSVReader.Read("Water"))
        {
            element = new Dictionary<string, float>();
            foreach (var infoDicti in info)
            {
                element[infoDicti.Key] = float.Parse(infoDicti.Value);
            }
            waterInfo.Add(element);
        }
    }

    private void Start()
    {
        Model = new WaterGunModel(500);
    }

    public void Update()
    {
        if (GameManager.Instance.input.AttackActive)
        {
            if (Model.WaterAmount > 0)
            {
                spreadWater.enableEmission = true;
                WaterSet();
            }
        }
        else
        {
            spreadWater.enableEmission = false;
            if (null != Model.NowWater)
                Model.NowWater = null;
        }
    }

    private void WaterSet()
    {
        if (Model.NowWater == null)
        {
            GameObject obj = ObjectPoolManager.Inst.pool.Pop();
            Model.NowWater = obj.GetComponent<Water>();
            Model.NowWater.SetFirst(Model.Damage);
        }

        if (Model.NowWater.VertexSet(transform.position, Angle, TimeSet()))
           Model.WaterAmount -= 1;
    }

    private float TimeSet()
    {
        foreach (var value in waterInfo)
        {
            if (value["MinPercent"] < Model.WaterAmount &&
                Model.WaterAmount >= value["MaxPercent"])
                return value["Length"] + 3;
        }
        Debug.LogError("waterAmount out of values in Water.csv");
        return -1;
    }
}