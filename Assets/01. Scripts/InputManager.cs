using UnityEngine;

public class InputManager : SingletonBehavior<InputManager>
{
    [SerializeField] KeyCode attackKey;
    [SerializeField] KeyCode jumpKey;

    public KeyCode AttackKey => attackKey;
    public KeyCode JumpKey => jumpKey;

    //enum�� ����ϸ� int������ ��ȯ��ų �� �ڽ��� �Ͼ�Ƿ� ����ü�� ó���غô�.
    struct eKeyValue
    {
        public const int
            KEY_ATTACK  = 0x00000001,
            KEY_JUMP    = 0x00000002,
            KEY_LEFT    = 0x00000010,
            KEY_RIGHT   = 0x00000020
            ;
    }

    //��Ʈ �����ڸ� ��������ν� �������� bool���� �δ°� �ƴ�, �� ���� ���������� ������ üũ�� �� �ְԲ� �ߴ�.
    public int GetKeyValue { get; private set; } = 0x00000000;

    protected override void OnAwake()
    {
    }

    private void Update()
    {
        InputKey(attackKey, eKeyValue.KEY_ATTACK);
        InputKey(jumpKey, eKeyValue.KEY_JUMP);
    }

    private void InputKey(KeyCode _key, int _keyValue)
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
