using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Timer
{
    float timer;
    float endSec;

    public Timer(float sec)
    {
        endSec = sec;
        timer = 0;
    }

    public Timer(float sec, float startSec) : this(sec)
    {
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
