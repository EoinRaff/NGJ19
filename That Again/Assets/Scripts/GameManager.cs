using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private const int maxSpawnRate = 5;
    private Camera mainCamera;

    private Vector3 screenPosition;
    private Vector3 worldPosition;
    private Vector3 previousWorldPosition;

    public Vector3 SwipeDirection { get; private set; }

    [Header("Game Difficulty Settings")]
    public float spawnRate = maxSpawnRate;
    public float spawnRateModifier = 0.01f;
    private float minSpawnRate = 2.5f;


    [Header("Game State Variables")]
    public float MaxTemperature;
    public float CurrentTemperature;
    public int currentObstacles;
    public float gameTime = 0;
    public float SecondsPerYear = 5.0f;

    float delayTimer = 0;
    public int yearsPassed = 0;
    [Range(0.1f, 2)]
    public float temperatureIncreaseRate = 0.5f;
    public float minTempIncreaseRate, maxTemperatureIncreaseRate;


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
        //temperatureIncreaseRate = Mathf.Lerp(minTempIncreaseRate, maxTemperatureIncreaseRate, currentObstacles/ObjectSpawner.Instance.spawnPoints.Count);
        CurrentTemperature += currentObstacles * temperatureIncreaseRate* Time.deltaTime;
        if (CurrentTemperature >=99)
        {
            CurrentTemperature = MaxTemperature;
        }
    }



    void IncrementSpawnRate()
    {
        int maxFactories = ObjectSpawner.Instance.OilRigsEnabled ? ObjectSpawner.Instance.spawnPoints.Count + ObjectSpawner.Instance.spawnPointsWater.Count : ObjectSpawner.Instance.spawnPoints.Count;
        int currentFactories = currentObstacles;

        float ratio =  5- ((float)currentFactories / (float)maxFactories);

       
        ObjectSpawner.Instance.SpawnRate = ratio;
        Debug.Log(((float)currentFactories / (float)maxFactories));
        //ObjectSpawner.Instance.SpawnRate = Mathf.Clamp(ObjectSpawner.Instance.SpawnRate, minSpawnRate, maxSpawnRate);


    }
    void DecrementSpawnRate()
    {
        ObjectSpawner.Instance.SpawnRate += spawnRateModifier;
    }


    public void IncrementObstacles()
    {
        currentObstacles++;
        int maxFactories = ObjectSpawner.Instance.OilRigsEnabled ? ObjectSpawner.Instance.spawnPoints.Count + ObjectSpawner.Instance.spawnPointsWater.Count : ObjectSpawner.Instance.spawnPoints.Count;

        currentObstacles = Mathf.Clamp(currentObstacles, 0, maxFactories);
    }

    public void DecrementObstacles()
    {
        int maxFactories = ObjectSpawner.Instance.OilRigsEnabled ? ObjectSpawner.Instance.spawnPoints.Count + ObjectSpawner.Instance.spawnPointsWater.Count : ObjectSpawner.Instance.spawnPoints.Count;
        currentObstacles--;
        currentObstacles = Mathf.Clamp(currentObstacles, 0, maxFactories);
    }


    void Update()
    {
        InputManager.Instance.ReadInput();
    
        //SwipeDirection = GetSwipeVector();
        gameTime += Time.deltaTime;
        delayTimer += Time.deltaTime;
        if (gameOver)
            return;

        if(delayTimer > SecondsPerYear)
        {
            //Every 10 Seconds Increment Spawn Rate
            Debug.Log("Increasing SpawnRate");
            IncrementSpawnRate();
            delayTimer = 0;
        }

        if (GameOver())
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;
            UIController.Instance.EnableGameOverMenu();
            
            //Game Over!
        }
        else
        {
            IncrementTemperature();
        }

        AudioSourceManager.Instance.CheckGameRange();

        yearsPassed = (int)Mathf.Floor( gameTime / SecondsPerYear);

    }



    public void Reset()
    {
        Time.timeScale = 1;
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
        AudioSourceManager.Instance.Reset();
    }

    private Vector3 GetSwipeVector()
    {
        previousWorldPosition = worldPosition;
        screenPosition = new Vector3(InputManager.Instance.Position.x, InputManager.Instance.Position.y, mainCamera.nearClipPlane);
        worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition - previousWorldPosition;
    }

}
