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
            //nowWater�� new WaterTest ���� �� �ð����� watertest �� ���� ���ư��� �� ���� ���� 

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            //nowWater �����

        }
        
    }

}
