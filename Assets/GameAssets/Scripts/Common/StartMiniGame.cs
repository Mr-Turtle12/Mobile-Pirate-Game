using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMiniGame : MonoBehaviour
{
    public GameObject UI;
    public Text countdownText;
    public bool start;

    private void Start()
    {
        start = false;

        // Start the countdown
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(1f);
        SetCountdownText("3..");
        yield return new WaitForSeconds(1f);
        SetCountdownText("2..");
        yield return new WaitForSeconds(1f);
        SetCountdownText("1..");
        yield return new WaitForSeconds(1f);
        SetCountdownText("Go!");
        yield return new WaitForSeconds(1f);

        // Enable the 'game' GameObject and disable the 'ui' GameObject
        UI.SetActive(false);
        start = true;

    }

    void SetCountdownText(string text)
    {
        countdownText.text = text;
    }
}
