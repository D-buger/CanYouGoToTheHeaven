using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HHH_Character : MonoBehaviour
{
    [SerializeField, Range(1, 127)] public int currentHitPoint; //정식에는 maxHitPoint랑 나누면 좋을 것

    public abstract void InflictDamage(); //Decrease hitPoint Method
    protected virtual void KiilCharacter()
    {
        Destroy(gameObject);
    }
}
