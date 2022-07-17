using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

    public Vector3 averagePosition;

    [SerializeField] private Transform[] suspensionPoints;

    [SerializeField] private float suspendLerpSpeed = 2.0f;

    [SerializeField] private Dice[] dice;

    [SerializeField] private float torqueMultiplier = 10.0f;

    [SerializeField] private float tossForceMultiplier = 5.0f;

    [SerializeField] private float maxDiceDist = 10.0f;

    [SerializeField] private float adhesionForce = 8.0f;

    private bool isSuspended = true;

    private bool isDoneRolling = false;

    private int resultScore = 0;

    private Vector3[] randTorques = new Vector3[2];

    [SerializeField] private ShakePreset skunkShakePreset;

    private float diceTimerLimit = 6.0f;
    private float diceTimer = 0.0f;

    [SerializeField] private GameObject stuckDiceUI;

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

        randTorques[0] = Vector3.zero;
        randTorques[1] = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (isSuspended)
        {
            for (int i = 0; i < dice.Length; i++)
            {
                dice[i].rb.position = Vector3.Lerp(dice[i].rb.position, suspensionPoints[i].position, Time.fixedDeltaTime * suspendLerpSpeed);

                dice[i].rb.velocity = Vector3.zero;

                //Vector3 randTorque = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f));

                dice[i].rb.AddTorque(randTorques[i] * torqueMultiplier, ForceMode.Force);
            }
        }
        /*else
        {
            float distBetween = GetDistBetweenDice();
            //Debug.Log("Distance between dice: " + distBetween);
            if (distBetween > maxDiceDist)
            {
                for (int i = 0; i < dice.Length; i++)
                {
                    dice[i].rb.AddExplosionForce(-adhesionForce, averagePosition, float.MaxValue, 0.0f, ForceMode.Force);
                }
            }
        }*/
    }

    private float GetDistBetweenDice()
    {
        return Vector3.Distance(dice[0].transform.position, dice[1].transform.position);
    }

    public void Roll()
    {
        if (StateManager.Instance.GetIsFirstRollOfTurn())
        {
            TurnManager.Instance.UseTurn();
        }

        AudioManager.Instance.PlaySound("gravels", "WorldSFX");
        AudioManager.Instance.PlaySound("whoosh", "Misc");

        isSuspended = false;
        NudgeDice();

        diceTimer = 0.0f;
    }

    public void NudgeDice()
    {
        foreach (Dice die in dice)
        {
            die.rb.AddForce(Vector3.up * tossForceMultiplier, ForceMode.Impulse);
        }

        diceTimer = 0.0f;
    }

    private void Update()
    {
        randTorques[0] = Vector3.Slerp(randTorques[0], new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)), Time.deltaTime);
        randTorques[1] = Vector3.Slerp(randTorques[1], new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)), Time.deltaTime);

        averagePosition = Vector3.Lerp(dice[0].transform.position, dice[1].transform.position, 0.5f);

        if (!isSuspended)
        {
            if (!isDoneRolling)
            {
                diceTimer += Time.deltaTime;
            }

            Debug.Log(diceTimer);
            stuckDiceUI.SetActive(diceTimer > diceTimerLimit);

            foreach (Dice die in dice)
            {
                if (!die.isStopped)
                {
                    return;
                }
            }

            if (!isDoneRolling)
            {
                isDoneRolling = true;
                OnDoneRolling();
            }
        }
    }

    public void ResetDice()
    {
        isSuspended = true;
        isDoneRolling = false;
        foreach (Dice die in dice)
        {
            float randX = UnityEngine.Random.Range(0.5f, 1.0f);
            float randY = UnityEngine.Random.Range(-1.0f, 1.0f);
            float randZ = UnityEngine.Random.Range(0.5f, 1.0f);
            Vector3 randTorque = new Vector3(randX, randY, randZ);
            die.rb.AddTorque(randTorque * 1000.0f, ForceMode.Impulse);
        }
    } 
    
    private void OnDoneRolling()
    {
        Debug.Log("Done Rolling");

        resultScore = 0;

        int numSkunks = 0;

        for (int i = 0; i < dice.Length; i++)
        {
            int rolledSide = dice[i].GetRolledSide();
            if (rolledSide == 6)
            {
                numSkunks++;
            }
            else
            {
                resultScore += rolledSide;
            }
        }

        if (numSkunks == 1)
        {
            resultScore = 0;
            ScoreManager.Instance.ResetTurnScore();
            StateManager.Instance.MustEnd();
            AudioManager.Instance.PlaySound("skunk1", "Misc");
            Shaker.ShakeAll(skunkShakePreset);
            
        }
        else if (numSkunks == 2)
        {
            ScoreManager.Instance.ResetTotalScore(true);
            ScoreManager.Instance.ResetTurnScore();
            StateManager.Instance.MustEnd();
            AudioManager.Instance.PlaySound("skunk1", "Misc");
            Shaker.ShakeAll(skunkShakePreset);
        }
        else
        {
            ScoreManager.Instance.AddTurnPoints(resultScore);
            StateManager.Instance.CanContinue();
        }

        diceTimer = 0.0f;
    }

    public bool GetIsSuspended()
    {
        return isSuspended;
    }

    public int GetResultScore()
    {
        return resultScore;
    }
}
