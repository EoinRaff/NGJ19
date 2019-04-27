using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        if (Input.GetMouseButton(0))
        {
            float h = GameManager.Instance.SwipeDirection.x;
            float v = GameManager.Instance.SwipeDirection.y;


            transform.position += new Vector3(h, 0, v) * Time.deltaTime;
        }
     
        //transform.position += GameManager.Instance.SwipeDirection;
    }
}
