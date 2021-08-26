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

    public void CheckingDamagedTime() //성능 우려가 있을 경우, 해당 작동 방식을 토대로 손보면 될것. 플레이어 공격에서 해당 메소드를 호출 해당 메소드도 override할 수 있을 것 같음
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
