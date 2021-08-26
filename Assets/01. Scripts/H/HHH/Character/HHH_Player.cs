using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HHH_Player : HHH_Character
{
    public int playerDamage; //Temporary
    HHH_ramgy enemy;

    public override void InflictDamage()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy1").GetComponent<HHH_ramgy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha6))
        {
            enemy.CheckingDamagedTime();
        }
    }
}
