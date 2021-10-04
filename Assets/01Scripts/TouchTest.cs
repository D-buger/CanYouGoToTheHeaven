using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchTest : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Awake()
    {
        GameManager.Instance.input.activeCallback += () => text.text = "�ȳ��ϼ���";
    }

    private void Update()
    {
        if (GameManager.Instance.input.Active)
        {
            text.text = "�ȳ��ϼ���2��";
        }
    }
}
