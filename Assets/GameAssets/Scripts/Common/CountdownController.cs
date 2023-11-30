using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownController : MonoBehaviour
{
    public GameObject UI;
    public float timeBetween = 0.5f;
    public string NextScene = "mapMenu";
    public float miniGameTime = 5f;

    private bool start;
    private Text Countdown;
    private Text Description;
    private GameObject Icon;

    private void Start()
    {
        timeBetween = (10f / miniGameTime) * timeBetween;

        Countdown = UI.transform.Find("Countdown").GetComponent<Text>();
        Description = UI.transform.Find("Description").GetComponent<Text>();
        Icon = UI.transform.Find("Icon").gameObject;

        start = false;

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

        UI.SetActive(false);
        start = true;
        StartCoroutine(Timer());


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
        SceneManager.LoadScene(NextScene);

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
    public bool isRunning()
    {
        return start;
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(miniGameTime);
        endGame();
    }
}
