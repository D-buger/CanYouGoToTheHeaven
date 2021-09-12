using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTest_Monster01 : HTest_Monster
{
    bool detectPlayer;
    bool isTrackingPlayer;
    [SerializeField] float interval;
    [SerializeField] bool showDetectDistance;
    [SerializeField] bool showShotAngle;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float shotAngle;
    [SerializeField] float projectileVelocity;
    [SerializeField] int projectileCount;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        detectPlayer = DetectPlayer();

        if (!isTrackingPlayer) //���� ����: �ڷ�ƾ�� �̹� �������� �� �÷��̾ ���� ������ _�����ٰ� ���͵�_ �ڷ�ƾ�� �� ������ ���� �ݿ���
        {
            //�÷��̾ ���������� ���� ������ �ڷ�ƾ�� �����Ű�� �� ��. ���� ���������� ���� �� �ڷ�ƾ�� ������Ű�� �ɰ�

            if (detectPlayer)
            {
                SetMovementBehavior(new HTest_IMovementBehavior_TrackingTarget());
                isTrackingPlayer = true;
            }
        }
        else
        {
            if (!detectPlayer)
            {
                StartCoroutine(Re_CheckingPlayer(interval));
            }
            else
            {
                StopCoroutine("Re_CheckingPlayer");
            }
        }
        currentMovement.OperateUpdate(rb2d, playersss, movementSpeed);
    }

    protected void FireProjectile(GameObject _projectile, float _shotAngle, float _velocity, int _projectileCount) //���Ÿ� ������ �ϴ� �� �� �ƴ� ������ Ŭ������ �и��ؼ� ������
    {

    }

    IEnumerator Re_CheckingPlayer(float _interval)
    {
        WaitForSeconds delay = new WaitForSeconds(_interval);
        yield return delay;

        detectPlayer = DetectPlayer();

        if (detectPlayer)
        {
            StartCoroutine(Re_CheckingPlayer(interval));
        }
        else
        {
            SetMovementBehavior(new HTest_IMovementBehavior_Static());
            isTrackingPlayer = false;
            detectPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (showDetectDistance)
        {
            Gizmos.color = new Color(1, 0, 0);
            Gizmos.DrawWireSphere(transform.position, detectDistance);
        }
        if (showShotAngle)
        {
            Gizmos.color = new Color(0, 0, 1);
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + 4, transform.position.y));
        }
    }
}
