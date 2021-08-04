using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatergunTest : MonoBehaviour
{
    Transform playerPosition;

    WaterTest nowWater;

    [SerializeField]
    private float waterDotSec;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //nowWater에 new WaterTest 생성 후 시간마다 watertest 각 점에 나아가야 할 방향 제시 

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //nowWater 끊어내기

        }
        
    }

}
