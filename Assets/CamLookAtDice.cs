using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookAtDice : MonoBehaviour
{
    [SerializeField] private Transform[] targets;

    [SerializeField] private float slerpSpeed = 2.0f;

    private void Update()
    {
        Vector3 averagePosition = DiceManager.Instance.averagePosition;

        Vector3 lookDireciton = averagePosition - transform.position;

        Quaternion desiredRotation = Quaternion.LookRotation(lookDireciton, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * slerpSpeed);
    }
}
