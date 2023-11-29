using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seagullController : MonoBehaviour
{
    private VocalController voiceController;
    private int score = 0;
    [SerializeField] private List<float> thresholds;
    [SerializeField] private seagullMovement[] Seagulls;
    [SerializeField] private GameObject seagullPrefab;
    private float flySpeed = 5.0f;
    private Coroutine spawnCoroutine;
    public CountdownController Starter;

    private void Start()
    {
        voiceController = gameObject.AddComponent<VocalController>();
        flySpeed = ((10.0f/Starter.miniGameTime)*flySpeed);
        Debug.Log(flySpeed);
    }

    public int GetScore()
    {
        return score;
    }

    public void IncreaseScore()
    {
        score += 15;
    }

    private void Update()
    {
        if(Starter.isRunning())
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