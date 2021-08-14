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
        Test(textComponent.textInfo);
    }

    private void Test(TMP_TextInfo textInfo)
    {
        TMP_CharacterInfo charInfo;

        float min = textInfo.characterInfo[0].vertex_BL.position.x;
        float max = textInfo.characterInfo[textInfo.characterCount - 1].vertex_TR.position.x;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            TMP_MeshInfo meshInfo = textInfo.meshInfo[charInfo.materialReferenceIndex];
            
            for(int j = 0; j < 4; ++j)
            {
                int index = charInfo.vertexIndex + j;
                Vector3 vec = meshInfo.vertices[index];
                meshInfo.vertices[index] = vec +
                    new Vector3(0, Mathf.Sin(Time.time * 3f + vec.x * 1f) * 0.1f, 0);
                float curXNormalized = 
                    Mathf.InverseLerp(min, max, vec.x);
                Color color = gradient.Evaluate(curXNormalized);
                meshInfo.colors32[index] = new Color(color.r, color.g, color.b, 1);
            }
        }

        for(int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            TMP_MeshInfo meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            meshInfo.mesh.colors32 = meshInfo.colors32;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }


}
