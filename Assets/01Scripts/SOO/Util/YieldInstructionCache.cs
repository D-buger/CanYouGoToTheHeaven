using System.Collections.Generic;
using UnityEngine;

//internal : 외부 접근 제한, 같은 어셈블리일 때만 접근 가능
internal static class YieldInstructionCache
{
    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    
    private static Dictionary<float, WaitForSeconds> timeInteval
        = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetTimeInteval(float _f)
    {
        if (!timeInteval.ContainsKey(_f))
            timeInteval.Add(_f, new WaitForSeconds(_f));

        return timeInteval[_f];
    }
}
