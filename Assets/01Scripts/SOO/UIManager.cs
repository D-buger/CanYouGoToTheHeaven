using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIManager
{
    //TODO : MVP ������ �̿��Ͽ� UI ����
    private Slider remainingWater;

    //TODO : UI Ŭ������ ����� �ش� UI Ŭ���� �� �ҷ�����
    public UIManager()
    {
        
    }


    public void SetMaxWater(float maxValue)
    {
        remainingWater.maxValue = maxValue;
    }

    public void SetWater(float value)
    {
        remainingWater.value = value;
    }

}
