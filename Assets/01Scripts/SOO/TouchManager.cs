using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TouchManager
{
    public const int MAX_MULTITOUCH = 2;

    public Action<Vector2>[,] touchAction
        = new Action<Vector2>[3, MAX_MULTITOUCH];

    public Touch[] Touches { get; private set; }
        = new Touch[MAX_MULTITOUCH];

    public int[] TouchFingerId { get; private set; }
        = new int[MAX_MULTITOUCH];

    private float buttonExtent = 0.5f;
    public float ButtonExtent
    {
        get => buttonExtent;
        set => buttonExtent = Mathf.Clamp01(value);
    }

    private Vector2 ResolutionPos(Touch touch) => new Vector2(
                    touch.position.x / GameManager.ScreenSize.x,
                    touch.position.y / GameManager.ScreenSize.y);

    public TouchManager()
    {
        for (int i = 0; i < MAX_MULTITOUCH; i++)
            TouchFingerId[i] = -1;
    }

#if UNITY_EDITOR

    public void TouchUpdate()
    {
        Touch touch = default;

        Vector2 touchPos = new Vector2(Input.mousePosition.x / GameManager.ScreenSize.x,
                Input.mousePosition.y / GameManager.ScreenSize.y);

        if (Input.GetMouseButtonDown(0)
            && touchPos.x < buttonExtent)
        {
            touch.fingerId = 1;
            touch.position = Input.mousePosition;
            TouchBegin(0, touch);
        }
        else if (Input.GetMouseButton(0)) {
            touch.fingerId = 1;
            touch.position = Input.mousePosition;
            TouchMove(0, touch);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            TouchEnd(0, touch);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Down");
            touch.fingerId = 2;
            TouchBegin(1, touch);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Up");
            TouchEnd(1, touch);
        }
        Debug.Log(TouchFingerId[1]);
    }

#else
    public void TouchUpdate()
    {
        for(int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (ResolutionPos(touch).x < buttonExtent 
                        && TouchFingerId[0] == -1)
                    {
                        TouchBegin(0, touch);
                    }
                    else if (ResolutionPos(touch).x > buttonExtent
                        && TouchFingerId[1] == -1)
                    {
                        TouchBegin(1, touch);
                    }
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (TouchFingerId[0] != -1 
                        && touch.fingerId == TouchFingerId[0])
                    {
                        TouchMove(0, touch);
                    }
                    else if (TouchFingerId[1] != 1
                        && touch.fingerId == TouchFingerId[1])
                    {
                        TouchMove(1, touch);
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (touch.fingerId == TouchFingerId[0])
                    {
                        TouchEnd(0, touch);
                    }
                    else if(touch.fingerId == TouchFingerId[1])
                    {
                        TouchEnd(1, touch);
                    }
                    break;
            }
        }
    }
#endif

    private void TouchBegin(int fingerId, Touch touch)
    {
        TouchFingerId[fingerId] = touch.fingerId;
        touchAction[0, fingerId]?.Invoke(touch.position);
    }

    private void TouchMove(int fingerId, Touch touch)
    {
        touchAction[1, fingerId]?.Invoke(touch.position);
    }

    private void TouchEnd(int fingerId, Touch touch)
    {
        TouchFingerId[fingerId] = -1;
        touchAction[2, fingerId]?.Invoke(touch.position);
    }
}