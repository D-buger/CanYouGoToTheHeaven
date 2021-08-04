using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunDefault : MonoBehaviour
{
    const int bulletPool = 6;

    ObjectPool pool;

    Sprite gunImg;
    Sprite bulletImg;

    //총알 속도
    float bulletSpeed;
    //총알 속도를 랜덤으로 설정할지
    bool randomBulletSpeed = false;

    //총알과 총알 사이의 간격
    float delay;
    //총알을 다 썼을때 리로딩 시간
    float reloadingTime;
    //반동
    float rebound;


    int firedBullet;
    
    public int bulletAmount;

    GameObject[] activeBullet;

    protected virtual void Awake()
    {
        pool = ObjectPoolManager.Inst.pool;
        activeBullet = new GameObject[bulletPool];
    }

    public abstract void Shot();

    protected virtual void Reloading()
    {

    }
}