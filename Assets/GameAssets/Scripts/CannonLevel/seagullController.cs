using System.Collections.Generic;
using UnityEngine;
using System;

public class seagullController : MonoBehaviour, IMiniGamesController
{
    private VocalController voiceController;
    private int score = 0;
    public string NextScene;
    [SerializeField] private List<float> thresholds;
    [SerializeField] private seagullMovement[] Seagulls;
    [SerializeField] private GameObject seagullPrefab;
    private float flySpeed = 5.0f;
    private float timeRatio;
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
        timeRatio = 10.0f/Starter.miniGameTime;
        flySpeed = Math.Max(timeRatio*flySpeed, flySpeed);
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
    //Done with Interface Functions

    private void Start()
    {
        voiceController = gameObject.AddComponent<VocalController>();
    }
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