using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InputManager
{
    public Joystick Joystick { get; private set; }
        = new Joystick();
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
    
    public event Action activeCallback;

    public void SetFirst()
    {
        Touch.touchAction[0] += () => Active = true;
        Touch.touchAction[2] += () => Active = false;
        //Invoke를 event에 추가하려 하면 범위를 넘어가는 값이라고 뜬다.
        //아마도 null일 때가 있다보니 그렇게 뜨는 것 같아서 람다식으로 함수를 만들어 넣어줬다.
        Touch.touchAction[0] += () => activeCallback?.Invoke();

        Joystick.SetFirst();

        Touch.movementAction[0] += Joystick.JoyStickSetActive;
        Touch.movementAction[1] += Joystick.OnDrag;
        Touch.movementAction[2] += Joystick.OnPointerUp;
    }

    public void InputUpdate()
    {
        Touch.TouchUpdate();
    }

    public bool IsMove => (Joystick.horizontalValue != 0);
}