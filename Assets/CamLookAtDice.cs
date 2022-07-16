using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookAtDice : MonoBehaviour
{
    [SerializeField] private Transform[] targets;

    [SerializeField] private float slerpSpeed = 2.0f;

    private void Update()
    {
        Vector3 averagePosition = Vector3.Lerp(targets[0].position, targets[1].position, 0.5f);

        Vector3 lookDireciton = averagePosition - transform.position;

        Quaternion desiredRotation = Quaternion.LookRotation(lookDireciton, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * slerpSpeed);
    }
}
