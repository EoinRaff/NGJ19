using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraAroundObject : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    public Transform planetTransform;
    private float angle;

    private float velocityX = 0.0f;
    private float velocityY = 0.0f;
    private float rotationYAxis;
    private float rotationXAxis;
    private float yMinLimit = -20;
    private float yMaxLimit = 20;
    private float smoothTime = 2f;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
    }

    void Update()
    {
        if (InputManager.Instance.Hold)
        {
            velocityX += rotationSpeed * GameManager.Instance.SwipeDirection.x;
            velocityY += rotationSpeed * GameManager.Instance.SwipeDirection.y;

            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
            Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;


            transform.rotation = rotation;
            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

}
