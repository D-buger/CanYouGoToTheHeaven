using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffects
{
    private static readonly Gradient rainbowGradient = new Gradient()
    {
        colorKeys = new GradientColorKey[7]
        {
            new GradientColorKey(new Vector4(1,    0,    0,   1), 0),
            new GradientColorKey(new Vector4(0,    0.6f, 0,   1), 0.16f),
            new GradientColorKey(new Vector4(1,    1,    0,   1), 0.33f),
            new GradientColorKey(new Vector4(0.2f, 1,    0,   1), 0.50f),
            new GradientColorKey(new Vector4(0,    1,    1,   1), 0.67f),
            new GradientColorKey(new Vector4(0.2f, 1,    0,   1), 0.84f),
            new GradientColorKey(new Vector4(0.4f, 1,    0,   1), 1)
        }
    };

    public TMP_Text textComponent;
    private TMP_TextInfo textInfo;
    private TMP_CharacterInfo charInfo;
    private TMP_MeshInfo[] meshInfo;

    private Dictionary<string, System.Action> customTags;
    
    public TextEffects()
    {
        customTags = new Dictionary<string, System.Action>();

        customTags.Add("<H>", () => HorizontalShaking());
        customTags.Add("<W>", () => Waving());
        customTags.Add("<T>", () => Typing(3));
        customTags.Add("<Rainbow>", () => Rainbow());

        GetTextElementsInFixedUpdate();
    }

    private void DivideWithTags()
    {

    }

    public void GetTextElementsInFixedUpdate()
    {
        textComponent.ForceMeshUpdate();
        textInfo = textComponent.textInfo;

        meshInfo = new TMP_MeshInfo[textInfo.characterCount];

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            meshInfo[i] = textInfo.meshInfo[charInfo.materialReferenceIndex];
        }
    }

    private void HorizontalShaking()
    {
        while (true)
        {
            //wait for FixedUpdate()
            for (int i = 0; i < textInfo.characterCount; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    int index = charInfo.vertexIndex + j;
                    Vector3 vec = meshInfo[i].vertices[index];
                    meshInfo[i].vertices[index] = vec +
                        new Vector3(Mathf.Sin(Time.time * 3f) * 0.1f, 0, 0);
                }
            }
        }
    }


    private void Waving()
    {
        while (true)
        {
            //wait for FixedUpdate()
            for (int i = 0; i < textInfo.characterCount; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    int index = charInfo.vertexIndex + j;
                    Vector3 vec = meshInfo[i].vertices[index];
                    meshInfo[i].vertices[index] = vec +
                        new Vector3(0, Mathf.Sin(Time.time * 3f + vec.x * 1f) * 0.1f, 0);
                }
            }
        }
    }

    private void Typing(float time)
    {
        string text = textComponent.text;
        textComponent.text = null;
        for (int i = 0; i < text.Length; i++)
        {
            textComponent.text += text[i];
            //wait for seconds realtime
        }
    }

    private void Rainbow()
    {
        while (true)
        {
            //wait for fixed Update
            float min = textInfo.characterInfo[0].vertex_BL.position.x;
            float max = textInfo.characterInfo[textInfo.characterCount - 1].vertex_TR.position.x;

            for (int i = 0; i < textInfo.characterCount; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    int index = charInfo.vertexIndex + j;
                    Vector3 vec = meshInfo[i].vertices[index];
                    float curXNormalized =
                           Mathf.InverseLerp(min, max, vec.x);
                    Color color = rainbowGradient.Evaluate(curXNormalized);
                    meshInfo[i].colors32[index] = new Color(color.r, color.g, color.b, 1);
                }
            }
        }
    }

    public void UpdateTexts()
    {
        TMP_TextInfo textInfo = textComponent.textInfo;
        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            TMP_MeshInfo meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            meshInfo.mesh.colors32 = meshInfo.colors32;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}