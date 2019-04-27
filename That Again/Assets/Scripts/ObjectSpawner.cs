using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : Singleton<ObjectSpawner>
{
    [Header("Prefabs")]
    public Transform obstaclePrefab;

    public List<Transform> spawnPoints;

    public float spawnDelay = 5.0f;
    public float SpawnRate = 1.0f;
    int prevRngIndex;


    void Start()
    {

        if(spawnPoints != null)
        {
            StartCoroutine(Generator());
        }
    }

    IEnumerator Generator()
    {
        while (true)
        {
            int rngIndex = Random.Range(0, spawnPoints.Count);
            if(rngIndex == prevRngIndex)
            {
                rngIndex = Random.Range(0, spawnPoints.Count);
            }
//            Instantiate(obstaclePrefab, spawnPoints[rngIndex].position, spawnPoints[rngIndex].rotation, transform);
            Instantiate(obstaclePrefab, spawnPoints[rngIndex]);
            prevRngIndex = rngIndex;
            GameManager.Instance.IncrementObstacles();
            yield return new WaitForSeconds(spawnDelay * SpawnRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
