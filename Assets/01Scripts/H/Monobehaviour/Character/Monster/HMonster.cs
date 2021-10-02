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
            Debug.LogWarning($"{gameObject.name}: HMonster��ũ��Ʈ�� ������ ������ �����մϴ�");
            currentHitPoint -= 3; //player�� damage�� ��� ��ũ��Ʈ���� �����ؾ���. �Ǵ� �Ű������� damage�� �޾ƾ���

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
            Debug.LogWarning($"{gameObject.name}: ������ ��ȯ�� �����Ͽ����ϴ�!");
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
        if (player.transform.position.y - transform.position.y >= 25) //�÷��̾��� y��ǥ�� �������κ��� 25�̻� �ö󰡸�
        {
            DespawnMonster(); //Ǯ�� ��ȯ��Ŵ
        }
    }

    public void MakeGoldenMonster()
    {
        isGoldenMonster = true;
    }

    protected WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    protected void ShotHomingProjectile(GameObject _projectile, int _damage, float _velocity)
    {
        Debug.Log($"{gameObject.name}: ���� �߻�ü �߻�!");
    }

    protected void ShotProjectile(GameObject _projectile, int _damage, int _projectileCount, float _velocity, float _totalAngle)
    {
        Debug.Log($"{gameObject.name}: �߻�ü �߻�!");
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
            if (_projectileCount % 2 != 0) //Ȧ��
            {
                currentRotZ = angle - (shotBtwAngle * (_projectileCount - 1) * 0.5f);
                for (int i = 0; i < _projectileCount; i++)
                {
                    InstantiateProjectile(_projectile, _damage, currentRotZ, _velocity);
                    currentRotZ += shotBtwAngle;
                }
            }
            else //¦��, �ʿ����
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
        if (isGoldenMonster) //���� Ȳ�ݸ��Ϳ��� ���
        {
            GameObject goldenPortal = MonsterPoolManager.instance.GetObject("TreasureRoomPortal");
            goldenPortal.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
        MonsterPoolManager.instance.ReturnObject(gameObject);
    }

    protected void OperateOnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) //�÷��̾�� ������ �浹�� �����ϴ� ����. �÷��̾� ��ũ��Ʈ ���� �ű�°� ����.
        {
            StageManager.Instance.Stat.CurrentHP -= contactDamage;
            Destroy(gameObject);
        }
    }
}
