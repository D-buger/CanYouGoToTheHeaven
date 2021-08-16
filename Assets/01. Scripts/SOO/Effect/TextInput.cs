using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInput : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textComponent;

    public Gradient gradient;
    public string message;

    private IEnumerator moveEffect = null;
    private IEnumerator colorEffect = null;

    private void Awake()
    {
        if (textComponent == null)
            textComponent = GetComponent<TMP_Text>();

        string str = GetComponent<TextMeshPro>().text;
        if (str != null)
            message = str;
        else
            str = message;
    }

    private void Update()
    {
        textComponent.ForceMeshUpdate();
        TextEffects.UpdateTexts(textComponent);
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
