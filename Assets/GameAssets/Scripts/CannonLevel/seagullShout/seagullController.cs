using System.Collections.Generic;
using System;
using UnityEngine;

public class seagullController : MonoBehaviour, IMiniGamesController
{
    private VocalController voiceController;
    private int score = 0;
    public string NextScene;
    [SerializeField] private List<float> thresholds;
    [SerializeField] private seagullMovement[] Seagulls;
    [SerializeField] private GameObject seagullPrefab;
    private float timeRatio;
    private float flySpeed = 5.0f;
    private Coroutine spawnCoroutine;
    private float miniGameTime;
    private bool isRunning = false;

    //Interface functions
    public int GetScore()
    {
        return score;
    }
    public void SetDuration(float time)
    {
        miniGameTime = time;
        timeRatio = 10.0f / miniGameTime;
        flySpeed = Math.Max(timeRatio * flySpeed, flySpeed);
    }

    public void IsRunning(bool running)
    {
        isRunning = running;
    }
    public bool GameRunning()
    {
        return isRunning;
    }
    public string getNextScene()
    {
        return NextScene;
    }
    public void StartminiGame()
    {
        voiceController = gameObject.AddComponent<VocalController>();
    }
    //Done with Interface Functions
    public void IncreaseScore()
    {
        score += 15;
    }

    private void Update()
    {
        if (isRunning)
        {
            List<bool> thresholdResults = voiceController.CheckVoiceThreshold(thresholds);

            for (int i = 0; i < thresholdResults.Count; i++)
            {
                if (thresholdResults[i])
                {
                    Seagulls[i].FlyAway(flySpeed);
                }
            }
        }
    }
}