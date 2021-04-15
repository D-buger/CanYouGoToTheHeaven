using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputManager
{
    [SerializeField] KeyCode moveLeftKey = KeyCode.LeftArrow;
    [SerializeField] KeyCode moveRightKey = KeyCode.RightArrow;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
 
    public KeyCode JumpKey => jumpKey;
    public KeyCode MoveLeftKey => moveLeftKey;
    public KeyCode MoveRightKey => moveRightKey;

    //enum�� ����ϸ� int������ ��ȯ��ų �� �ڽ��� �Ͼ�Ƿ� ����ü�� ó���غô�.
    public struct eKeyValue
    {
        public const byte
            KEY_JUMP    = 0x0001,
            KEY_LEFT    = 0x0010,
            KEY_RIGHT   = 0x0020
            ;
    }
    //��Ʈ �����ڸ� ��������ν� �������� bool���� �δ°� �ƴ�, �� ���� ���������� ������ üũ�� �� �ְԲ� �ߴ�.
    public byte GetKeyValue { get; private set; } = 0x0000;

    #region Dictionary
    private Dictionary<KeyCode, byte> dKeyValues = new Dictionary<KeyCode, byte>();
    
    public void SetFirstValue()
    {
        //Dictionary<KeyCode, byte> dKeyValues = new Dictionary<KeyCode, byte>();

        dKeyValues.Add(jumpKey, eKeyValue.KEY_JUMP);
        dKeyValues.Add(moveLeftKey, eKeyValue.KEY_LEFT);
        dKeyValues.Add(moveRightKey, eKeyValue.KEY_RIGHT);
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
        if (dKeyValues == null)
            Debug.Log("����ִ�");

        if (dKeyValues?.Count < 0)
            return;

        foreach (KeyValuePair<KeyCode, byte> pair in dKeyValues)
        {
            InputKey(pair.Key, pair.Value);
        }
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
