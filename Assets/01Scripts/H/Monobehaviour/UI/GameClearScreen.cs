using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearScreen : MonoBehaviour
{
    [SerializeField] Text clearMessage;
    [SerializeField] Text informationText;
    [SerializeField] GameObject buttons;

    public void MethodStart()
    {
        MakeInformationText();
        StartCoroutine(ProgressEndingScreen());
    }

    string infoString = null;
    
    void MakeInformationText()
    {
        infoString = $"Total Time {SecondsParseTimer(33921)}\n\nÃ³Ä¡ÇÑ ÀûÀÇ ¼ö: 33\nÀ½·á ¸®ÇÊ È½¼ö: 09\nÃÑ ¾ÆÀÌÅÛ È¹µæ ¼ö: 33";
    }

    string SecondsParseTimer(float _totalSecond)
    {
        int hour = 0;
        int minute = 0;

        while (_totalSecond - 60f >= float.Epsilon)
        {
            _totalSecond -= 60f;
            minute += 1;
        }
        while (minute - 60 >= 0)
        {
            minute -= 60;
            hour += 1;
        }
        string hourString = "";
        if (hour - 10 >= 0)
        {
            hourString = $"{hour}";
        }
        else
        {
            hourString = $"0{hour}";
        }

        if (minute - 10 >= 0)
        {
            return $"{hourString} : {minute} : {_totalSecond}";
        }
        else
        {
            return $"{hourString} : 0{minute} : {_totalSecond}";
        }
    }

    IEnumerator ProgressEndingScreen()
    {
        clearMessage.gameObject.SetActive(true);

        StartCoroutine(TypingEffect(clearMessage, "Game Clear!", 0.19f));
        WaitForSeconds waitDelay00 = new WaitForSeconds("Game Clear!".Length * 0.19f + 1f);

        yield return waitDelay00;

        informationText.gameObject.SetActive(true);

        StartCoroutine(TypingEffect(informationText, infoString, 0.076f));
        WaitForSeconds waitDelay01 = new WaitForSeconds(infoString.Length * 0.076f + 1f);

        yield return waitDelay01;

        buttons.SetActive(true);
    }

    IEnumerator TypingEffect(Text _text, string _message, float _speed)
    {
        WaitForSeconds ddd = new WaitForSeconds(_speed);
        for (int i = 0; i < _message.Length; i++)
        {
            _text.text = _message.Substring(0, i + 1);
            yield return ddd;
        }
    }
}
