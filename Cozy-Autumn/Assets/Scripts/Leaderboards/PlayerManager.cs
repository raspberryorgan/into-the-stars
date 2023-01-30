using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoginRoutine());  
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;

        LootLockerSDKManager.StartGuestSession((response) => 
        {
            if (response.success)
            {
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                done = true;
            }

        });

        yield return new WaitWhile(() => done == false);


        if (GameManager.Instance.leaderboard != null)
            GameManager.Instance.leaderboard.FetchHighScores();
    }
}
