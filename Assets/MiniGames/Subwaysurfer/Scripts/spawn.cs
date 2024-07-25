using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array of obstacle prefabs to spawn
    public Transform[] spawnPoints; // Array of lane positions for obstacle spawning
    public float spawnInterval = 2f; // Time interval between spawns
    public Transform movesobj;
    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnObstacle();
            timer = spawnInterval;
        }
    }

    void SpawnObstacle()
    {
        int laneIndex = Random.Range(0, spawnPoints.Length); // Randomly select a lane
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length); // Randomly select an obstacle

      GameObject spawnedobj= Instantiate(obstaclePrefabs[obstacleIndex], spawnPoints[laneIndex].position, Quaternion.identity);

        spawnedobj.transform.SetParent(movesobj);
    }
}

