using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance { get; private set; }

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

    public Vector3 averagePosition;

    [SerializeField] private Transform[] suspensionPoints;

    [SerializeField] private Dice[] dice;

    [SerializeField] private float torqueMultiplier = 10.0f;

    [SerializeField] private float tossForceMultiplier = 5.0f;

    [SerializeField] private float maxDiceDist = 10.0f;

    [SerializeField] private float adhesionForce = 8.0f;

    private bool isSuspended = true;

    private void FixedUpdate()
    {
        if (isSuspended)
        {
            for (int i = 0; i < dice.Length; i++)
            {
                dice[i].rb.position = suspensionPoints[i].position;

                dice[i].rb.velocity = Vector3.zero;

                Vector3 randTorque = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f));

                dice[i].rb.AddTorque(randTorque * torqueMultiplier, ForceMode.Force);
            }
        }
        else
        {
            float distBetween = GetDistBetweenDice();
            Debug.Log("Distance between dice: " + distBetween);
            if (distBetween > maxDiceDist)
            {
                for (int i = 0; i < dice.Length; i++)
                {
                    dice[i].rb.AddExplosionForce(-adhesionForce, averagePosition, float.MaxValue, 0.0f, ForceMode.Force);
                }
            }
        }
    }

    private float GetDistBetweenDice()
    {
        return Vector3.Distance(dice[0].transform.position, dice[1].transform.position);
    }

    public void Roll()
    {
        isSuspended = false;

        foreach (Dice die in dice)
        {
            die.rb.AddForce(Vector3.up * tossForceMultiplier, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        averagePosition = Vector3.Lerp(dice[0].transform.position, dice[1].transform.position, 0.5f);

        if (!isSuspended)
        {
            foreach (Dice die in dice)
            {
                if (!die.isStopped)
                {
                    return;
                }
            }

            OnDoneRolling();
        }
    }

    public void ResetDice()
    {
        isSuspended = true;
    } 
    
    private void OnDoneRolling()
    {
        Debug.LogWarning("Done rolling!");
        foreach (Dice die in dice)
        {
            Debug.LogWarning(die.name + " result: " + die.GetRolledSide());
        }
    }

    public bool GetIsSuspended()
    {
        return isSuspended;
    }
}
