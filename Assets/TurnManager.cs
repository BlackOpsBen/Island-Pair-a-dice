using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    [SerializeField] private int turnsPerGame = 10;

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

        UpdateTurnsText();
    }

    public int GetTurnsRemaining()
    {
        return turnsRemaining;
    }

    public void UseTurn()
    {
        turnsRemaining--;
        UpdateTurnsText();
    }

    private void UpdateTurnsText()
    {
        turnsText.text = turnsRemaining + " Turns remaining";
    }

    public void EndTurn()
    {
        if (turnsRemaining > 0)
        {
            UseTurn();
            ScoreManager.Instance.BankPoints();
            DiceManager.Instance.ResetDice();
        }
    }
}
