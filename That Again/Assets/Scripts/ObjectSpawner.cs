using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : Singleton<ObjectSpawner>
{
    [Header("Prefabs")]
    public Transform obstaclePrefab;

    public List<Sprite> obstacleSprites;
    // Start is called before the first frame update
    [Header("Refs")]
    public MeshFilter meshfilter;
    private List<Vector3> world_v;

    public List<Transform> spawnPoints;


    public Dictionary<Vector3, Transform> spawnLocations;
    public List<Factory> spawnedFactories;

    public float spawnDelay = 5.0f;
    public float SpawnRate = 0.0f;
    void Start()
    {


        if (spawnPoints != null)
        {
            IntializeDictionary();

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

    public Factory GetRandomFactory()
    {
        int rngIndex = Random.Range(0, spawnPoints.Count);
        if (rngIndex == prevRngIndex)
        {
            rngIndex = Random.Range(0, spawnPoints.Count);
        }
        Vector3 location = spawnPoints[rngIndex].position;
        Factory f = spawnedFactories[rngIndex];
        if(obstacleSprites != null)
        {
            f.rend.sprite = obstacleSprites[Random.Range(0, obstacleSprites.Count)];
        }
        //spawnedFactories.TryGetValue(location, out f);
        prevRngIndex = rngIndex;
        return f;
    }

    int prevRngIndex;
    IEnumerator Generator()
    {
        while (true)
        {
            Factory f = GetRandomFactory();

            if (f.isActive)
            {
                f = GetRandomFactory();
            }

            f.SetToActive();
            //GameManager.Instance.IncrementObstacles();
            yield return new WaitForSeconds(spawnDelay + SpawnRate);
            //yield return new WaitForEndOfFrame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
