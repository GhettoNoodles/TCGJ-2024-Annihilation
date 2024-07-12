using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public float spawnInterval = 2f;
    public float xMin, xMax, yMin, yMax;

    void Start()
    {
        StartCoroutine(SpawnTargets());
    }

    IEnumerator SpawnTargets()
    {
        while (true)
        {
            SpawnTarget();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnTarget()
    {
        float xPosition = Random.Range(xMin, xMax);
        float yPosition = Random.Range(yMin, yMax);
        Vector2 spawnPosition = new Vector2(xPosition, yPosition);
        Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a rectangle in the scene view to show the spawn area
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2((xMin + xMax) / 2, (yMin + yMax) / 2), new Vector2(xMax - xMin, yMax - yMin));
    }
}