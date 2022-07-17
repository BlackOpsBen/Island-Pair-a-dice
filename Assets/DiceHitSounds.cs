using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHitSounds : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.PlaySound("DiceHitSC", transform);
    }
}
