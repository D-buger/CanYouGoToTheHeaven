using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMonster : MonoBehaviour
{
    [SerializeField] protected string monsterName;
    [SerializeField] protected int currentHitPoint;
    protected float damagedTime;
    protected int contactDamage = 2;
    protected bool isGoldenMonster = false;

    protected GameObject player;
    protected Animator animator;
    MonsterManager monsterManager;
    protected WaitForSeconds waitFor1Seconds = new WaitForSeconds(1f);

    public virtual void InflictDamage()
    {
        damagedTime += Time.deltaTime;
        if (damagedTime >= 1)
        {
            damagedTime -= 1;
            Debug.LogWarning($"{gameObject.name}: HMonster스크립트에 변경할 내용이 존재합니다");
            currentHitPoint -= 3; //player의 damage가 담긴 스크립트에서 참조해야함. 또는 매개변수로 damage를 받아야함

            if (currentHitPoint <= 0)
            {
                ActionAfterDeath();
            }
        }
    }

    protected virtual void SettingData()
    {
        currentHitPoint = StringToInteger(GetDataWithVariableName("HitPoint"));
        damagedTime = 0f;
    }

    protected string GetDataWithVariableName(string _variableName)
    {
        return monsterManager.GetDataWithMonsterName(monsterName)[_variableName];
    }

    protected int StringToInteger(string _string)
    {
        if (int.TryParse(_string, out int _int))
        {
            return _int;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: 정수형 변환에 실패하였습니다!");
            return 0;
        }
    }

    protected virtual void OperateStart()
    {
        animator = GetComponent<Animator>();
        SettingData();
        monsterManager = MonsterManager.instance;
        player = monsterManager.player;
    }

    protected void OperateUpdate()
    {
        CheckDistanceFromPlayer();
    }

    void DespawnMonster()
    {
        MonsterPoolManager.instance.ReturnObject(gameObject);
    }

    void CheckDistanceFromPlayer()
    {
        if (player.transform.position.y - transform.position.y >= 25) //플레이어의 y좌표가 본인으로부터 25이상 올라가면
        {
            DespawnMonster(); //풀로 반환시킴
        }
    }

    public void MakeGoldenMonster()
    {
        isGoldenMonster = true;
    }

    protected WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    protected void ShotHomingProjectile(GameObject _projectile, int _damage, float _velocity)
    {
        Debug.Log($"{gameObject.name}: 유도 발사체 발사!");
    }

    protected void ShotProjectile(GameObject _projectile, int _damage, int _projectileCount, float _velocity, float _totalAngle)
    {
        Debug.Log($"{gameObject.name}: 발사체 발사!");
        Vector2 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float currentRotZ = angle;
        if (_projectileCount == 1)
        {
            InstantiateProjectile(_projectile, _damage, currentRotZ, _velocity);
        }
        else
        {
            float shotBtwAngle = _totalAngle / _projectileCount;
            if (_projectileCount % 2 != 0) //홀수
            {
                currentRotZ = angle - (shotBtwAngle * (_projectileCount - 1) * 0.5f);
                for (int i = 0; i < _projectileCount; i++)
                {
                    InstantiateProjectile(_projectile, _damage, currentRotZ, _velocity);
                    currentRotZ += shotBtwAngle;
                }
            }
            else //짝수, 필요없음
            {

            }
        }
    }

    void InstantiateProjectile(GameObject _projectile, int _damage, float _rotZ, float _velocity)
    {
        GameObject pj = Instantiate(_projectile);
        pj.transform.position = transform.position;
        pj.GetComponent<MonsterProjectile>().Initialize(_damage, _velocity, _rotZ);
    }

    protected Vector2 FindDirectionVector(Vector3 _destination)
    {
        Vector3 dir = _destination - transform.position;
        dir *= 2000f;
        return dir.normalized;
    }

    protected void ActionAfterDeath()
    {
        if (isGoldenMonster) //만약 황금몬스터였을 경우
        {
            GameObject goldenPortal = MonsterPoolManager.instance.GetObject("TreasureRoomPortal");
            goldenPortal.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
        MonsterPoolManager.instance.ReturnObject(gameObject);
    }

    protected void OperateOnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) //플레이어와 몬스터의 충돌을 감지하는 역할. 플레이어 스크립트 내로 옮기는게 좋음.
        {
            StageManager.Instance.Stat.CurrentHP -= contactDamage;
            Destroy(gameObject);
        }
    }
}
