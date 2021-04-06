using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager
{
    [SerializeField] KeyCode attackKey = KeyCode.Z;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    public KeyCode AttackKey => attackKey;
    public KeyCode JumpKey => jumpKey;

    //enum�� ����ϸ� int������ ��ȯ��ų �� �ڽ��� �Ͼ�Ƿ� ����ü�� ó���غô�.
    public struct eKeyValue
    {
        public const byte
            KEY_ATTACK  = 0x0001,
            KEY_JUMP    = 0x0002,
            KEY_LEFT    = 0x0010,
            KEY_RIGHT   = 0x0020
            ;
    }
    //��Ʈ �����ڸ� ��������ν� �������� bool���� �δ°� �ƴ�, �� ���� ���������� ������ üũ�� �� �ְԲ� �ߴ�.
    public byte GetKeyValue { get; private set; } = 0x0000;

    #region Dictionary
    public Dictionary<KeyCode, byte> dKeyValues;
    
    public void SetFirstValue()
    {
        Dictionary<KeyCode, byte> dKeyValues = new Dictionary<KeyCode, byte>();
        dKeyValues.Add(attackKey, eKeyValue.KEY_ATTACK);
        dKeyValues.Add(jumpKey, eKeyValue.KEY_JUMP);
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
    #endregion

    public void GetKeyUpdate()
    {
        InputKey(attackKey, eKeyValue.KEY_ATTACK);
        InputKey(jumpKey, eKeyValue.KEY_JUMP);
    }

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

}
