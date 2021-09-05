using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Prologue : MonoBehaviour
{
    private List<Dictionary<string, string>> prologue;

    private ImageLoader image;

    public TMP_Text textShow;
    public Image imageShow;

    private void Awake()
    {
        prologue = CSVReader.Read("Prologue", out int size);
        image = new ImageLoader("02Sprites/prologue");
    }

    private void Update()
    {
        Timer timer = new Timer(3);
        if (timer.TimerUpdate())
        {
            Next();
            timer.Reset();
        }

        if(GameManager.Instance.input.touch.mouseState == eMouse.Down)
        {
            Skip();
        }
    }

    private void Skip()
    {

    }

    private void Next()
    {

    }
}
