using System.Collections;
using UnityEngine;
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

    private void Start()
    {
        MoveEffect(TextEffects.Typing(textComponent, 0.1f));
        //ColorEffect(TextEffects.Gradient(textComponent, gradient));
    }

    private void Update()
    {
        TextEffects.UpdateTexts(textComponent);

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
