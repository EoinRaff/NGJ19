using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{

    void Update()
    {

        print(string.Format("Tap: {0}, Hold: {1}", InputManager.Instance.Tap, InputManager.Instance.Hold));

        if (InputManager.Instance.Hold)
        {
            gameObject.transform.Rotate(GameManager.Instance.SwipeDirection);
        }
    }



}
