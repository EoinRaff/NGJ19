using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{

    //public bool Touch = false;
    public bool Tap { get; private set; }
    public bool Hold { get; private set; }
    public Vector3 Position { get; private set; }

    public void ReadInput()
    {
        //Touch = Input.GetMouseButtonDown(0);
        Hold = Input.GetMouseButton(0);
        Tap = Input.GetMouseButtonUp(0);
        Position = Input.mousePosition;
    }
}
