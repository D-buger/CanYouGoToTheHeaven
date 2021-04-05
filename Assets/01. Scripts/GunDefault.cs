using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunDefault : MonoBehaviour
{
    const int bulletPool = 6;

    ObjectPool pool;

    Sprite gunImg;
    Sprite bulletImg;

    //�Ѿ� �ӵ�
    float bulletSpeed;
    //�Ѿ� �ӵ��� �������� ��������
    bool randomBulletSpeed = false;

    //�Ѿ˰� �Ѿ� ������ ����
    float delay;
    //�Ѿ��� �� ������ ���ε� �ð�
    float reloadingTime;
    //�ݵ�
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