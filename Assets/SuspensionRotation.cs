using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 2.0f;

    private Rigidbody rb;

    private Vector3 randTorque = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 2.0f;
    }

    private void Update()
    {
        randTorque = Vector3.Slerp(randTorque, new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)), Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rb.AddTorque(randTorque * rotationSpeed, ForceMode.Force);
    }
}
