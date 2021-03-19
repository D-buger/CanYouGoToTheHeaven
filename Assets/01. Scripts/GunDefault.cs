using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDefault : MonoBehaviour
{
    const int bulletPool = 6;

    ObjectPool pool;

    Sprite gunImg;
    Sprite bulletImg;

    float bulletSpeed;
    bool randomBulletSpeed = false;

    float delay;
    float reloadingTime;
    float rebound;

    int firedBullet;

    public int bulletAmount;

    private void Awake()
    {
        pool = ObjectPoolManager.Inst.pool;
    }

    public void Shot() { }
}
