using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInput : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textComponent;

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
        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            Vector3[] verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for(int j = 0; j < 4; ++j)
            {
                Vector3 vec = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = vec +
                    new Vector3(0, Mathf.Sin(Time.time * 2f + vec.x * 0.01f) * 10f, 0) ;
            }
        }

        for(int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            TMP_MeshInfo meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }


}
