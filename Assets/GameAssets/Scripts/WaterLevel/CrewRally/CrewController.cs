using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewController : MonoBehaviour, IMiniGamesController
{
    private VocalController voiceController;
    private int score = 0;
    public string NextScene;
    [SerializeField] private List<float> thresholds;
    [SerializeField] private CrewMovement[] crews;
    private float timeRatio;
    private float moveSpeed = 5.0f;
    private bool PirateRallying = false;
    private float miniGameTime;
    private bool isRunning = true;

    //Interface functions
    public int GetScore()
    {
        return score;
    }
    public void SetDuration(float time)
    {
        miniGameTime = time;
        timeRatio = 10.0f / miniGameTime;
        moveSpeed = Math.Max(timeRatio * moveSpeed, moveSpeed);
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
    public void startedPirateRallying()
    {
        PirateRallying = true;
    }
    public void pirateRallied()
    {
        PirateRallying = false;
    }
    private void Update()
    {
        if (isRunning && !PirateRallying)
        {
            List<bool> thresholdResults = voiceController.CheckVoiceThreshold(thresholds);

            for (int i = 0; i < thresholdResults.Count; i++)
            {
                if (thresholdResults[i])
                {
                    crews[i].WalkIn(moveSpeed);
                }
            }
        }
    }
}
