using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : Singleton<ObjectSpawner>
{
    [Header("Prefabs")]
    public Transform obstaclePrefab;
    // Start is called before the first frame update
    [Header("Refs")]
    public MeshFilter meshfilter;
    private List<Vector3> world_v;

    public List<Transform> spawnPoints;


    public float spawnDelay = 5.0f;
    public float SpawnRate = 1.0f;
    void Start()
    {
        world_v = new List<Vector3>();
        Matrix4x4 localToWorld = transform.localToWorldMatrix;

        for (int i = 0; i < meshfilter.mesh.vertices.Length; ++i)
        {
            Vector3 v =(meshfilter.mesh.vertices[i]);
            v += meshfilter.transform.position;
            world_v.Add(v);
        }

        if(spawnPoints != null)
        {
            StartCoroutine(Generator());
        }
    }

    int prevRngIndex;
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
            Instantiate(obstaclePrefab, spawnPoints[rngIndex].transform);
            prevRngIndex = rngIndex;
            GameManager.Instance.IncrementObstacles();
            yield return new WaitForSeconds(spawnDelay * SpawnRate);
            //yield return new WaitForEndOfFrame();
        }
    }

    [ContextMenu("Generate Objects")]
    public void GenerateObjects()
    {
        foreach(Vector3 v in world_v)
        {
            Instantiate(obstaclePrefab, v + rngVector(), Quaternion.identity, transform);
            Instantiate(obstaclePrefab, v + rngVector(), Quaternion.identity, transform);
        }
    }

    Vector3 rngVector()
    {
        return new Vector3(Random.Range(-1.0f, 1.0f),0.0f, Random.Range(-1.0f, 1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
