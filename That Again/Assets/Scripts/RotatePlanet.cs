using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    void Update()
    {
        if (InputManager.Instance.Touch.phase != TouchPhase.Moved)
        {
            return;
        }
        Vector3 eulerAngles = new Vector3(GameManager.Instance.SwipeDirection.y, -GameManager.Instance.SwipeDirection.x, 0);
        gameObject.transform.Rotate(eulerAngles * rotationSpeed, Space.World);
    }



}
