using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public Transform obstaclePrefab;
    // Start is called before the first frame update
    [Header("Refs")]
    public MeshFilter meshfilter;
    private List<Vector3> world_v;
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
    }

    IEnumerator Generator()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
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
