using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerStats
{
    public static event Action<int, int> maxHpCallback;
    private int maxHp;
    public int MaxHp
    {
        get => maxHp;
        set
        {
            value = value % 2 == 0 ? value : ++value;
            maxHp = Mathf.Max(maxHp, value);
            maxHpCallback?.Invoke(maxHp, currentHp);
        }
    }

    public static event Action<int, int> currentHpCallback;
    private int currentHp;
    public int CurrentHp
    {
        get => currentHp;
        set
        {
            currentHp = Mathf.Clamp(value, 0, maxHp);
            currentHpCallback?.Invoke(maxHp, currentHp);
        }
    }

    public static event Action<int> soulCallback;
    private int soul;
    public int Soul
    {
        get => soul;
        set
        {
            soul = Mathf.Max(0, value);
            soulCallback?.Invoke(soul);
        }
    }

    public Watergun watergun;

    public PhysicsStats physicsStat;

    //Monobehaviour가 달려있지 않아서인지 Reset에서 초기화하면 할당되지않는 오류가 발생한다.
    public void SetFirst(Transform _trans, Watergun _watergun)
    {
        physicsStat.Set(_trans);
        watergun = _watergun;
        MaxHp = 6;
        CurrentHp = MaxHp;
        Soul = 0;
    }
}
