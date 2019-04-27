using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource aSource;
    public AudioClip ObstacleConstructed;
    // Start is called before the first frame update


    public void PlayObstacleConstructed()
    {
        aSource.PlayOneShot(ObstacleConstructed);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
