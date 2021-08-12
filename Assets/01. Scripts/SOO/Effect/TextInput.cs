using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInput : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public string message;

    private void Awake()
    {
        if (text == null)
            text = GetComponent<Text>();
        
        
    }


}
