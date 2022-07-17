using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    [SerializeField] private int turnsPerGame = 5;

    [SerializeField] private TextMeshProUGUI turnsText;

    private int turnsRemaining;

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

        turnsRemaining = turnsPerGame;

        UseTurn();
    }

    public void UseTurn()
    {
        turnsRemaining--;
        UpdateTurnsText();
    }

    private void UpdateTurnsText()
    {
        turnsText.text = turnsRemaining.ToString();
    }

    public void EndTurn()
    {
        ScoreManager.Instance.BankPoints();

        if (turnsRemaining > 0)
        {
            UseTurn();
            DiceManager.Instance.ResetDice();
            StateManager.Instance.NewTurnStarted();
        }
        else
        {
            StateManager.Instance.GameEnded();
            Debug.LogWarning("Game Ended");
        }
    }

    public void NewGame()
    {
        turnsRemaining = turnsPerGame;
        UseTurn();
    }
}
