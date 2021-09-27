using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InputManager
{
    public Joystick Joystick;
    public TouchManager Touch { get; private set; }
        = new TouchManager();
    private bool active = false;
    public bool Active
    {
        get => active;
        set
        {
            active = value;
        }
    }
    public bool BehaviourActive => active && !StageManager.PlayerInStage;
    public bool AttackActive => active && StageManager.PlayerInStage;
    
    public Action activeCallback;

    public void SetFirst()
    {
        Joystick.SetFirst();

        Touch.movementAction[0] += Joystick.JoyStickSetActive;
        Touch.movementAction[1] += Joystick.OnDrag;
        Touch.movementAction[2] += Joystick.OnPointerUp;

        Touch.touchAction[0] += () => Active = true;
        Touch.touchAction[2] += () => Active = false;
        Touch.touchAction[0] += activeCallback;
    }

    public void InputUpdate()
    {
        Touch.TouchUpdate();
    }

    public bool IsMove => (Joystick.horizontalValue != 0);
}