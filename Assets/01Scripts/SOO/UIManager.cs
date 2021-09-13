using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIManager
{
    //TODO : MVP 패턴을 이용하여 UI 구현
    private Slider remainingWater;

    //TODO : UI 클래스를 만들어 해당 UI 클래스 다 불러오기
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
