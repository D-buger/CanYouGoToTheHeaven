using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField]
    public InputManager input;

    public Vector2 ScreenSize { get; private set; }
                = new Vector2(Screen.width, Screen.height);

    private struct eSettings
    {
        public const int
            TEST_SETTING = 0x00000000
            ;
    } 
    public int GameSetting { get; private set; } = 0x00000000;

    protected override void OnAwake()
    {
        input.SetFirst();
    }

    private void Update()
    {
        input.InputUpdate();
    }
}
