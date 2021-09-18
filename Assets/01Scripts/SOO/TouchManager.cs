using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TouchManager
{
    public static int MaxMultiTouch { get; private set; } = 2;
    public int NowTouchCount { get; private set; }

    public Vector2[] TouchPos { get; private set; }
        = new Vector2[MaxMultiTouch];
    public Vector2[] ResolutionPos { get; private set; }
        = new Vector2[MaxMultiTouch];
    public TouchPhase[] TouchState { get; private set; }
        = new TouchPhase[MaxMultiTouch];

    public void TouchUpdate()
    {
        NowTouchCount = Mathf.Min(Input.touchCount, MaxMultiTouch);
        for (int i = 0; i < NowTouchCount; i++)
        {
            TouchPos[i] = Input.GetTouch(i).position;
            ResolutionPos[i] = new Vector2(
                TouchPos[i].x / GameManager.Instance.ScreenSize.x,
                TouchPos[i].y / GameManager.Instance.ScreenSize.y);
            TouchState[i] = Input.GetTouch(i).phase;
        }
    }
}