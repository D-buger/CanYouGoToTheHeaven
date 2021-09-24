using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager
{
    public Joystick Joystick;
    public TouchManager Touch { get; private set; }
        = new TouchManager();
    private bool active = false;
    public bool BehaviourActive => active && !StageManager.PlayerInStage;
    public bool AttackActive => active && StageManager.PlayerInStage;

    public void SetFirst()
    {
        Joystick.SetFirst();

        Touch.touchAction[0, 0] += Joystick.JoyStickSetActive;
        Touch.touchAction[1, 0] += Joystick.OnDrag;
        Touch.touchAction[2, 0] += Joystick.OnPointerUp;

        Touch.touchAction[0, 1] += (Vector2 vec) => active = true;
        Touch.touchAction[2, 1] += (Vector2 vec) => active = false;
    }

    public void InputUpdate()
    {
        Touch.TouchUpdate();
    }

    public bool IsMove => (Joystick.horizontalValue != 0);
}