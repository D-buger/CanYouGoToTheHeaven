using System.Collections.Generic;
using UnityEngine;

public enum eMouse
{
    Nothing,
    Down,
    Drag,
    Up
}

[System.Serializable]
public class TouchManager
{
    public Vector2 mousePos;
    public Vector2 touchPos;
    public eMouse mouseState;

    public void TouchUpdate()
    {
        mousePos = Input.mousePosition;
        touchPos = new Vector2(mousePos.x / GameManager.Instance.ScreenSize.x, mousePos.y / GameManager.Instance.ScreenSize.y);

        if (Input.GetMouseButton(0))
            Drag();
        else
            Nothing();

        if (Input.GetMouseButtonDown(0))
            Down();
        else if (Input.GetMouseButtonUp(0))
            Up();
        
    }

    private void Nothing()
    {
        mouseState = eMouse.Nothing;
    }

    private void Down()
    { 
        mouseState = eMouse.Down;
    }

    private void Drag()
    {
        mouseState = eMouse.Drag;
    }

    private void Up()
    {
        mouseState = eMouse.Up;
    }
}
