using System.Collections;
using UnityEngine;
using TMPro;

public class TextInput : MonoBehaviour
{
    private TMP_Text textComponent;

    private TextSplit textDictionary;
    private TextEffects textEffects;
    
    private IEnumerator moveEffect = null;
    private IEnumerator colorEffect = null;

    private void Awake()
    {
        if (textComponent == null)
            textComponent = GetComponent<TMP_Text>();

        textEffects = new TextEffects();
    }

    private void Update()
    {
        textEffects.UpdateTexts();

        Skip();
    }

    public void Skip()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StopCoroutine(moveEffect);
            ChangeText(message);
        }
    }

    public void Clear()
    {
        StopAllCoroutines();
        textComponent.text = null;
    }

    private void ChangeText(string afterText)
    {
        message = afterText;
        textComponent.text = message;
    }

    private void MoveEffect(IEnumerator effect)
    {
        if (moveEffect != null)
            StopCoroutine(moveEffect);
        moveEffect = effect;
        StartCoroutine(moveEffect);
    }

    private void ColorEffect(IEnumerator effect)
    {
        if (colorEffect != null)
            StopCoroutine(colorEffect);
        colorEffect = effect;
        StartCoroutine(colorEffect);
    }
}
