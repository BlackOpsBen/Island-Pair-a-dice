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

    [SerializeField] private GameObject rollBtn, keepGoingBtn, endTurnBtn, turnScore, turnScoreLabel, bankedLabel, bankedScore, rollsLabel, rollsCounter;

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
    }

    public void OnEndTurn()
    {
        TurnManager.Instance.EndTurn();
        keepGoingBtn.SetActive(false);
        endTurnBtn.SetActive(false);
        turnScore.SetActive(false);
        turnScoreLabel.SetActive(false);
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
        rollsLabel.SetActive(false);
        rollsCounter.SetActive(false);
    }
}
