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
            if (!StageManager.PlayerInStage)
            {
                Touch.touchAction[2, 1] -= (Vector2 vec) => attackEndCallback?.Invoke();
                Touch.touchAction[1, 1] -= (Vector2 vec) => attackCallback?.Invoke();
                Touch.touchAction[0, 1] += (Vector2 vec) => activeCallback?.Invoke();
            }
            else
            {
                Touch.touchAction[2, 1] += (Vector2 vec) => attackEndCallback?.Invoke();
                Touch.touchAction[1, 1] += (Vector2 vec) => attackCallback?.Invoke();
                Touch.touchAction[0, 1] -= (Vector2 vec) => activeCallback?.Invoke();
            }
        }
    }
    public bool BehaviourActive => active && !StageManager.PlayerInStage;
    public bool AttackActive => active && StageManager.PlayerInStage;
    
    public Action activeCallback;
    public Action attackCallback;
    public Action attackEndCallback;

    public void SetFirst()
    {
        Joystick.SetFirst();

        Touch.touchAction[0, 0] += Joystick.JoyStickSetActive;
        Touch.touchAction[1, 0] += Joystick.OnDrag;
        Touch.touchAction[2, 0] += Joystick.OnPointerUp;

        Touch.touchAction[0, 1] += (Vector2 vec) => Active = true;
        Touch.touchAction[2, 1] += (Vector2 vec) => Active = false;
    }

    public void InputUpdate()
    {
        Touch.TouchUpdate();
    }

    public bool IsMove => (Joystick.horizontalValue != 0);
}