using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : Singleton<ObjectSpawner>
{
    [Header("Prefabs")]
    public Transform obstaclePrefab;

    public List<Sprite> obstacleSprites;
    public List<Transform> spawnPoints;
    public List<Transform> spawnPointsWater;

    public Dictionary<Vector3, Transform> spawnLocations;
    public List<Factory> spawnedFactories;
    public List<Factory> spawnedOilRigs;

    public float spawnDelay = 5.0f;
    public float SpawnRate = 0.0f;

    public bool OilRigsEnabled = false;

    void Start()
    {


        if (spawnPoints != null)
        {
            IntializeDictionary();

            if (OilRigsEnabled && spawnPointsWater != null)
            {
                IntializeOilRigs();
            }
            StartCoroutine(Generator());
        }
    }

    public void IntializeDictionary()
    {
        spawnedFactories = new List<Factory>();
        foreach (Transform trs in spawnPoints)
        {
            Factory f = Instantiate(obstaclePrefab,  trs).GetComponentInChildren<Factory>();
            spawnedFactories.Add(f);
        }
    }

    public void IntializeOilRigs()
    {
        spawnedOilRigs = new List<Factory>();
        foreach(Transform trs in spawnPointsWater)
        {
            Factory f = Instantiate(obstaclePrefab, trs).GetComponentInChildren<Factory>();
            f.transform.Rotate(Vector3.forward, 90);
            f.transform.Rotate(Vector3.up, 180);
            spawnedOilRigs.Add(f);
        }
    }

    public Factory GetRandomOilRig()
    {
        int rngIndex = Random.Range(0, spawnPoints.Count);
        if (rngIndex == prevRngIndex)
        {
            rngIndex = Random.Range(0, spawnPoints.Count);
        }
        Factory f = spawnedOilRigs[rngIndex];
        if (obstacleSprites.Count > 0 && f.rend.GetType() == typeof(SpriteRenderer))
        {
            ((SpriteRenderer)f.rend).sprite = obstacleSprites[0];
        }

        prevRngIndex = rngIndex;
        return f;
    }

    public Factory GetRandomFactory()
    {
        int rngIndex = Random.Range(0, spawnPoints.Count);
        if (rngIndex == prevRngIndex)
        {
            rngIndex = Random.Range(0, spawnPoints.Count);
        }
        Vector3 location = spawnPoints[rngIndex].position;
        Factory f = spawnedFactories[rngIndex];
        if (obstacleSprites.Count > 0 && f.rend.GetType() == typeof(SpriteRenderer))
        {   
            ((SpriteRenderer)f.rend).sprite = obstacleSprites[1];
        }

        prevRngIndex = rngIndex;
        return f;
    }

    int prevRngIndex;
    IEnumerator Generator()
    {
        while (true)
        {
            Factory f = GetRandomFactory();
            if (OilRigsEnabled)
            {
                int either = Random.Range(0, 2);
                Factory[] director = { f, GetRandomOilRig() };
                f = director[either];
            }
              //  Factory o = GetRandomOilRig();
            if (f.isActive)
            {
                f = GetRandomFactory();
                if (OilRigsEnabled)
                {
                    int either = Random.Range(0, 2);
                    Factory[] director = { f, GetRandomOilRig() };
                    f = director[either];
                }
            }

            f.SetToActive();
            //GameManager.Instance.IncrementObstacles();
            yield return new WaitForSeconds(spawnDelay - SpawnRate);
            //yield return new WaitForEndOfFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
