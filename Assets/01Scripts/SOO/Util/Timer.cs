using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float timer = 0.0f;
    float endSec;

    public Timer(float sec)
    {
        endSec = sec;
    }

    public Timer(float sec, float startSec)
    {
        endSec = sec;
        timer = startSec;
    }


    public bool TimerUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= endSec)
        {
            return true;
        }

        return false;
    }

    public void Reset()
    {
        timer = 0;
    }
}
