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

    void CheckPlayer() //circle�� radius��ŭ ������ ����: ���ο��� ��� ���� �ذ�
    {
        RaycastHit2D rightRay = Physics2D.Raycast(new Vector2(transform.position.x + radius, transform.position.y), Vector2.right, detectPlayerRange);
        RaycastHit2D leftRay = Physics2D.Raycast(new Vector2(transform.position.x + -radius, transform.position.y), Vector2.left, detectPlayerRange);
        Debug.DrawRay(new Vector2(transform.position.x + radius, transform.position.y), Vector2.right * detectPlayerRange, Color.magenta);
        Debug.DrawRay(new Vector2(transform.position.x + -radius, transform.position.y), Vector2.left * detectPlayerRange, Color.magenta);
        if (rightRay.collider.gameObject == player || leftRay.collider.gameObject == player)
        {
            detectPlayer = true;
            StartCoroutine(Explosive()); //��Ǯ������� �ִϸ��̼��� ���� ��� �ش� �ִϸ��̼� ���¸� üũ�Ѱ����� ��ü�Ѵ�
        }
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (_collision.gameObject.CompareTag("Player")) //�÷��̾�� ������ �浹�� �����ϴ� ����. �÷��̾� ��ũ��Ʈ ���� �ű�°� ����.
        {
            Debug.LogWarning($"{gameObject.name}: �÷��̾� �浹 �޼ҵ带 �÷��̾� ��ũ��Ʈ�� �ű�� �ٶ�");
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
