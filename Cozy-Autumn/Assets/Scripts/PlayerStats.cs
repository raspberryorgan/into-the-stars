using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int kills = 0;
    public int bossKilled = 0;
    public float timeSurvived;
    public int score;
    public float gold;

    public int killsPoints = 100;
    public int bossPoints = 5000;
    public int secondsSurvivedPoints = 50;

    public void Reset()
    {
        kills = 0;
        bossKilled = 0;
        timeSurvived = 0;
        score = 0;
        gold = 0;
    }
    public void UpdateTime()
    {
        timeSurvived += Time.deltaTime;
        score = TotalScore();
    }
    public int killScore;
    public int timeScore;
    public int bossScore;
    public int TotalScore()
    {
        killScore = kills * killsPoints;
        timeScore = (int)timeSurvived * secondsSurvivedPoints;
        bossScore = bossKilled * bossPoints;
        score = killScore + timeScore + bossScore;
        return score;
    }
}
