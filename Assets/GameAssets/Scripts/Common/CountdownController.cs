using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownController : MonoBehaviour
{
    public GameObject UI;
    public float timeBetween = 0.5f;
    public float miniGameTime;

    private Text Countdown;
    private Text Description;
    private Text PlayerName;
    private GameObject Icon;
    private CountdownState countdownState;
    private enum CountdownState
    {
        NotStarted,
        GameRunning,
        TimeIsUp
    }

    public void StartGame(string currentPlayer)
    {

        Countdown = UI.transform.Find("Countdown").GetComponent<Text>();
        Description = UI.transform.Find("Description").GetComponent<Text>();
        PlayerName = UI.transform.Find("PlayerName").GetComponent<Text>();
        PlayerName.text = currentPlayer;
        Icon = UI.transform.Find("Icon").gameObject;
        timeBetween = (10f / miniGameTime) * timeBetween;
        countdownState = CountdownState.NotStarted;
        StartCoroutine(StartCountdown());
    }

    void SetCountdownText(string text)
    {
        Countdown.text = text;
    }

    //End Game to progess to the next minigame
    public void endGame(string NextScene, string currentPlayer)
    {
        Countdown.text = "";
        Icon.SetActive(false);
        PlayerName.text = currentPlayer;
        Description.text = "Pass the Phone!";
        UI.SetActive(true);
        StartCoroutine(EndCountdown(NextScene));
    }
    //End Game due to losing all your lives, go back to menu
    public void lostGame(int score)
    {
        Countdown.text = "";
        Icon.SetActive(false);
        Description.text = "You Failed with a score of " + score;
        UI.SetActive(true);
        StartCoroutine(WaitTime());
    }
    public bool TimerUp()
    {
        return countdownState == CountdownState.TimeIsUp;
    }
    public bool StartMiniGame()
    {
        return countdownState == CountdownState.GameRunning;
    }
    IEnumerator Timer()
    {
        Debug.Log(miniGameTime);
        yield return new WaitForSeconds(miniGameTime);
        countdownState = CountdownState.TimeIsUp;
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(timeBetween);
        SceneManager.LoadScene("mainMenu");
    }
    IEnumerator StartCountdown()
    {
        UI.SetActive(true);

        SetCountdownText("3..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("2..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("1..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("Go!");
        yield return new WaitForSeconds(timeBetween);

        UI.SetActive(false);
        countdownState = CountdownState.GameRunning;
        StartCoroutine(Timer());
    }
    IEnumerator EndCountdown(string NextScene)
    {

        SetCountdownText("3..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("2..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("1..");
        yield return new WaitForSeconds(timeBetween);
        SceneManager.LoadScene(NextScene);
    }
}
