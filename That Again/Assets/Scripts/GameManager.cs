using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Camera mainCamera;

    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Vector3 previousWorldPosition;

    public Vector3 SwipeDirection { get; private set; }

    [Header("Game State Variables")]
    public float MaxTemperature;
    public float CurrentTemperature;

    public int currentObstacles;

    public float gameTime = 0;

    public bool gameOver;

    void Start()
    {
        #region Initialize Member Variables
        mainCamera = Camera.main;

        screenPosition = Vector3.zero;
        worldPosition = Vector3.zero;
        previousWorldPosition = Vector3.zero;

        SwipeDirection = Vector3.zero;
        #endregion

        UIController.Instance.Reset.enabled = false;

    }

    public bool GameOver()
    {
        if(CurrentTemperature >= MaxTemperature)
        {
            return true;
        }
        return false;
    }

    void IncrementTemperature()
    {
        CurrentTemperature += currentObstacles * Time.deltaTime;
    }

    void IncrementSpawnRate()
    {
        ObjectSpawner.Instance.SpawnRate -= 0.1f;
    }
    void DecrementSpawnRate()
    {
        ObjectSpawner.Instance.SpawnRate += 0.1f;
    }


    public void IncrementObstacles()
    {
        currentObstacles++;
    }

    public void DecrementObstacles()
    {
        currentObstacles--;
    }

    void Update()
    {
        InputManager.Instance.ReadInput();
    
        SwipeDirection = GetSwipeDirection();

        gameTime += Time.deltaTime;

        if(gameTime % 10.0f == 0)
        {
            //Every 10 Seconds Increment Spawn Rate
            Debug.Log("Increasing SpawnRate");
            IncrementSpawnRate();
        }

        if (GameOver())
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;

            UIController.Instance.Reset.enabled = true;
        }
        else
        {
            IncrementTemperature();
        }
    }

    public void Reset()
    {
        UIController.Instance.Reset.enabled = false;
        Time.timeScale = 1;
        currentObstacles = 0;
        CurrentTemperature = 0;
        gameOver = false;
        gameTime = 0;
    }

    private Vector3 GetSwipeDirection()
    {
        previousWorldPosition = worldPosition;
        screenPosition = new Vector3(InputManager.Instance.Position.x, InputManager.Instance.Position.y, mainCamera.nearClipPlane);
        worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        return (worldPosition - previousWorldPosition).normalized;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Current Obstacles" + currentObstacles);
        GUI.Label(new Rect(10, 10, 100, 20), "Current Temperature " + CurrentTemperature);
        //if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
        //{
        //    print("You clicked the button!");
        //}
    }
}
