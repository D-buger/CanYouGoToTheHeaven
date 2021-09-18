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
        Touch.TouchUpdate();

        for(int i = 0; i < Touch.NowTouchCount; i++)
        { 
            JoystickSet(i);
            Behaviour(i);
        }
    }

    public bool IsMove => (Joystick.horizontalValue != 0);

    private void JoystickSet(int i)
    {
        if (!Joystick.isActive)
        {
            if (Touch.TouchState[i] == TouchPhase.Began
                && Touch.ResolutionPos[i].x < buttonExtent)
            {
                Joystick.JoyStickSetActive(true, Touch.TouchPos[i]);
            }
        }
        else
        {
            if (Touch.TouchState[i] == TouchPhase.Moved)
            {
                Joystick.OnDrag(Touch.TouchPos[i]);
            }
            if (Touch.TouchState[i] == TouchPhase.Ended)
            {
                Joystick.OnPointerUp();
            }
        }
    }

    //전처리기 지시어
#if UNITY_EDITOR
    private void Behaviour(int i)
    {
        if (Touch.ResolutionPos[i].x > buttonExtent) {
            if (Touch.TouchState[i] == TouchPhase.Began)
            {
                BehaviourActive = true;
            }

            if (Touch.TouchState[i] == TouchPhase.Ended)
            {
                BehaviourActive = false;
            }
        }
    }
#else
    private void Behaviour(int i)
    {
        if(Touch.mouseState == eMouse.Down)
        {
            if(Touch.TouchPos.x > 0)
            {
                behaviourActive = true;
            } 
        }

        if(Touch.mouseState == eMouse.Up)
        {
            if(!Joystick.isActive)
            {
                behaviourActive = false;
            }
        }
    }
#endif
}