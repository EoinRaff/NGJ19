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

    [Header("Game State Variables")]
    public float MaxTemperature;
    public float CurrentTemperature;

    public int currentObstacles;

    public float gameTime = 0;
    public float spawnRateIncreaseDelay = 5.0f;

    public bool gameOver;


    [Header("UI Refs")]
    public Transform GameOverMenu;

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

    float delayTimer = 0;
    void Update()
    {
        InputManager.Instance.ReadInput();
    
        SwipeDirection = GetSwipeDirection();
        gameTime += Time.deltaTime;
        delayTimer += Time.deltaTime;
        if (gameOver)
            return;

        if(delayTimer > spawnRateIncreaseDelay)
        {
            //Every 10 Seconds Increment Spawn Rate
            Debug.Log("Increasing SpawnRate");
            IncrementSpawnRate();
            delayTimer = 0;
        }

        if (GameOver())
        {
            Debug.Log("Game Over!");
            EnableGameOverMenu();
            
            //Game Over!
        }
        else
        {
            IncrementTemperature();
        }
    }

    public void EnableGameOverMenu()
    {
        GameOverMenu.transform.gameObject.SetActive(true);
        gameOver = true;
    }

    public void DisableGameOverMenu()
    {
        GameOverMenu.transform.gameObject.SetActive(false);
        Reset();
    }


    public void Reset()
    {
        foreach(Factory f in ObjectSpawner.Instance.spawnedFactories)
        {
            f.SetToInactive();
        }
        ObjectSpawner.Instance.SpawnRate = 1.0f;
        currentObstacles = 0;
        CurrentTemperature = 0;
        gameOver = false;
        delayTimer = 0;
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
        if (GUI.Button(new Rect(10, 10, 250, 20), "Current Temperature " + (int)CurrentTemperature + " : " + MaxTemperature))
        {
            print("You clicked the button!");
        }
        if (GUI.Button(new Rect(10, 30, 250, 20), "Current Obstacles" + currentObstacles))
        {
            print("You clicked the button!");
        }
       
    }
}
