using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveInOut : MonoBehaviour
{
    [SerializeField] private float inZ = -5.0f;
    [SerializeField] private float outZ = -15.0f;
    [SerializeField] private float transitionSpeed = 2.0f;

    private float targetZ;

    private void Awake()
    {
        targetZ = outZ;
    }

    private void Update()
    {
        Vector3 desiredPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, targetZ);
        transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition, Time.deltaTime * transitionSpeed);

        SetCloseUp(!DiceManager.Instance.GetIsSuspended());
    }

    public void SetCloseUp(bool value)
    {
        if (value)
        {
            targetZ = inZ;
        }
        else
        {
            targetZ = outZ;
        }
    }
}
