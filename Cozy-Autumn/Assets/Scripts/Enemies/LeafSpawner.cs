using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour
{
    public Transform[] spawnPos;
    public PlayerController player;

    public float timeBetweenLeaves = 1f;
    float spawnTimer = 0;
    public Leaf leafPF;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= timeBetweenLeaves)
        {
            spawnTimer = 0;
            SpawnLeaf();
        }        
    }

    public void SpawnLeaf()
    {
        int rand = Random.Range(0, spawnPos.Length);
        Transform t = spawnPos[rand];
        Vector3 rot = new Vector3(0,0,Random.Range(-180f,180f));

        GameManager.Instance.objectPool.SpawnPooledObject(leafPF, t.position, Quaternion.Euler(rot)).SetTarget(player.transform);
    }
}
