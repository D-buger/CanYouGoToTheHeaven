using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Text gameOverMessage;
    [SerializeField] Image background;
    [SerializeField] GameObject buttons;

    public void MethodStart()
    {
        StartCoroutine(AdjustBlackScreen());
    }

    IEnumerator ProgressUI()
    {
        StartCoroutine(TypingEffect(gameOverMessage, "Game Over", 0.32f));
        WaitForSeconds waitDelay00 = new WaitForSeconds("Game Over".Length * 0.32f + 1f);
        yield return waitDelay00;

        buttons.SetActive(true);
    }

    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    IEnumerator AdjustBlackScreen()
    {
        float timeCounter = 0f;
        background.gameObject.SetActive(true);

        while (true)
        {
            float deltaTime = Time.deltaTime;

            timeCounter += deltaTime;
            background.color = new Color(background.color.r, background.color.g, background.color.b, background.color.a + (deltaTime * 0.4f));
            if (timeCounter >= 4f)
            {
                break;
            }
            yield return waitForEndOfFrame;
        }
        StartCoroutine(ProgressUI());
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
