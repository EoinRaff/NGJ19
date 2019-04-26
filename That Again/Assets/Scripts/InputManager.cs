using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool Tap { get; private set; }
    public bool Hold { get; private set; }
    public Vector3 Position { get; private set; }

    public void ReadInput()
    {
        Hold = Input.GetMouseButton(0);
        Tap = Input.GetMouseButtonUp(0);
        Position = Input.mousePosition;
    }
}
