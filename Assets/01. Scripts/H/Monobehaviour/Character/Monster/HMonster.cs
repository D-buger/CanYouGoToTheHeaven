using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMonster : MonoBehaviour
{
    [SerializeField] protected int hitPoint;
    protected float damagedTime;
    protected GameObject player;
    protected Animator animator;

    protected virtual void OperateStart()
    {
        player = GameObject.FindWithTag("Player");
    }

    protected void ShotHomingProjectile()
    {
        Debug.Log($"{gameObject.name}: 유도 발사체 발사!");
    }

    protected void ShotProjectile(GameObject _projectile, int _damage, int _projectileCount, float _velocity, float _totalAngle)
    {
        Debug.Log($"{gameObject.name}: 발사체 발사!");
        /*Vector3 pointP = player.transform.position;
        float radius = Vector2.Distance(player.transform.position, transform.position);
        float lookToward = Mathf.Atan2(pointP.y, pointP.x) * Mathf.Rad2Deg;
        float shotBtwAngle = _totalAngle / _projectileCount;
        float currentAngle;
        if (_projectileCount % 2 == 0)
        {
            currentAngle = -90f - ((shotBtwAngle * 0.5f) + ((_projectileCount - 2) * _projectileCount * 0.5f));
            pointP = new Vector3(radius * -Mathf.Sin(((shotBtwAngle * 0.5f) + ((_projectileCount - 2) * _projectileCount * 0.5f))), radius * -Mathf.Cos(((shotBtwAngle * 0.5f) + ((_projectileCount - 2) * _projectileCount * 0.5f))));
            for (int i = 0; i < _projectileCount; i++)
            {
                Debug.Log(pointP);
                InstantiateProjectile(_projectile, _damage, (pointP + transform.position).normalized * _velocity, currentAngle);
                pointP.x = radius * Mathf.Cos((shotBtwAngle * 0.5f) + ((_projectileCount - 2) * _projectileCount * 0.5f));
                pointP.y = radius * Mathf.Sin((shotBtwAngle * 0.5f) + ((_projectileCount - 2) * _projectileCount * 0.5f));
                currentAngle += shotBtwAngle;
            }
        }
        else
        {
            currentAngle = -90f - (shotBtwAngle * _projectileCount * 0.5f);
            for (int i = 0; i < _projectileCount; i++)
            {
                InstantiateProjectile(_projectile, _damage, pointP.normalized * _velocity, currentAngle);
                currentAngle += shotBtwAngle;
            }
            return;
        }*/
    }

    void InstantiateProjectile(GameObject _projectile, int _damage, Vector2 _velocity, float _rotZ)
    {
        GameObject pj = Instantiate(_projectile);
        pj.transform.position = transform.position;
        pj.transform.rotation = Quaternion.Euler(0, 0, _rotZ);
    }

    protected Vector2 FindDirectionVector(Vector3 _destination)
    {
        Vector3 dir = _destination - transform.position;
        dir *= 2000f;
        return dir.normalized;
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
