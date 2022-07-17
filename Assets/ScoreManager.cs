using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI totalScoreText, turnScoreText, finalScoreText;

    [SerializeField] private Animator animator;

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
    }

    private int totalScore = 0;
    private int turnScore = 0;

    public void AddTurnPoints(int amount)
    {
        turnScore += amount;
        UpdateUI();
    }

    public void BankPoints()
    {
        totalScore += turnScore;
        if (turnScore > 0)
        {
            AudioManager.Instance.PlaySound("bank", "Misc");
            animator.SetTrigger("Gain");
        }
        ResetTurnScore();
    }

    public int GetTurnScore()
    {
        return turnScore;
    }

    public void ResetTurnScore()
    {
        turnScore = 0;
        UpdateUI();
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public void ResetTotalScore(bool isSkunk)
    {
        totalScore = 0;
        UpdateUI();
        if (isSkunk)
        {
            animator.SetTrigger("Reset");
        }
    }

    private void UpdateUI()
    {
        totalScoreText.text = totalScore.ToString();
        turnScoreText.text = turnScore.ToString();
        finalScoreText.text = totalScore.ToString();
    }
}
