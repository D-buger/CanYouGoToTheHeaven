using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyDictionary
{
    //��Ʈ ����ũ�� �̿��� ����
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

    //��Ʈ �����ڸ� ��������ν� �������� bool���� �δ°� �ƴ�, �� ���� ���������� ������ üũ�� �� �ְԲ� �ߴ�.
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
