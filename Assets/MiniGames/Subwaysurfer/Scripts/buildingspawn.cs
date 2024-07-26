using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingspawn : MonoBehaviour

{
    [SerializeField]
    private Building[] squarePrefabs; // Array of square prefabs with varying sizes
    private Vector3 nextSpawnPosition;
    private bool canSpawn=true;
    [SerializeField]
    float spawnCooldown = 0.3f;
    public Transform movesobj;

    [System.Serializable]
    public struct Building
    {
        public float height;
        public GameObject buildingObject;

        public Building(float height, GameObject buildingObject)
        {
            this.height = height;
            this.buildingObject = buildingObject;
        }
    }
    void Start()
    {
        Debug.Log("start");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canSpawn)
        {
            Debug.Log("exited");
            StartCoroutine(SpawnBuilding());
        }
    }

    private IEnumerator SpawnBuilding()
    {
        canSpawn = false;
        Building squarePrefab = squarePrefabs[Random.Range(0, squarePrefabs.Length)];
        nextSpawnPosition = new Vector3(transform.position.x, transform.position.y + squarePrefab.height);
       GameObject builobj= Instantiate(squarePrefab.buildingObject, nextSpawnPosition, Quaternion.identity);
        builobj.transform.SetParent(movesobj);
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }

}

