using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{

    void Update()
    {

        if (InputManager.Instance.Hold)
        {
            gameObject.transform.Rotate(GameManager.Instance.SwipeDirection);
        }
    }



}
