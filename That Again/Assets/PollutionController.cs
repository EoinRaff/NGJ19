using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollutionController : MonoBehaviour
{
    public SpriteRenderer pollutionSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pollutionSprite.color = new Color(1, 1, 1, GameManager.Instance.CurrentTemperature / GameManager.Instance.MaxTemperature);
    }
}
