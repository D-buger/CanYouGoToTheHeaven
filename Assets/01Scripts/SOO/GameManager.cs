using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField]
    public InputManager input;
    public MapGenerator maps = new MapGenerator(5, 6);

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

    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
