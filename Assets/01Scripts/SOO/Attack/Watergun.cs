using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watergun : MonoBehaviour
{
    private WaterGunModel model;

    private Vector2 Angle => SOO.Util.AngleToVector(transform.rotation.eulerAngles.z);

    private void Awake()
    {
        model = new WaterGunModel(100);
    }

    private void Update()
    {
        if (GameManager.Instance.input.behaviourActive)
        {
            ShootWatergun();
        }
        else
        {
            if (null != model.NowWater)
                model.NowWater = null;
        }
    }

    public void ShootWatergun()
    {
        if (model.WaterAmount > 0)
        {
            WaterSet();
        }
    }

    private void WaterSet()
    {
        if (model.NowWater == null)
        {
            GameObject obj = ObjectPoolManager.Inst.pool.Pop();
            model.NowWater = obj.GetComponent<Water>();
            model.NowWater.SetFirst(model.Damage);
        }

        if (model.NowWater.VertexSet(transform.position, Angle, TimeSet()))
           model.WaterAmount -= 1;
    }

    private float TimeSet()
    {
        if (model.MaxWaterAmount / 100 * 20 > model.WaterAmount)
        {
            return 0.3f;
        }
        else if (model.MaxWaterAmount / 100 * 50 > model.WaterAmount)
        {
            return 0.5f;
        }
        else if(model.MaxWaterAmount / 100 * 50 <= model.WaterAmount)
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