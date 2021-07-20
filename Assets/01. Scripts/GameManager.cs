using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    public InputManager input;

    private struct eSettings
    {
        public const int
            TEST_SETTING = 0x00000000
            ;
    } 
    public int GameSetting { get; private set; } = 0x00000000;

    protected override void OnAwake()
    {
        input = new InputManager();
    }

    private void Update()
    {
        input.GetKeyUpdate();
    }
}
