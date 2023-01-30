using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public TMP_Text counters;
    public TMP_Text scores;
    public TMP_Text finalScore;

   public void SetCounter(Vector2 timesurvived, int kills, int bossesKilled)
    {
        counters.text = timesurvived.x.ToString("00") + ":" + timesurvived.y.ToString("00") + "\n\n" +
            kills.ToString() + "\n\n" + bossesKilled.ToString();
    }




    public void SetScores(int _timeScore, int _killScore, int _bossScore)
    {
        timerScore = _timeScore;
        killScore = _killScore;
        bossScore =_bossScore;

        StartCoroutine(scoreRoutine());
    }

    int timerScore;
    int killScore;
    int bossScore;
    IEnumerator scoreRoutine()
    {
        int a = 0;
        int b = 0;
        int c = 0;

        while(a < timerScore)
        {
            a += 50;
            if (a > timerScore) a = timerScore;

            SetScoresUI(a, b, c);
            yield return new WaitForSeconds(0.01f);
        } 
        while (b < killScore)
        {
            b += 100;
            if (b > killScore) b = killScore;

            SetScoresUI(a, b, c);
            yield return new WaitForSeconds(0.02f);
        } 
        while (c < bossScore)
        {
            c += 1000;
            if (c > bossScore) c = bossScore;

            SetScoresUI(a, b, c);
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void SetScoresUI(int a, int b, int c)
    {
        scores.text = a.ToString() + "\n\n" + b.ToString() + "\n\n" + c.ToString();

        finalScore.text = (a + b + c).ToString();
    }
}
