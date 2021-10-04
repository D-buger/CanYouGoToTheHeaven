using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehavior<GameManager>
{
    public AchievementManager achievement;
    public static GameAchivementData Data { get; private set; }

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

    protected override void OnAwake()
    {
        DontDestroyOnLoad(this);
        input = new InputManager();
        achievement = new AchievementManager();
        Data = new GameAchivementData(JsonManager.Load());
        Input.simulateMouseWithTouches = true;
        Input.multiTouchEnabled = true;
        input.SetFirst();
    }

    private void Update()
    {
        input.InputUpdate();
    }

    public void ChangeScene(int i)
        => SceneManager.LoadScene(i);

    public void ShowAchivement()
        => achievement.OnShowAchievement();
}
