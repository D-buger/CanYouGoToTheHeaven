using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameUX : MonoBehaviour
{
    public static GameUX instance = null;
    public StrategyJudgement currentJudgement = new StrategyJudgementNull();
    public GameObject confirmScreen;

    public GameObject gameOverScreen;
    public GameObject gameClearScreen;
    public Image whiteScreen;

    public GameOverScreen gameOverScreenComp { get; private set; }
    public GameClearScreen gameClearScreenComp { get; private set; }

    private void Awake()
    {
        MakeSingleton();
        gameClearScreenComp = gameClearScreen.GetComponent<GameClearScreen>();
        gameOverScreenComp = gameOverScreen.GetComponent<GameOverScreen>();
    }

    private void Start()
    {
        StageManager.clearEvent += new System.Action(GameHasCleared); //게임 클리어를 클리어 이벤트에 추가
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            StartCoroutine(AdjustWhiteScreen(gameClearScreen));
        }
    }

    void MakeSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public void GameHasCleared()
    {
        AdjustWhiteScreen(gameClearScreen);
    }

    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    IEnumerator AdjustWhiteScreen(GameObject _appearUI)
    {
        float timeCounter = 0f;
        whiteScreen.gameObject.SetActive(true);

        while (true)
        {
            float deltaTime = Time.deltaTime;

            timeCounter += deltaTime;
            whiteScreen.color = new Color(whiteScreen.color.r, whiteScreen.color.g, whiteScreen.color.b, whiteScreen.color.a + (deltaTime * 0.35f));
            if (timeCounter >= 4f)
            {
                break;
            }
            yield return waitForEndOfFrame;
        }
        timeCounter = float.Epsilon;
        _appearUI.SetActive(true);

        while (true)
        {
            float deltaTime = Time.deltaTime;

            timeCounter += deltaTime;
            whiteScreen.color = new Color(whiteScreen.color.r, whiteScreen.color.g, whiteScreen.color.b, whiteScreen.color.a - (deltaTime * 0.55f));
            if (timeCounter >= 3f)
            {
                break;
            }
            yield return waitForEndOfFrame;
        }
        whiteScreen.gameObject.SetActive(false);
        gameClearScreenComp.MethodStart();
    }

    public void ChangeStrategy(StrategyJudgement _strategy)
    {
        if (currentJudgement.GetType() == _strategy.GetType())
        {
            Debug.Log("이미 해당 상태입니다.");
        }
        else
        {
            currentJudgement.Exit();
            currentJudgement = _strategy;
            currentJudgement.Enter();
        }
    }

    public void Button_Confirm()
    {
        currentJudgement.Confirm();
    }

    public void Button_Cancle()
    {
        currentJudgement.Cancle();
    }

    public void Button_GoToTitleScreen()
    {
        ChangeStrategy(new StrategyJudgementGoToTitleScreen());
    }

    public void Button_PlayAgain()
    {
        gameOverScreen.SetActive(false);
        gameClearScreen.SetActive(false);
        confirmScreen.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public interface StrategyJudgement
{
    void Enter();
    void Exit();
    void Confirm();
    void Cancle();
}

class StrategyJudgementNull : StrategyJudgement
{
    void StrategyJudgement.Cancle()
    {
        Debug.LogWarning("현재 확인할 선택이 없습니다");
    }

    void StrategyJudgement.Confirm()
    {
        Debug.LogWarning("현재 확인할 선택이 없습니다");
    }
    public void Enter() //만일의 오류 방지
    {
        GameUX.instance.confirmScreen.SetActive(false);
    }

    public void Exit()
    {

    }
}

class StrategyJudgementGoToTitleScreen : StrategyJudgement
{
    public void Cancle()
    {
        GameUX.instance.ChangeStrategy(new StrategyJudgementNull());
    }

    public void Confirm()
    {
        GameManager.Instance.ChangeScene(0); Debug.LogWarning("타이틀 스크린 인덱스 넣을것");
        GameUX.instance.gameClearScreen.SetActive(false);
        GameUX.instance.gameOverScreen.SetActive(false);
        GameUX.instance.ChangeStrategy(new StrategyJudgementNull());
    }

    public void Enter()
    {
        GameUX.instance.confirmScreen.SetActive(true);
    }

    public void Exit()
    {
        GameUX.instance.confirmScreen.SetActive(false);
    }
}
