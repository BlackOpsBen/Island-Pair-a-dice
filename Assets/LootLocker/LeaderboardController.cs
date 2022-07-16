using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using System;
using TMPro;

public class LeaderboardController : MonoBehaviour
{
    public InputField memberID;
    public int ID;
    int maxScores = 10;
    public TextMeshProUGUI[] entryName, entryScore;

    public GameObject leaderboardUI;
    public GameObject submitUI;

    private void Start()
    {
        LootLockerSDKManager.StartSession("Player", (response) =>
        {
            if (response.success)
            {
                Debug.Log("Session Success");
            }
            else
            {
                Debug.Log("Session Failed");
            }
        });
    }

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(ID, maxScores, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;

                for (int i = 0; i < scores.Length; i++)
                {
                    entryName[i].text = scores[i].rank + ". " + scores[i].member_id;
                    entryScore[i].text = scores[i].score.ToString();
                }

                if (scores.Length < maxScores)
                {
                    for (int i = scores.Length; i < maxScores; i++)
                    {
                        entryName[i].text = (i + 1).ToString() + ".";
                        entryScore[i].text = "...";
                    }
                }
            }
            else
            {
                Debug.Log("Failed");
            }
        });
    }

    // Called by Submit Button
    public void OnSubmitScore()
    {
        StartCoroutine(ScoreSequence());
    }

    private void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(memberID.text, 12321, ID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Submit Success");
            }
            else
            {
                Debug.Log("Submit Failed: " + response.Error);
            }
        });
    }

    private IEnumerator ScoreSequence()
    {
        SubmitScore();
        leaderboardUI.SetActive(true);
        submitUI.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        ShowScores();
    }
}
