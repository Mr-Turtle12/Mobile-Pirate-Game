using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class pirateController : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private GameObject piratePrefab; 
    [SerializeField] private Slicer playerSlicer;
    [SerializeField] private pirateMovement mover;
    private float timeRatio;
    private float nextSpawnTime = 0f;
    private float spawnInterval = 1.8f;
    public CountdownController Starter;

    public void Start()
    {
        timeRatio = 10.0f/Starter.miniGameTime;
        spawnInterval = Math.Min(spawnInterval/timeRatio, spawnInterval);
    }

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore()
    {
        score += 40;
    }

    void Update()
    {
        if (Starter.isRunning() && Time.time >= nextSpawnTime)
        {
            SpawnPirate();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnPirate()
    {
        GameObject newPirate = Instantiate(piratePrefab, GetRandomSpawnPosition(), Quaternion.identity);
        pirateMovement pirateScriptComponent = newPirate.GetComponent<pirateMovement>();
        pirateScriptComponent.SetController(this);
        pirateScriptComponent.OnHit += IncreaseScore;
    }

    Vector3 GetRandomSpawnPosition()
    {
        float spawnX = UnityEngine.Random.Range(-1f, 2.5f);
        float spawnY = 9f;
        return new Vector3(spawnX, spawnY, 0);
    }
}