using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnMonsterGradeType
{
    E, D, C, B, A, S, Elite
}

public class MonsterSpawnPoint : MonoBehaviour
{
    [SerializeField] SpawnMonsterGradeType spawnMonsterGradeType;
    MonsterManager monsterManager;
    GameObject[] monsterList;
    [SerializeField, Range(0.0f, 100.0f), Tooltip("Ȳ�� ���Ͱ� ������ �����")] float aChanceOfSpawningGoldenMonster = 2.5f;
    [SerializeField, Tooltip("������")] GameObject monsterToCreat;

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        if (_collision.CompareTag("Player"))
        {
            if (monsterToCreat == null)
            {
                //Debug.LogError($"({gameObject.transform.position}) {gameObject.name}: ������ ���Ͱ� �����ϴ�!");
                return;
            }
            SpawnMonster();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        monsterManager = MonsterManager.instance;
        StartCoroutine(FindArray());
    }

    WaitForSeconds wait2Sec = new WaitForSeconds(2f);

    IEnumerator FindArray() //!!!!dictionary Ȱ���ؼ� �̸� �ٸ� �ڷ��� ������ ���� �� ����?!!!!
    {
        yield return wait2Sec;
        switch ((int)spawnMonsterGradeType)
        {
            case 0: monsterList = monsterManager.EGradeMonster;
                break; 
            case 1: monsterList = monsterManager.DGradeMonster;
                break;
            case 2: monsterList = monsterManager.CGradeMonster;
                break;
            case 3: monsterList = monsterManager.BGradeMonster;
                break;
            case 4: monsterList = monsterManager.AGradeMonster;
                break;
            case 5: monsterList = monsterManager.SGradeMonster;
                break;
            case 6: monsterList = monsterManager.EliteMonster;
                break;
            default: Debug.LogError($"{gameObject.name}: Not Implemented!");
                break;
        }
        int index = Random.Range(0, monsterList.Length);
        //Debug.Log($"index is {index}");
        if (monsterList[index] == null)
        {
            Debug.LogError("���͸� ã�� ����");
            yield break;
        }
        monsterToCreat = monsterList[index];
    }

    void SpawnMonster()
    {
        if (monsterToCreat == null)
        {
            Debug.LogError($"{gameObject.name}: ��ȯ��ų ���Ͱ� ����");
            return;
        }
        GameObject spawnedMonster = Instantiate(monsterToCreat);
        spawnedMonster.transform.position = transform.position;
        bool isGoldenMonster = ProbabilityOfBecomingGoldenMonster();
        if (isGoldenMonster)
        {
            GameObject particle = Instantiate(monsterManager.goldenMonsterParticle, spawnedMonster.transform);
            ParticleSystem particleComp = particle.GetComponent<ParticleSystem>();
            particleComp.gravityModifier = spawnedMonster.GetComponent<SpriteRenderer>().flipY ? -1 * particleComp.gravityModifier : particleComp.gravityModifier;
            spawnedMonster.GetComponent<HMonster>().isGoldenMonster = true;
        }
        Destroy(gameObject);
    }

    bool ProbabilityOfBecomingGoldenMonster() //100 / ����� = �ִ� ����?
    {
        bool isGolden = false;
        int maxRange = 10000;
        int pickedValue = Random.Range(0, maxRange);
        int chance = (int)(((System.Math.Truncate(aChanceOfSpawningGoldenMonster * 100f) * 0.01f) * 100f) + 1);
        if (pickedValue < chance)
        {
            isGolden = true;
            //Debug.Log($"���� Ȯ����{chance} / {maxRange}�̾����� ���� ����{pickedValue} < {chance}�̹Ƿ� Ȳ�� ���Ͱ� �Ǿ����ϴ�");
        }
        else
        {
            //Debug.Log($"���� Ȯ����{chance} / {maxRange}�̾����� ���� ����{pickedValue} >= {chance}�̹Ƿ� Ȳ�� ���Ͱ� ���� ���Ͽ����ϴ�");
        }
        return isGolden;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SpawnMonster();
        }
    }
}
