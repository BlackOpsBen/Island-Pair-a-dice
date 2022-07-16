using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Rigidbody rb;

    public bool isStopped = false;

    [SerializeField] private Transform[] sides;

    private void Update()
    {
        isStopped = rb.velocity.sqrMagnitude < float.Epsilon * 2;
    }

    public int GetRolledSide()
    {
        int resultIndex = 0;
        float sideHeight = sides[resultIndex].position.y;

        for (int i = 1; i < sides.Length; i++)
        {
            float newSideHeight = sides[i].position.y;
            if (newSideHeight > sideHeight)
            {
                resultIndex = i;
                sideHeight = newSideHeight;
            }
        }

        return resultIndex + 1;
    }
}
