using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextInput : MonoBehaviour
{
    private TMP_Text textComponent;

    private TextSplit textDictionary;
    private TextEffects textEffects;
    
    private IEnumerator moveEffect = null;
    private IEnumerator colorEffect = null;

    private List<Dictionary<string, string>> prologue;

    int index = 0;

    private void Awake()
    {
        if (textComponent == null)
            textComponent = GetComponent<TMP_Text>();

        textEffects = new TextEffects();

        prologue = CSVReader.Read("Prologue");
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
            textDictionary = new TextSplit(prologue[index++]["story"]);
        }
    }

    public void Clear()
    {
        StopAllCoroutines();
        textComponent.text = null;
    }

    private void ChangeText(string afterText)
    {
        textComponent.text = afterText;
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
