using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        keepGoingBtn.SetActive(false);
        endTurnBtn.SetActive(false);
        turnScore.SetActive(false);
        turnScoreLabel.SetActive(false);
    }

    [SerializeField] private GameObject rollBtn, keepGoingBtn, endTurnBtn, turnScore, turnScoreLabel, scoreUI, leaderBoardUI, gameOverUI;

    public void OnRoll()
    {
        DiceManager.Instance.Roll();
        rollBtn.SetActive(false);
    }

    public void OnKeepGoing()
    {
        DiceManager.Instance.ResetDice();
        rollBtn.SetActive(true);
        keepGoingBtn.SetActive(false);
        endTurnBtn.SetActive(false);
        AudioManager.Instance.PlaySound("mallet_2", "UI");
    }

    public void OnEndTurn()
    {
        TurnManager.Instance.EndTurn();
        keepGoingBtn.SetActive(false);
        endTurnBtn.SetActive(false);
        turnScore.SetActive(false);
        turnScoreLabel.SetActive(false);
        AudioManager.Instance.PlaySound("mallet_3", "UI");
    }


    public void MustEnd()
    {
        keepGoingBtn.SetActive(false);
        endTurnBtn.SetActive(true);
        turnScore.SetActive(false);
        turnScoreLabel.SetActive(false);
    }

    public void CanContinue()
    {
        keepGoingBtn.SetActive(true);
        endTurnBtn.SetActive(true);
        turnScore.SetActive(true);
        turnScoreLabel.SetActive(true);
    }

    public void NewTurnStarted()
    {
        rollBtn.SetActive(true);
        turnScore.SetActive(false);
        turnScoreLabel.SetActive(false);
    }

    public void GameEnded()
    {
        keepGoingBtn.SetActive(false);
        endTurnBtn.SetActive(false);
        turnScore.SetActive(false);
        turnScoreLabel.SetActive(false);
        scoreUI.SetActive(false);

        gameOverUI.SetActive(true);
    }

    public void ShowLeaderBoard()
    {
        gameOverUI.SetActive(false);
        leaderBoardUI.SetActive(true);
        AudioManager.Instance.PlaySound("mallet_light_1", "UI");

        FindObjectOfType<Leaderboard>().SubmitScore(ScoreManager.Instance.GetTotalScore());
    }

    public void OnNewGame()
    {
        leaderBoardUI.SetActive(false);
        scoreUI.SetActive(true);
        NewTurnStarted();
        TurnManager.Instance.NewGame();
        ScoreManager.Instance.ResetTotalScore();
        ScoreManager.Instance.ResetTurnScore();
        DiceManager.Instance.ResetDice();
    }
}
