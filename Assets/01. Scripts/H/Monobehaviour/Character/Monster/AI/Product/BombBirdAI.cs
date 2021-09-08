using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBirdAI : HMonster
{
    WaitForSeconds explosionDelayCount;

    [SerializeField] float explosionDelay;
    [SerializeField] float detectPlayerRange;
    [SerializeField] GameObject fragment;
    [SerializeField] int fragmentDamage;
    [SerializeField] float fragmentVelocity;

    [SerializeField] GameObject testPj;
    [SerializeField] float velo;

    CircleCollider2D circle;
    float radius;
    bool detectPlayer;

    // Start is called before the first frame update
    void Start()
    {
        OperateStart();
        explosionDelayCount = new WaitForSeconds(explosionDelay);
        circle = GetComponent<CircleCollider2D>();
        radius = circle.radius + 0.15f;
    }

    private void Update()
    {
        if (!detectPlayer)
        {
            //CheckPlayer();
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            ShotProjectile(testPj, 2, 1, velo, 0);
        }
    }

    void CheckPlayer() //circle의 radius만큼 지나서 시작: 본인에게 닿는 문제 해결
    {
        RaycastHit2D rightRay = Physics2D.Raycast(new Vector2(transform.position.x + radius, transform.position.y), Vector2.right, detectPlayerRange);
        RaycastHit2D leftRay = Physics2D.Raycast(new Vector2(transform.position.x + -radius, transform.position.y), Vector2.left, detectPlayerRange);
        Debug.DrawRay(new Vector2(transform.position.x + radius, transform.position.y), Vector2.right * detectPlayerRange, Color.magenta);
        Debug.DrawRay(new Vector2(transform.position.x + -radius, transform.position.y), Vector2.left * detectPlayerRange, Color.magenta);
        if (rightRay.collider.gameObject == player || leftRay.collider.gameObject == player)
        {
            detectPlayer = true;
            StartCoroutine(Explosive()); //부풀어오르는 애니메이션이 있을 경우 해당 애니메이션 상태를 체크한것으로 대체한다
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) //플레이어와 몬스터의 충돌을 감지하는 역할. 플레이어 스크립트 내로 옮기는게 좋음.
        {
            Debug.LogWarning($"{gameObject.name}: 플레이어 충돌 메소드를 플레이어 스크립트로 옮기기 바람");
            _collision.gameObject.GetComponent<HHH_Player>().currentHitPoint -= 2;
            Destroy(gameObject);
        }
    }

    IEnumerator Explosive()
    {
        yield return explosionDelayCount;
        GameObject frag1 = Instantiate(fragment);
        frag1.transform.position = transform.position;
        frag1.GetComponent<Fragment>().Initialize(fragmentDamage, fragmentVelocity);
        GameObject frag2 = Instantiate(fragment);
        frag2.transform.position = transform.position;
        frag2.GetComponent<Fragment>().Initialize(fragmentDamage, -fragmentVelocity);
        frag2.GetComponent<SpriteRenderer>().flipX = true;
        Destroy(gameObject);
    }
}
