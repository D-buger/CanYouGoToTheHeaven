using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunDefault : MonoBehaviour
{
    const int bulletPool = 6;

    Sprite gunImg;
    Sprite bulletImg;

    float bulletSpeed;
    bool randomBulletSpeed = false;

    float delay;
    float reloadingTime;
    float rebound;

    int firedBullet;

    public int bulletAmount;
    

}
