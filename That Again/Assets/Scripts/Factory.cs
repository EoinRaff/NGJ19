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

    public float activateDelayTime = 5.0f;
    float delayTimer;
    public bool delayed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delayCounter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator delayCounter()
    {
        while (true)
        {
            if (delayed)
            {
                delayTimer += Time.deltaTime;
                if(delayTimer > activateDelayTime)
                {
                    delayed = false;
                    delayTimer = 0;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void SetToActive()
    {
        if (!delayed)
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
    }

    public void SetToInactive()
    {
        delayed = true;

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
