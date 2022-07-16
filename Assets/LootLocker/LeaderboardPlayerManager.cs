using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using System;
using TMPro;

public class LeaderboardPlayerManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoginRoutine());
    }

    private IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Players was logged in.");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.LogWarning("Could not start session.");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
