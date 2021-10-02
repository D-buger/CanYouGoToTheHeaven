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
            currentHitPoint -= StageManager.Instance.Stat.watergun.Model.Damage;

            if (currentHitPoint <= 0)
            {
                ActionAfterDeath();
            }
        }
    }

    protected virtual void SettingData()
    {
        if (monsterName == "")
        {
            Debug.LogWarning($"{gameObject.name}: 이름이 지정되지 않았습니다!");
            return;
        }
        SettingVariables();
    }

    protected virtual void SettingVariables()
    {
        Debug.LogWarning("변경할 내용 있음");
        int currentStage = 3; //직접 스테이지 매니저에서 가져올것

        if (currentStage <= 3) //3스테이지면
        {
            contactDamage = 1;
        }
        else
        {
            contactDamage = 2;
        }

        damagedTime = 0f;

        currentHitPoint = StringToInteger(GetDataWithVariableName("HitPoint"));
    }

    protected string GetDataWithVariableName(string _variableName) //변수명을 넣으면 해당 몬스터의 해당 변수명의 값을 제공
    {
        if (monsterManager.GetDataWithMonsterName(monsterName) == null)
        {
            Debug.LogWarning("이런 몬스터는 없는데요");
            return null;
        }
        string ddd = monsterManager.GetDataWithMonsterName(monsterName)[_variableName];
        Debug.Log($"{_variableName}: {ddd}");
        return ddd;
    }

    protected void OperateOnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            Player player = _collision.gameObject.GetComponent<Player>();
            player.stats.CurrentHp -= contactDamage;

            MonsterPoolManager.instance.ReturnObject(gameObject);
        }
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

    public void OperateOnEnable()
    {
        SettingData();
    }

    protected virtual void OperateStart()
    {
        animator = GetComponent<Animator>();
        monsterManager = MonsterManager.instance;
        SettingData();
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

    void CheckDistanceFromPlayer() //플레이어가 본인과 멀어지면 디스폰 시키는 역할
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
        //Debug.Log($"{gameObject.name}: 유도 발사체 발사!");
    }

    protected void ShotProjectile(GameObject _projectile, int _damage, int _projectileCount, float _velocity, float _totalAngle)
    {
        //Debug.Log($"{gameObject.name}: 발사체 발사!");
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
}
