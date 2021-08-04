using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyDictionary
{
    //비트 마스크를 이용한 연산
    public Dictionary<KeyCode, byte> dKeyValues = new Dictionary<KeyCode, byte>();

    public InputKeyDictionary(params KeyCode[] keys)
    {
        for(int i = 0; i < keys.Length; ++i)
        {
            dKeyValues.Add(keys[i], (byte)(2^i * 1));
        }
    }

    public bool GetKey(KeyCode _key)
    {
        byte _keyCode = GetValue(_key);

        return (GetKeyValue & _keyCode) > 0;
    }

    public void SetKey(KeyCode _key, KeyCode _changeKey)
    {
        byte _keyCode = GetValue(_key);

        dKeyValues.Remove(_key);
        dKeyValues.Add(_changeKey, _keyCode);
    }

    private byte GetValue(KeyCode _key)
    {
        if (!dKeyValues.ContainsKey(_key))
            return 0;

        dKeyValues.TryGetValue(_key, out byte _keyCode);
        return _keyCode;
    }

    //비트 연산자를 사용함으로써 여러개의 bool값을 두는게 아닌, 한 개의 변수만으로 값들을 체크할 수 있게끔 했다.
    public byte GetKeyValue { get; private set; } = 0000;

    private void InputKey(KeyCode _key, byte _keyValue)
    {
        if (Input.GetKeyDown(_key))
        {
            GetKeyValue |= _keyValue;
        }
        else if (Input.GetKeyUp(_key))
        {
            GetKeyValue ^= _keyValue;
        }
    }

    public void GetKeyUpdate()
    {
        foreach (var pair in dKeyValues)
        {
            InputKey(pair.Key, pair.Value);
        }
    }
}
