using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Camera mainCamera;

    private Vector3 worldPosition = Vector3.zero;
    private Vector3 previousWorldPosition = Vector3.zero;

    public Vector3 SwipeDirection { get; private set; }

    void Start()
    {
        #region Initialize Member Variables
        mainCamera = Camera.main;
        worldPosition = Vector3.zero;
        previousWorldPosition = Vector3.zero;
        SwipeDirection = Vector3.zero;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        InputManager.Instance.ReadInput();

        SwipeDirection = GetSwipeDirection();
    }

    private void LateUpdate()
    {
        previousWorldPosition = worldPosition;
    }


    private Vector3 GetSwipeDirection()
    {
        worldPosition = mainCamera.ScreenToWorldPoint(InputManager.Instance.Position);
        return (worldPosition - previousWorldPosition).normalized;
    }
}
