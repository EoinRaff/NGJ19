using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotateBySwipe : MonoBehaviour
{

    float angularAcceleration = 0;
    float angularVelocity = 0;
    float angle = 0;
    float drag;
    public float swipePower;

    void Update()
    {
        if (angle > 360 || angle < -360)
        {
            angle = 0;
        }

        if (InputManager.Instance.Touch.phase == TouchPhase.Moved)
        {
            Vector3 swipeDirection = InputManager.Instance.Direction;
            float mag;
            if (Vector3.Dot(Vector3.right, InputManager.Instance.Direction) > 0)
            {
                mag = InputManager.Instance.Direction.magnitude * -1;
            }
            else
            {
                mag = InputManager.Instance.Direction.magnitude;
            }
            angularAcceleration = swipePower * mag * Time.deltaTime;
        }
        else
        {
            angularAcceleration = 0.0f;
        }

        float drag = angularVelocity * -0.1f;

        angularVelocity += angularAcceleration;
        angularVelocity += drag;


        transform.Rotate(transform.up, angularVelocity);


    }
}
