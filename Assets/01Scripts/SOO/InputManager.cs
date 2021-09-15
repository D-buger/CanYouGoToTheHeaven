using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager
{
    public Joystick joystick;
    public TouchManager touch;
    public bool behaviourActive { get; private set; }

    private float buttonExtent = 0.5f;
    public float ButtonExtent
    {
        get => buttonExtent;
        set => buttonExtent = Mathf.Lerp(0, 1, value);
    }

    public void SetFirst()
    {
        joystick.SetFirst();
    }

    public void InputUpdate()
    {
        touch.TouchUpdate();

        Joystick();
        Behaviour();
    }

    public bool IsMove => (joystick.horizontalValue != 0);

    private void Joystick()
    {
        if (touch.mouseState == eMouse.Down && touch.touchPos.x < 0)
        {
            joystick.JoyStickSetActive(true, touch.mousePos);
        }

        if (touch.mouseState == eMouse.Drag)
        {
            joystick.OnDrag(touch.mousePos);
        }

        if (touch.mouseState == eMouse.Up && joystick.isActive)
        {
            joystick.OnPointerUp();
        }
    }

    //전처리기 지시어
#if UNITY_EDITOR
    private void Behaviour()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            behaviourActive = true;
        }
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            behaviourActive = false;
        }
    }
#else
    private void Behaviour()
    {
        if(touch.mouseState == eMouse.Down)
        {
            if(touch.touchPos.x > 0)
            {
                behaviourActive = true;
            } 
        }

        if(touch.mouseState == eMouse.Up)
        {
            if(!joystick.isActive)
            {
                behaviourActive = false;
            }
        }
    }
#endif
}