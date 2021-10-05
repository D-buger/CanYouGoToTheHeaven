using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Prologue : MonoBehaviour
{
    private TMP_Text textComponent;
    [SerializeField]
    private Image imageComponent;

    private TextSplit textDictionary;
    private TextEffects textEffects;

    private List<Dictionary<string, string>> prologue;
    private FileLoader<Sprite> prologueImage;

    int index = 0;

    private void Start()
    {
        if (textComponent == null)
            textComponent = GetComponent<TMP_Text>();

        //textEffects = new TextEffects(textComponent);
        prologueImage = new FileLoader<Sprite>("Images/Prologue");

        prologue = CSVReader.Read("Prologue");
        SetTextEffects();

        GameManager.Instance.input.Touch.ButtonExtent = 0;
        GameManager.Instance.input.activeCallback += SetTextEffects;

        parent = transform.parent.gameObject;
    }

    private void Update()
    {
        //textEffects.UpdateTexts();
    }

    private void FixedUpdate()
    {
        //textEffects.GetTextElementsInFixedUpdate();
    }

    GameObject parent;
    private void SetTextEffects()
    {
        if (index < prologue.Count)
        {
            //StopAllCoroutines();

            imageComponent.sprite = prologueImage.files[prologue[index]["image"]];
            textDictionary = new TextSplit(prologue[index++]["story"]);
            ChangeText(textDictionary.TextWithoutTags);
            /*
            textEffects.NewTextEffects();
            for (int i = 0; i < textDictionary.Tags.Count; i++)
            {
                string tag = textDictionary.Tags[i];
                if (TextEffects.customTags.ContainsKey(tag))
                {
                    StartCoroutine(
                    TextEffects.customTags[tag](
                        textDictionary.TextEffect[tag]));
                }
            }
            */
        }
        else
        {
            GameManager.Instance.input.activeCallback -= SetTextEffects;
            parent.SetActive(false);
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
}
