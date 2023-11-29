using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class VocalController : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isListening = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.25f);
        isListening = false;
    }

    public List<bool> CheckVoiceThreshold(List<float> thresholds)
    {
        List<bool> thresholdResults = new List<bool>();

        if (!isListening)
        {
            float[] data = new float[256];
            audioSource.GetOutputData(data, 0);

            float average = 0;
            foreach (float s in data)
            {
                average += Mathf.Abs(s);
            }
            average /= 256;

            foreach (float threshold in thresholds)
            {
                thresholdResults.Add(average > threshold);
            }

            isListening = true;
            StartCoroutine(Timer());
        }

        return thresholdResults;
    }
}

