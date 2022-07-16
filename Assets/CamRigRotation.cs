using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRigRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float lerpSpeed = 2.0f;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        Vector3 averageFlattened = DiceManager.Instance.averagePosition;
        averageFlattened = new Vector3(averageFlattened.x, transform.position.y, averageFlattened.z);

        transform.position = Vector3.Lerp(transform.position, averageFlattened, Time.deltaTime * lerpSpeed);
    }
}
