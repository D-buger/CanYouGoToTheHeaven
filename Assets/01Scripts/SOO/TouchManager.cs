using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TouchManager
{
    public static int MaxMultiTouch { get; private set; } = 2;
    public int NowTouchCount { get; private set; }
    
    public CustomTouch?[] Touches { get; private set; }
        = new CustomTouch?[MaxMultiTouch];

    public int[] SavedFingerId = new int[MaxMultiTouch];

    public TouchManager()
    {
        for (int i = 0; i < MaxMultiTouch; i++)
            SavedFingerId[i] = -1;
    }
    
    public bool TouchUpdate()
    {
        NowTouchCount = Mathf.Min(Input.touchCount, MaxMultiTouch);
        for(int i = 0; i < MaxMultiTouch; i++)
        {
            Touches[i] = null;
        }

        if (NowTouchCount > 0) {
            CustomTouch customTouch;
            for (int i = 0; i < MaxMultiTouch; i++)
            {
                if (Input.touchCount > i)
                {
                    customTouch = new CustomTouch(Input.touches[i]);
                    Touches[customTouch.touch.fingerId] = customTouch;
                }
            }
            for (int i = 0; i < Touches.Length; i++)
            {
                if (!Touches[i].HasValue)
                {
                    for(int j = 0; j < SavedFingerId.Length; j++)
                        SavedFingerId[j] = SavedFingerId[j] == i ? -1 : SavedFingerId[j];
                }
            }

             return true;
        }
        else
            return false;
    }
    

}

public struct CustomTouch
{
    public CustomTouch(Touch _touch)
    {
        touch = _touch;
        resolutionPos = new Vector2(
                    touch.position.x / GameManager.ScreenSize.x,
                    touch.position.y / GameManager.ScreenSize.y);
    }

    public Touch touch;
    public Vector2 resolutionPos;
}