using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watergun : MonoBehaviour
{
    public WaterGunModel Model { get; private set; }

    private Vector2 Angle => SOO.Util.AngleToVector(transform.rotation.eulerAngles.z);

    private void Awake()
    {
        Model = new WaterGunModel(100);
    }

    private void Update()
    {
        if (GameManager.Instance.input.AttackActive)
        {
            ShootWatergun();
        }
        else
        {
            if (null != Model.NowWater)
                Model.NowWater = null;
        }
    }

    public void ShootWatergun()
    {
        if (Model.WaterAmount > 0)
        {
            WaterSet();
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
        if (Model.MaxWaterAmount / 100 * 20 > Model.WaterAmount)
        {
            return 0.3f;
        }
        else if (Model.MaxWaterAmount / 100 * 50 > Model.WaterAmount)
        {
            return 0.5f;
        }
        else if(Model.MaxWaterAmount / 100 * 50 <= Model.WaterAmount)
        {
            return 1f;
        }
        else
        {
            Debug.Log("해당 되지 않는 양");
            return 0;
        }
    }
}