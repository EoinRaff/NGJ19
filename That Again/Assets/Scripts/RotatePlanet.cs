using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    void Update()
    {
        gameObject.transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
    }



}
