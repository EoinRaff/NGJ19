using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwipeVelocityDebug : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Velocity: " + InputManager.Instance.Direction.magnitude;
    }
}
