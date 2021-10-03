using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMonster : MonoBehaviour
{
    public string monsterName;
    [SerializeField] protected int currentHitPoint;
    protected float damagedTime;
    protected int contactDamage = 2;
    protected bool isGoldenMonster = false;

    protected GameObject player;
    protected Animator animator;
    MonsterManager monsterManager;
    protected WaitForSeconds waitFor1Seconds = new WaitForSeconds(1f);
    protected GameObject particle = null;

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
            Debug.LogWarning($"{gameObject.name}: �̸��� �������� �ʾҽ��ϴ�!");
            return;
        }
        SettingVariables();
    }

    protected virtual void SettingVariables()
    {
        int currentStage = StageManager.Instance.playerRoom;

        if (currentStage <= 3) //3�������� ���ϸ�
        {
            contactDamage = 1;
        }
        else
        {
            contactDamage = 2;
        }

        damagedTime = 0f;
        int hh = (int)StringToFloat(GetDataWithVariableName("HitPoint"));

        currentHitPoint = hh;
    }

    protected string GetDataWithVariableName(string _variableName) //�������� ������ �ش� ������ �ش� �������� ���� ����
    {
        if (monsterManager == null)
        {
            monsterManager = MonsterManager.instance;
        }
        string ddd = monsterManager.GetDataWithMonsterName(monsterName)[_variableName];
        if (ddd == null)
        {
            Debug.LogWarning($"{_variableName}�� �������� �ʴ� ������");
        }
        return ddd;
    }

    protected virtual void OperateOnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            Player player = _collision.gameObject.GetComponent<Player>();
            player.stats.CurrentHp -= contactDamage;

            MonsterPoolManager.instance.ReturnObject(gameObject, monsterName);
        }
    }

    protected float StringToFloat(string _string)
    {
        if (float.TryParse(_string, out float _float))
        {
            return _float;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: �Ǽ��� ��ȯ�� �����Ͽ����ϴ�!");
            return 0;
        }
    }

    public void OperateOnEnable()
    {
        SettingData();
        OperateStart();
    }

    protected virtual void OperateAwake()
    {
        animator = GetComponent<Animator>();
        monsterManager = MonsterManager.instance;
        player = monsterManager.player;
    }

    protected virtual void OperateStart()
    {

    }

    protected virtual void OperateUpdate()
    {
        CheckDistanceFromPlayer();
    }

    protected void DespawnMonster()
    {
        MonsterPoolManager.instance.ReturnObject(gameObject, monsterName);
        if (isGoldenMonster)
        {
            MonsterPoolManager.instance.ReturnObject(particle);
        }
    }

    protected void CheckDistanceFromPlayer() //�÷��̾ ���ΰ� �־����� ���� ��Ű�� ����
    {
        if (MonsterManager.instance.player.transform.position.y - transform.position.y >= 25) //�÷��̾��� y��ǥ�� �������κ��� 25�̻� �ö󰡸�
        {
            DespawnMonster(); //Ǯ�� ��ȯ��Ŵ
        }
    }

    public void MakeGoldenMonster()
    {
        isGoldenMonster = true;
        particle = MonsterPoolManager.instance.GetObject("GoldenMonsterParticle");
        particle.transform.position = transform.position;
        particle.transform.SetParent(transform);
        ParticleSystem particleComp = particle.GetComponent<ParticleSystem>();
        particleComp.Play();
    }

    protected WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    protected void ShotHomingProjectile(GameObject _projectile, int _damage, float _velocity, float _projectileCount, float _totalAngle, float _lifetime)
    {
        Vector2 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float currentRotZ = angle;
        if (_projectileCount == 1)
        {
            GameObject pj = MonsterPoolManager.instance.GetObject(_projectile.name);
            pj.transform.position = transform.position;
            pj.transform.rotation = Quaternion.Euler(0, 0, currentRotZ);
            pj.GetComponent<HomingMonsterProjectile>().StartingInitialize(_damage, _velocity, _lifetime);
        }
        else
        {
            float shotBtwAngle = _totalAngle / _projectileCount;
            if (_projectileCount % 2 != 0)
            {
                currentRotZ = angle - (shotBtwAngle * (_projectileCount - 1) * 0.5f);
                for (int i = 0; i < _projectileCount; i++)
                {
                    GameObject pj = MonsterPoolManager.instance.GetObject(_projectile.name);
                    pj.transform.position = transform.position;
                    pj.GetComponent<HomingMonsterProjectile>().StartingInitialize(_damage, _velocity, _lifetime);
                    pj.transform.rotation = Quaternion.Euler(0, 0, currentRotZ);
                    currentRotZ += shotBtwAngle;
                }
            }
        }
    }

    protected void ShotProjectile(GameObject _projectile, int _damage, int _projectileCount, float _velocity, float _totalAngle, float _lifetime)
    {
        Vector2 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float currentRotZ = angle;
        if (_projectileCount == 1)
        {
            InstantiateProjectile(_projectile, _damage, currentRotZ, _lifetime, _velocity);
        }
        else
        {
            float shotBtwAngle = _totalAngle / _projectileCount;
            if (_projectileCount % 2 != 0) //Ȧ��
            {
                currentRotZ = angle - (shotBtwAngle * (_projectileCount - 1) * 0.5f);
                for (int i = 0; i < _projectileCount; i++)
                {
                    InstantiateProjectile(_projectile, _damage, currentRotZ, _lifetime, _velocity);
                    currentRotZ += shotBtwAngle;
                }
            }
        }
    }

    void InstantiateProjectile(GameObject _projectile, int _damage, float _rotZ, float _lifetime, float _velocity)
    {
        GameObject pj = MonsterPoolManager.instance.GetObject(_projectile.name);
        pj.transform.position = transform.position;
        pj.GetComponent<MonsterProjectile>().Initialize(_damage, _velocity, _lifetime, _rotZ);
    }

    protected Vector2 FindDirectionVector(Vector3 _destination)
    {
        Vector3 dir = _destination - transform.position;
        dir *= 2000f;
        return dir.normalized;
    }

    protected void ActionAfterDeath()
    {
        if (isGoldenMonster) //���� Ȳ�ݸ��Ϳ��� ���
        {
            GameObject goldenPortal = MonsterPoolManager.instance.GetObject("TreasureRoomPortal");
            goldenPortal.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            MonsterPoolManager.instance.ReturnObject(particle);
        }
        GameObject droppedSoul = MonsterPoolManager.instance.GetObject("DroppedSoul");
        droppedSoul.transform.position = transform.position;
        MonsterPoolManager.instance.ReturnObject(gameObject, monsterName);
    }
}
