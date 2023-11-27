using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class seagullController : MonoBehaviour
{
    public AudioSource audioSource;
    public flyAway[] Seagulls = new flyAway[4];

    public float[] thresholds = new float[4] { 0.01f, 0.05f, 0.09f, 0.13f };
    private bool Stop = false;
    public CountdownController Starter;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();

        StartCoroutine(StartGameTimer());
    }

    IEnumerator StartGameTimer()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        Starter.endGame(); // Call endGame() after 5 seconds
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        Stop = true;
    }

    void Update()
    {
        if (Starter.isRunning() && !Stop)
        {
            float[] data = new float[256];
            audioSource.GetOutputData(data, 0);

            float average = 0;
            foreach (float s in data)
            {
                average += Mathf.Abs(s);
            }
            average /= 256;

            for (int i = 0; i < 4; i++)
            {
                if (average > thresholds[i])
                {
                    Seagulls[i].OnFly();
                    StartCoroutine(Timer());
                }
            }
        }
    }
}