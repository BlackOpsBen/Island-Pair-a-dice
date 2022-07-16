using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private int leaderboardID = 4767;

    public void SubmitScore(int score)
    {
        StartCoroutine(SubmitScoreRoutine(score));
    }

    private IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
