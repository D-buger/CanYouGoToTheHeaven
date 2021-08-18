using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class TextEffects
{
    public static IEnumerator Waving(TMP_Text textComponent)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            textComponent.ForceMeshUpdate();
            TMP_TextInfo textInfo = textComponent.textInfo;
            TMP_CharacterInfo charInfo;
            for (int i = 0; i < textInfo.characterCount; ++i)
            {
                charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible)
                    continue;

                TMP_MeshInfo meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];

                for (int j = 0; j < 4; ++j)
                {
                    int index = charInfo.vertexIndex + j;
                    Vector3 vec = meshInfo.vertices[index];
                    meshInfo.vertices[index] = vec +
                        new Vector3(0, Mathf.Sin(Time.time * 3f + vec.x * 1f) * 0.1f, 0);
                }
            }
            yield return null;
        }
    }

    public static IEnumerator Typing(Text _text, string _message, float _typingTime)
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < _message.Length; i++)
        {
            _text.text = _message.Substring(0, i);
            yield return new WaitForSeconds(_typingTime);
        }
    }

    public static IEnumerator Gradient(TMP_Text textComponent, Gradient gradient)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            TMP_TextInfo textInfo = textComponent.textInfo;
            TMP_CharacterInfo charInfo;

            float min = textInfo.characterInfo[0].vertex_BL.position.x;
            float max = textInfo.characterInfo[textInfo.characterCount - 1].vertex_TR.position.x;
            for (int i = 0; i < textInfo.characterCount; ++i)
            {
                charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible)
                    continue;

                TMP_MeshInfo meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];

                for (int j = 0; j < 4; ++j)
                {
                    int index = charInfo.vertexIndex + j;
                    Vector3 vec = meshInfo.vertices[index];
                    float curXNormalized =
                           Mathf.InverseLerp(min, max, vec.x);
                    Color color = gradient.Evaluate(curXNormalized);
                    meshInfo.colors32[index] = new Color(color.r, color.g, color.b, 1);
                }
            }
            yield return null;
        }
    }

    public static void UpdateTexts(TMP_Text textComponent)
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