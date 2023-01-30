using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IUpdate
{
    public Transform[] spawnPos;
    public PlayerController player;

    public float timeBetweenEnemies = 1f;
    float spawnTimer = 0;
    public Enemy[] enemyPfs;
    public Boss[] bossPfs;

    public float dificultyTimer = 0;
    public float spawnRateTimer = 0;

    private void Start()
    {
        UpdateManager.Instance.AddUpdate(this);
    }
    public void OnUpdate()
    {  
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= timeBetweenEnemies)
        {
            spawnTimer = 0;
            SpawnEnemy();
        }
        Dificulty();
    }

    public void SpawnEnemy()
    {
        int rand = Random.Range(0, spawnPos.Length);
        Transform t = spawnPos[rand];

        Enemy e = GameManager.Instance.objectPool.SpawnPooledObject(enemyPfs[Random.Range(0, enemyPfs.Length)], t.position, Quaternion.identity);
        e.SetTarget(player.transform);
        e.LevelUpEnemy(spawnLevel);
    }

    public int spawnLevel = 0;
    public void Dificulty()
    {
        dificultyTimer += Time.deltaTime;

        if(dificultyTimer >= 60)
        {
            dificultyTimer = 0;
            spawnLevel++;

            if(spawnLevel % 3 == 0)
            {
                int rand = Random.Range(0, spawnPos.Length);
                Transform t = spawnPos[rand];
                Boss e = GameManager.Instance.objectPool.SpawnPooledObject(bossPfs[Random.Range(0, bossPfs.Length)], t.position, Quaternion.identity);
                e.LevelUpEnemy(spawnLevel);
                e.SetTarget(player.transform);
            }
        }

        spawnRateTimer += Time.deltaTime;
        if (spawnRateTimer >= 5.2f)
        {
            spawnRateTimer = 0;
            timeBetweenEnemies = Mathf.Clamp(timeBetweenEnemies - 0.01f, 0.05f, 2);
        }
    }
}
