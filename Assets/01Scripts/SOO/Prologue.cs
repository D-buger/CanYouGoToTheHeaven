using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Prologue : MonoBehaviour
{
    static char[] SPLIT_SEPARATOR = { '/' };

    private List<Dictionary<string, string>> prologue;

    private FileLoader<Sprite> image;

    public TMP_Text textShow;
    public Image imageShow;

    private void Awake()
    {
        GameManager.Instance.input.Touch.ButtonExtent = 0;

        prologue = CSVReader.Read("Prologue", out int size);
        image = new FileLoader<Sprite>("02Sprites/prologue");

        string[] split = prologue[0]["story"].Split(SPLIT_SEPARATOR);
        for(int i = 0; i < split.Length; i++)
        {
            Debug.Log(split[i]);
        }
    }

    private void MovingWordCheck(string str)
    {
        string[] find = str.Split('>');
    }

    private void ColorWordCheck()
    {

    }

    private void Update()
    {



        //Timer timer = new Timer(3);

        //if (timer.TimerUpdate())
        //{
        //    Next();
        //    timer.Reset();
        //}

        //if(GameManager.Instance.input.touch.mouseState == eMouse.Down)
        //{
        //    Skip();
        //}
    }

    private void Skip()
    {

    }

    private void Next()
    {

    }
}
