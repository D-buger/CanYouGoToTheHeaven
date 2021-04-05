using UnityEngine;

public class InputManager : SingletonBehavior<InputManager>
{
    [SerializeField] KeyCode attackKey;
    [SerializeField] KeyCode jumpKey;

    public KeyCode AttackKey => attackKey;
    public KeyCode JumpKey => jumpKey;

    //enum을 사용하면 int형으로 변환시킬 때 박싱이 일어나므로 구조체로 처리해봤다.
    struct eKeyValue
    {
        public const int
            KEY_ATTACK  = 0x00000001,
            KEY_JUMP    = 0x00000002,
            KEY_LEFT    = 0x00000010,
            KEY_RIGHT   = 0x00000020
            ;
    }

    //비트 연산자를 사용함으로써 여러개의 bool값을 두는게 아닌, 한 개의 변수만으로 값들을 체크할 수 있게끔 했다.
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
