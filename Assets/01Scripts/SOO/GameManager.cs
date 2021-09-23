using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField]
    public InputManager input;

    public static readonly Vector2 ScreenSize 
                = new Vector2(Screen.width, Screen.height);

    private struct eSettings
    {
        public const int
            TEST_SETTING = 0x00000000
            ;
    } 
    public int GameSetting { get; private set; } = 0x00000000;

    private void Reset()
    {
        input = new InputManager();   
    }

    protected override void OnAwake()
    {
        Input.simulateMouseWithTouches = true;
        input.SetFirst();
    }

    private void Update()
    {
        input.InputUpdate();
    }
}
