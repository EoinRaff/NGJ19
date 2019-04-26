using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Camera mainCamera;

    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Vector3 previousWorldPosition;

    public Vector3 SwipeDirection { get; private set; }

    void Start()
    {
        #region Initialize Member Variables
        mainCamera = Camera.main;

        screenPosition = Vector3.zero;
        worldPosition = Vector3.zero;
        previousWorldPosition = Vector3.zero;

        SwipeDirection = Vector3.zero;
        #endregion
    }

    void Update()
    {
        InputManager.Instance.ReadInput();
    
        SwipeDirection = GetSwipeDirection();
    }

    private Vector3 GetSwipeDirection()
    {
        previousWorldPosition = worldPosition;
        screenPosition = new Vector3(InputManager.Instance.Position.x, InputManager.Instance.Position.y, mainCamera.nearClipPlane);
        worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        return (worldPosition - previousWorldPosition).normalized;
    }
}
