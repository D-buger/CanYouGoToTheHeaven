using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class TextEffects
{


    static IEnumerator cTyping(Text _text, string _message, float _typingTime)
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < _message.Length; i++)
        {
            _text.text = _message.Substring(0, i);
            yield return new WaitForSeconds(_typingTime);
        }
    }
}