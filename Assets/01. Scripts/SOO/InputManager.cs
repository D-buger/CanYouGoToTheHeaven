using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager
{
    public Joystick joystick;
    public bool behaviourActive { get; set; }
    public TouchManager touch;

    public void SetFirst()
    {
        joystick.SetFirst();
    }

    public void InputUpdate()
    {
        touch.TouchUpdate();

        if(touch.mouseState == eMouse.Down)
        {
            if(touch.touchPos.x < 0)
            {
                joystick.JoyStickSetActive(true, touch.mousePos);
            }
            else
            {
                behaviourActive = true;
            }
        }

        if(touch.mouseState == eMouse.Drag)
        {
            joystick.OnDrag(touch.mousePos);
        }
        
        if(touch.mouseState == eMouse.Up)
        {
            if (touch.touchPos.x < 0)
            {
                joystick.OnPointerUp();
            }
            else
            {
                behaviourActive = false;
            }
        }
    }

    private void Joystick()
    {

    }

    public bool IsMove() => (joystick.horizontalValue != 0);
    

}