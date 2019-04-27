using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceManager : Singleton<AudioSourceManager>
{
    public float CrossfadeSpeed = 1;
    public AudioClip one;
    public AudioClip two;
    public AudioClip three;
    public AudioClip four;
    public AudioClip five;
    public AudioClip six;

    public DoubleAudioSource dAS;
    public AudioSource soundEffectASource;

    public void PlayEffect(AudioClip ac)
    {
        soundEffectASource.PlayOneShot(ac);
    }

    public void CrossTo(AudioClip clip)
    {
        dAS.CrossFade(clip, 1, 1);
    }

    bool range1 = false;
    bool range2 = false;
    bool range3 = false;
    bool range4 = false;
    bool range5 = false;


    public int Soundrange1 = 10;
    public int Soundrange2 = 25;
    public int Soundrange3 = 40;
    public int Soundrange4 = 60;
    public int Soundrange5 = 80;
    //int Soundrange6 = 85;

    public void Reset()
    {
         range1 = false;
         range2 = false;
         range3 = false;
         range4 = false;
         range5 = false;
    }

    public void CheckGameRange()
    {
        if (GameManager.Instance.CurrentTemperature > Soundrange1 && GameManager.Instance.CurrentTemperature < Soundrange2 && !range1)
        {
            CrossTo(two);
            range1 = true;
        }
        else if (GameManager.Instance.CurrentTemperature > Soundrange2 && !range2)
        {
           CrossTo(Instance.three);
            range2 = true;
        }
        else if (GameManager.Instance.CurrentTemperature > Soundrange3 && !range3)
        {
            CrossTo(Instance.four);
            range3 = true;
        }
        else if (GameManager.Instance.CurrentTemperature > Soundrange4 && !range4)
        {
            CrossTo(Instance.five);
            range4 = true;
        }

        if (GameManager.Instance.gameOver && !range5)
        {
            CrossTo(Instance.six);
            range5 = true;
        }
        //else if (GameManager.Instance.CurrentTemperature > Soundrange5 && !range5)
        //{
        //    CrossTo(Instance.six);
        //    range5 = true;
        //}
    }
}
