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

    public ParticleSystem smokeEffect;
    public ParticleSystem deathEffect;
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
        if (smokeEffect != null)
        {
            Debug.Log("SMOKE");
            smokeEffect.Play();
        }
    }

    public void SetToInactive()
    {
    
        isActive = false;
        rend.enabled = false;
        col.enabled = false;
        GameManager.Instance.DecrementObstacles();
        AudioSourceManager.Instance.PlayEffect(deathSound);
        if (smokeEffect != null)
        {
            smokeEffect.Stop();
        }
        if (deathEffect!=null)
        {
            //FIXME
            var main = deathEffect.main;
            main.loop = false;
            deathEffect.Play();
            deathEffect.Stop();
        }
    }

    void OnMouseDown()
    {
        // load a new scene
        SetToInactive();
    }
}
