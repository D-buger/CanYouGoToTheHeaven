using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMonster : MonoBehaviour
{
    public enum Grade
    {
        D, C, B, A, S, SS, SSS
    }

    [SerializeField] protected string monsterName;
    [SerializeField] protected Grade rank;
    [SerializeField] protected int hitPoint;
    protected float damagedTime;
    protected GameObject player;
    protected Animator animator;
    public bool isGoldenMonster = false;
    MonsterManager monsterManager;
    protected WaitForSeconds waitFor1Seconds = new WaitForSeconds(1f);

    protected int damage = 1;
    protected int contactDamage;

    protected virtual void SettingData()
    {
        hitPoint = StringToInteger(monsterManager.GetDataWithName(gameObject.name)["HitPoint"]);
        Debug.LogWarning("�ش� �κ��� ���� ���������� ���� �ٲ����");
        if (true /*���� ���� ���������� <= 3�̶��*/) //���� �ٲܰ�
        {
            damage = 1;
        }
        else
        {
            damage = 2;
        }
        contactDamage = 2;
        damagedTime = 0f;
    }

    public void MakeGoldenMonster()
    {
        isGoldenMonster = true;
    }

    protected int StringToInteger(string _string)
    {
        if(int.TryParse(_string, out int _int))
        {
            return _int;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: ������ ��ȯ�� �����Ͽ����ϴ�!");
            return 0;
        }
    }

    protected virtual void OperateAwake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void OperateStart()
    {
        SettingData();
        monsterManager = MonsterManager.instance;
        player = monsterManager.player;
    }

    protected WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    protected IEnumerator AdjustSpriteColor(Color _targetColor, float totalDuration)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color SpriteColor = sprite.color;
        float[] colorPerSecond = new float[4];
        for (int i = 0; i < 4; i++)
        {
            float colorPerSecondValue = 0f;
            colorPerSecondValue = (_targetColor[i] - SpriteColor[i]) / totalDuration;
            colorPerSecond[i] = colorPerSecondValue;
        }
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                SpriteColor[i] += colorPerSecond[i] * Time.deltaTime;
            }
            yield return waitForEndOfFrame;
        }
    }

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

    public void DamageCharacter()
    {
        damagedTime += Time.deltaTime;
        if (damagedTime >= 1)
        {
            damagedTime -= 1;
            Debug.LogWarning($"{gameObject.name}: HMonster��ũ��Ʈ�� ������ ������ �����մϴ�");
            hitPoint -= 3; //player�� damage�� ��� ��ũ��Ʈ���� �����ؾ���. �Ǵ� �Ű������� damage�� �޾ƾ���
            if (hitPoint <= 0)
            {
                ActionAfterDeath(); //ü���� 0�� �� �� �ൿ
            }
        }
    }

    protected void ActionAfterDeath()
    {
        if (isGoldenMonster) //���� Ȳ�ݸ��Ϳ��� ���
        {
            GameObject goldenPortal = Instantiate(MonsterManager.instance.TreasureRoomPortalPrefab, null);
            goldenPortal.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        Destroy(gameObject);
    }

    //�浹 ������
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) //�÷��̾�� ������ �浹�� �����ϴ� ����. �÷��̾� ��ũ��Ʈ ���� �ű�°� ����.
        {
            StageManager.Instance.Stat.CurrentHP -= contactDamage;
            Destroy(gameObject);
        }
    }
}
