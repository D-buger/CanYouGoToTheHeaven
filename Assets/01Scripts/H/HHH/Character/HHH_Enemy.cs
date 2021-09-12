using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHH_Enemy : HHH_Character
{
    protected HHH_Player player; //Temporary
    [SerializeField] protected float damagedTime = 0;

    public override void InflictDamage()
    {
        damagedTime -= 1;
        currentHitPoint -= player.playerDamage;
    }

    public void CheckingDamagedTime() //���� ����� ���� ���, �ش� �۵� ����� ���� �պ��� �ɰ�. �÷��̾� ���ݿ��� �ش� �޼ҵ带 ȣ�� �ش� �޼ҵ嵵 override�� �� ���� �� ����
    {
        damagedTime += Time.deltaTime;
        if (damagedTime >= 1)
        {
            InflictDamage();
        }
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
