using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
public class LeaderboardsManager : MonoBehaviour
{
    int leaderboardID = 6963;
    public TMP_Text playerNames;
    public TMP_Text playerScores;
    public TMP_InputField nameInputField;

    void Awake()
    {
        GameManager.Instance.leaderboard = this;
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(nameInputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("SuccesfulChange");
            }
        });
    }
    int score;
    public void SetScore(int _score)
    {
        score = _score;
    }
    public void SubmitScore()
    {
        StartCoroutine(SubmitScoreRoutine(score));
    }

    public void CheckIfIsHighScore(int scoreToUpload, GameObject button, GameObject inputfield)
    {
        StartCoroutine(CheckHighScoreRoutine(scoreToUpload, button, inputfield));
    }

    IEnumerator CheckHighScoreRoutine(int scoreToUpload, GameObject button, GameObject inputfield)
    {
        string playerID = PlayerPrefs.GetString("PlayerID");

        bool done = false;
        LootLockerSDKManager.GetMemberRank(leaderboardID, playerID, (response) =>
        {
            if (response.success)
            {
                button.SetActive(response.score < scoreToUpload);
                inputfield.SetActive(response.score < scoreToUpload);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");

        LootLockerSDKManager.GetMemberRank(leaderboardID, playerID, (response) =>
         {
             if (response.success && response.score >= scoreToUpload)
             {
                 Debug.Log("Didn't beat best score");
                 //done = true;
                 //return;
             }
         });

        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Succesfully submitted score");
                done = true;
            }
            else
            {
                Debug.Log("Failed: " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);

        FetchHighScores();
    }

    public void FetchHighScores()
    {
        StartCoroutine(FetchTopHighScoresRoutine());
    }
    IEnumerator FetchTopHighScoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 10, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Players\n";
                string tempScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ")";

                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += "Guest: " + members[i].player.id;
                    }
                    tempScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                    done = true;

                    playerNames.text = tempPlayerNames;
                    playerScores.text = tempScores;
                }
            }
            else
            {
                Debug.Log("Failed fetching high scores");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
