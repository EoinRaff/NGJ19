using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public bool isActive;
    public Renderer rend;
    public Collider col;

    public AudioClip spawnSound;
    public AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetToActive()
    {
        isActive = true;
        rend.enabled = true;
        col.enabled = true;
        GameManager.Instance.IncrementObstacles();
        AudioSourceManager.Instance.PlayEffect(spawnSound);
    }

    public void SetToInactive()
    {
        isActive = false;
        rend.enabled = false;
        col.enabled = false;
        GameManager.Instance.DecrementObstacles();
        AudioSourceManager.Instance.PlayEffect(deathSound);
    }

    void OnMouseDown()
    {
        // load a new scene
        SetToInactive();
    }
}
