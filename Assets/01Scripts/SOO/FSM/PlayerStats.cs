using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int MaxHP = 3;
    [SerializeField]
    private int currentHP;
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = Mathf.Min(value, MaxHP);
            currentHP = Mathf.Max(0, currentHP);
        }
    }

    public int soul;

    public Watergun watergun;

    public PhysicsStats physicsStat;

    public void Set(Transform _trans, Watergun watergun)
    {
        physicsStat.Set(_trans);
        currentHP = MaxHP;
        this.watergun = watergun;
    }
}
