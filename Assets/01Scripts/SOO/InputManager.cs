using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager
{
    public Joystick Joystick;
    public TouchManager Touch { get; private set; }
        = new TouchManager();
    public bool BehaviourActive { get; private set; }

    private float buttonExtent = 0.5f;
    public float ButtonExtent
    {
        get => buttonExtent;
        set => buttonExtent = Mathf.Lerp(0, 1, value);
    }

    public void SetFirst()
    {
        Joystick.SetFirst();
    }

    public void InputUpdate()
    {
        if (Touch.TouchUpdate())
        {
            for (int i = 0; i < Touch.Touches.Length; i++)
            {
                if (Touch.SavedFingerId[0] == i
                    || Touch.SavedFingerId[1] == i)
                    continue;


                //처음 초기화 구문
                {
                    if (Touch.Touches[i].Value.resolutionPos.x < buttonExtent
                        && Touch.SavedFingerId[0] == -1)
                    {
                        Joystick.JoyStickSetActive(true, Touch.Touches[i].Value.touch.position);
                        Touch.SavedFingerId[0] = Touch.Touches[i].Value.touch.fingerId;
                    }
                    else if (Touch.SavedFingerId[1] == -1)
                    {
                        BehaviourActive = true;
                        Touch.SavedFingerId[1] = Touch.Touches[i].Value.touch.fingerId;
                    }
                }
            }

            if (Touch.SavedFingerId[0] != -1)
            {
                JoystickSet( Touch.Touches[Touch.SavedFingerId[0]].Value );
            }
            if (Touch.SavedFingerId[1] != -1)
            {
                Behaviour(Touch.Touches[Touch.SavedFingerId[1]].Value);
            }

        }
    }

    public bool IsMove => (Joystick.horizontalValue != 0);
    
    private void JoystickSet(CustomTouch customTouch)
    {
        if (customTouch.touch.phase == TouchPhase.Moved)
        {
            Joystick.OnDrag(customTouch.touch.position);
        }
        if (customTouch.touch.phase == TouchPhase.Ended)
        {
            Joystick.OnPointerUp();
        }
    }

    //전처리기 지시어
#if UNITY_EDITOR
    private void Behaviour(CustomTouch customTouch)
    {
        if (customTouch.touch.phase == TouchPhase.Ended)
        {
            BehaviourActive = false;
        }
    }
#else
    private void Behaviour(CustomTouch customTouch)
    {
        if (customTouch.touch.phase == TouchPhase.Ended)
        {
            BehaviourActive = false;
            Touch.SavedFingerID.Remove("Behaviour");
        }
    }
#endif

}