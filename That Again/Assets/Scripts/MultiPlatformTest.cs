using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiPlatformTest : MonoBehaviour
{
    public TextMeshProUGUI textField;
    private string message;

    private InputManager inputManager;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        message = string.Format("Tap: {0} \nHold: {1} \nPosition: {2}", inputManager.Tap, inputManager.Hold, inputManager.Position);
        textField.text = message;
    }

}
