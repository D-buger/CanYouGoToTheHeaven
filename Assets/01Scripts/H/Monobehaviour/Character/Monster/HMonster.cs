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

    protected SpriteRenderer renderer;

    private IEnumerator colorChange;

    public virtual void InflictDamage()
    {
        if (colorChange != null)
            StopCoroutine(colorChange);

        colorChange = ColorChange();
        StartCoroutine(ColorChange());

        damagedTime += Time.deltaTime;
        if (damagedTime >= 0.3f)
        {
            damagedTime -= 0.3f;
            currentHitPoint -= StageManager.Instance.Stat.watergun.Model.Damage;

            if (currentHitPoint <= 0)
            {
                ActionAfterDeath();
            }
        }
    }

    public IEnumerator ColorChange()
    {
        if (renderer == null)
            renderer = GetComponent<SpriteRenderer>();
        renderer.color = Color.red;
        yield return waitFor1Seconds;
        renderer.color = Color.white;
        colorChange = null;
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
        int currentStage = StageManager.Instance.PlayerRoom;

        if (currentStage <= 9) //3스테이지 이하면
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

    protected string GetDataWithVariableName(string _variableName) //변수명을 넣으면 해당 몬스터의 해당 변수명의 값을 제공
    {
        if (monsterManager == null)
        {
            monsterManager = MonsterManager.instance;
        }
        string ddd = monsterManager.GetDataWithMonsterName(monsterName)[_variableName];
        if (ddd == null)
        {
            Debug.LogWarning($"{_variableName}은 존재하지 않는 변수명");
        }

        return ddd;
    }

    protected virtual void OperateOnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player"))
        {
            Player player = _collision.gameObject.GetComponent<Player>();
            player.stats.CurrentHp -= contactDamage;

            ActionAfterDeath();
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
            Debug.LogWarning($"{gameObject.name}: 실수형 변환에 실패하였습니다!");
            return 0;
        }
    }

    public virtual void OperateOnEnable()
    {
        SettingData();
        OperateStart();
    }

    protected virtual void OperateAwake()
    {
        animator = GetComponent<Animator>();
        monsterManager = MonsterManager.instance;
        player = monsterManager.player;
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected virtual void OperateStart()
    {

    }

    protected virtual void OperateUpdate()
    {
        CheckDistanceFromPlayer();
    }

    public void DespawnMonster()
    {
        MonsterPoolManager.instance.ReturnObject(gameObject, monsterName);
        if (isGoldenMonster)
        {
            isGoldenMonster = false;
            MonsterPoolManager.instance.ReturnObject(particle);
        }
    }

    protected void CheckDistanceFromPlayer() //플레이어가 본인과 멀어지면 디스폰 시키는 역할
    {
        if (MonsterManager.instance.player.transform.position.y - transform.position.y >= 15) //플레이어의 y좌표가 본인으로부터 15이상 올라가면
        {
            DespawnMonster(); //풀로 반환시킴
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
            if (_projectileCount % 2 != 0) //홀수
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
        if (isGoldenMonster) //만약 황금몬스터였을 경우
        {
            GameObject goldenPortal = MonsterPoolManager.instance.GetObject(MonsterManager.instance.TreasureRoomPortalPrefab.name);
            goldenPortal.transform.position = new Vector2(transform.position.x, transform.position.y - 0.4f);
            GameObject tempPlat = Instantiate(MonsterManager.instance.tempPlatform);
            tempPlat.transform.position = new Vector2(transform.position.x, transform.position.y + 0.45f);
            MonsterPoolManager.instance.ReturnObject(particle);
        }
        int dropAmount = Random.Range(1, StageManager.Instance.PlayerRoom / 3);
        for (int i = 0; i < dropAmount; i++)
        {
            GameObject droppedSoul = MonsterPoolManager.instance.GetObject("DroppedSoul");
            droppedSoul.transform.position = transform.position;
        }
        isGoldenMonster = false;
        MonsterPoolManager.instance.ReturnObject(gameObject, monsterName);
    }
}
