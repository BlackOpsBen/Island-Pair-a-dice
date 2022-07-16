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
    }

    [SerializeField] private GameObject rollBtn, keepGoingBtn, endTurnBtn;

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
    }


    public void MustEnd()
    {
        keepGoingBtn.SetActive(false);
        endTurnBtn.SetActive(true);
    }

    public void CanContinue()
    {
        keepGoingBtn.SetActive(true);
        endTurnBtn.SetActive(true);
    }

    public void NewTurnStarted()
    {
        rollBtn.SetActive(true);
    }
}
