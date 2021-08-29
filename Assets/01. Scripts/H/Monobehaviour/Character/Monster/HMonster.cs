using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMonster : MonoBehaviour
{
    [SerializeField] protected int hitPoint;
    [SerializeField] protected float damagedTime;
    protected GameObject player;

    protected virtual void OperateStart()
    {
        player = GameObject.FindWithTag("Player");
    }

    protected void ShotProjectile(GameObject _projectile, int _damage, int _projectileCount, float _velocity, float _totalAngle)
    {
        Vector2 center = player.transform.position - transform.position;
        if (_projectileCount > 1)
        {
            float shotBtwAngle = _totalAngle / _projectileCount;
            float currentAngle;
            for (int i = 0; i < _projectileCount; i++)
            {
                
            }
            return;
        }
        InstantiateProjectile(_projectile, _damage, center.normalized * _velocity);
    }

    void InstantiateProjectile(GameObject _projectile, int _damage, Vector2 _velocity)
    {
        GameObject pj = Instantiate(_projectile);
        pj.transform.position = transform.position;
        pj.GetComponent<MonsterProjectile>().Initialize(_damage, _velocity);
    }

    public void DamageCharacter()
    {
        damagedTime += Time.deltaTime;
        if (damagedTime >= 1)
        {
            damagedTime -= 1;
            Debug.LogWarning($"{gameObject.name}: HMonster스크립트에 변경할 내용이 존재합니다");
            hitPoint -= 3; //player의 damage가 담긴 스크립트에서 참조해야함
        }
    }
}
