using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearScreen : MonoBehaviour
{
    [SerializeField] Image whiteScreen;

    bool isAlreadyProgressing = false;

    public void MethodStart()
    {
        if (isAlreadyProgressing)
        {
            return;
        }
    }

    IEnumerator ProgressEndingScreen()
    {
        yield return null;
    }
}
