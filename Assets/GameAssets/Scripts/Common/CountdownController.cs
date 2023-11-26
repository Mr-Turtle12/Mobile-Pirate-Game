using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownController : MonoBehaviour
{
    public GameObject UI;
    public bool start;
    private float timeBetween = 0.4f;
    private Text Countdown;
    private Text Description;
    private GameObject Icon;

    private void Start()
    {
        Countdown = UI.transform.Find("Countdown").GetComponent<Text>();
        Description = UI.transform.Find("Description").GetComponent<Text>();
        Icon = UI.transform.Find("Icon").gameObject;

        start = false;

        // Start the countdown
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        SetCountdownText("3..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("2..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("1..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("Go!");
        yield return new WaitForSeconds(timeBetween);

        // Enable the 'game' GameObject and disable the 'ui' GameObject
        UI.SetActive(false);
        start = true;

    }
    IEnumerator EndCountdown()
    {

        SetCountdownText("3..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("2..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("1..");
        yield return new WaitForSeconds(timeBetween);

        //Next Level but for time being going back to map:
        SceneManager.LoadScene("mapMenu");

    }

    void SetCountdownText(string text)
    {
        Countdown.text = text;
    }
    public void endGame()
    {
        start = false;
        Countdown.text = "";
        Icon.SetActive(false);
        Description.text = "Pass the Phone!";
        UI.SetActive(true);
        StartCoroutine(EndCountdown());
    }
}
