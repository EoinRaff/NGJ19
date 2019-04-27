using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool Tap { get; private set; }
    public bool Hold { get; private set; }
    public Vector3 Position { get; private set; }
    public Vector2 Direction { get; private set; }

#if UNITY_ANDROID
    private Touch touch;
#endif

    public void ReadInput()
    {

#if UNITY_ANDROID
        Tap = false;

        if (Input.touchCount <= 0)
            return;


        touch = Input.GetTouch(0);
        Direction = touch.position - touch.deltaPosition;
        switch (touch.phase)
        {
            case TouchPhase.Began:
                Hold = true;
                Tap = false;
                Position = new Vector3(touch.position.x, touch.position.y, 0);
                break;
            case TouchPhase.Moved:
                Hold = true;
                Tap = false;
                Position = new Vector3(touch.position.x, touch.position.y, 0);
                break;
            case TouchPhase.Stationary:
                Hold = true;
                Tap = false;
                Position = new Vector3(touch.position.x, touch.position.y, 0);
                break;
            case TouchPhase.Ended:
                Hold = false;
                Tap = true;
                Position = new Vector3(touch.position.x, touch.position.y, 0);
                break;
            case TouchPhase.Canceled:
                Hold = false;
                Tap = false;
                Position = new Vector3(touch.position.x, touch.position.y, 0);
                break;
            default:
                Hold = false;
                Tap = false;
                Position = new Vector3(touch.position.x, touch.position.y, 0);
                break;
        }
#endif

#if UNITY_STANDALONE
        Hold = Input.GetMouseButton(0);
        Tap = Input.GetMouseButtonUp(0);
        Position = Input.mousePosition;
        Direction = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

#endif

#if UNITY_WEBGL
        Hold = Input.GetMouseButton(0);
        Tap = Input.GetMouseButtonUp(0);
        Position = Input.mousePosition;
        Direction = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

#endif

#if UNITY_EDITOR
        Hold = Input.GetMouseButton(0);
        Tap = Input.GetMouseButtonUp(0);
        Position = Input.mousePosition;
        Direction = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
#endif

    }
}
