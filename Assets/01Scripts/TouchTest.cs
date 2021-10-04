using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TouchTest : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void Awake()
    {
        GameManager.Instance.input.activeCallback += () => text.text = "안녕하세용";
    }

    private void Update()
    {
        if (GameManager.Instance.input.Active)
        {
            text.text = "안녕하세용2차";
        }
    }
}
