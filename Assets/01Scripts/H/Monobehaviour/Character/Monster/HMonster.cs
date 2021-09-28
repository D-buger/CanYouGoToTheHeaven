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
        Debug.LogWarning("해당 부분을 현재 스테이지에 따라서 바꿔야함");
        if (true /*만약 현재 스테이지가 <= 3이라면*/) //조건 바꿀것
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
            Debug.LogWarning($"{gameObject.name}: 정수형 변환에 실패하였습니다!");
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

    public void DamageCharacter()
    {
        damagedTime += Time.deltaTime;
        if (damagedTime >= 1)
        {
            damagedTime -= 1;
            Debug.LogWarning($"{gameObject.name}: HMonster스크립트에 변경할 내용이 존재합니다");
            hitPoint -= 3; //player의 damage가 담긴 스크립트에서 참조해야함. 또는 매개변수로 damage를 받아야함
            if (hitPoint <= 0)
            {
                ActionAfterDeath(); //체력이 0이 될 시 행동
            }
        }
    }

    protected void ActionAfterDeath()
    {
        if (isGoldenMonster) //만약 황금몬스터였을 경우
        {
            GameObject goldenPortal = Instantiate(MonsterManager.instance.TreasureRoomPortalPrefab, null);
            goldenPortal.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        Destroy(gameObject);
    }

    //충돌 데미지
    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) //플레이어와 몬스터의 충돌을 감지하는 역할. 플레이어 스크립트 내로 옮기는게 좋음.
        {
            StageManager.Instance.Stat.CurrentHP -= contactDamage;
            Destroy(gameObject);
        }
    }
}
