using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HTest_Character : MonoBehaviour
{
    [SerializeField] protected float hitPoint;

    protected abstract void InflictDamage(float _damage);
}
