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

    [SerializeField] private Animator bustedAnimator;

    private int ascendingSteps = 0;

    private bool isFirstRollOfTurn = true;

    public void OnRoll()
    {
        DiceManager.Instance.Roll();
        rollBtn.SetActive(false);
        isFirstRollOfTurn = false;
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
        bustedAnimator.ResetTrigger("Show");
        bustedAnimator.SetTrigger("Hide");
    }


    public void MustEnd()
    {
        keepGoingBtn.SetActive(false);
        endTurnBtn.SetActive(true);
        turnScore.SetActive(false);
        turnScoreLabel.SetActive(false);
        bustedAnimator.ResetTrigger("Hide");
        bustedAnimator.SetTrigger("Show");
    }

    public void CanContinue()
    {
        keepGoingBtn.SetActive(true);
        endTurnBtn.SetActive(true);
        turnScore.SetActive(true);
        turnScoreLabel.SetActive(true);
        ascendingSteps++;
        if (ascendingSteps > 10)
        {
            ascendingSteps = 10;
        }
        AudioManager.Instance.PlaySound("GainSC " + ascendingSteps, "Misc");
    }

    public void NewTurnStarted()
    {
        rollBtn.SetActive(true);
        turnScore.SetActive(false);
        turnScoreLabel.SetActive(false);
        ascendingSteps = 0;
        isFirstRollOfTurn = true;
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
        ScoreManager.Instance.ResetTotalScore(false);
        ScoreManager.Instance.ResetTurnScore();
        DiceManager.Instance.ResetDice();
        AudioManager.Instance.PlaySound("mallet_light_1", "UI");
    }

    public bool GetIsFirstRollOfTurn()
    {
        return isFirstRollOfTurn;
    }
}
