using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using System;
using TMPro;

public class LeaderboardPlayerManager : MonoBehaviour
{
    public Leaderboard leaderboard;

    public TMP_InputField playerNameInputField;

    private void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
            }
            else
            {
                Debug.LogWarning("Could not set player name. " + response.Error);
            }
        });
    }

    private IEnumerator SetupRoutine()
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighScoresRoutine();
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
