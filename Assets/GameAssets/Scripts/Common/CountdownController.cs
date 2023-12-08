using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class CountdownController : MonoBehaviour
{
    public GameObject UI;
    public float timeBetween = 0.5f;
    public float miniGameTime;

    private Text Countdown;
    private GameObject CountdownComponent;
    private GameObject RopeTime;
    private Text Description;
    private Text PlayerName;
    private Text ScoreText;
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
        //Find all the Objects for the countdown
        CountdownComponent = UI.transform.Find("Countdown").gameObject;
        RopeTime = UI.transform.Find("Rope").gameObject;
        Countdown = CountdownComponent.transform.Find("Countdown").GetComponent<Text>();
        Description = CountdownComponent.transform.Find("Description").GetComponent<Text>();
        PlayerName = CountdownComponent.transform.Find("PlayerName").GetComponent<Text>();
        ScoreText = CountdownComponent.transform.Find("Score").GetComponent<Text>();
        Icon = CountdownComponent.transform.Find("Icon").gameObject;
        RopeTime.SetActive(false);
        UI.SetActive(true);

        PlayerName.text = currentPlayer;
        timeBetween = (10f / miniGameTime) * timeBetween;
        countdownState = CountdownState.NotStarted;
        StartCoroutine(StartCountdown());
    }

    void SetCountdownText(string text)
    {
        Countdown.text = text;
    }

    //End Game to progess to the next minigame
    public void endGame(string NextScene, string currentPlayer, int score)
    {
        Countdown.text = "";
        Icon.SetActive(false);
        PlayerName.text = currentPlayer;
        Description.text = "Pass the Phone!";
        ScoreText.text = "You got:" + score;
        CountdownComponent.SetActive(true);
        StartCoroutine(EndCountdown(NextScene));
    }
    //End Game due to losing all your lives, go back to menu
    public void lostGame(int score)
    {
        Countdown.text = "";
        Icon.SetActive(false);
        Description.text = "You Failed with a score of " + score;
        CountdownComponent.SetActive(true);
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
        RopeTime.SetActive(true);

        float elapsedTime = 0f;
        float initialFillAmount = RopeTime.GetComponent<Image>().fillAmount;

        while (elapsedTime < miniGameTime)
        {
            elapsedTime += Time.deltaTime;
            float newFillAmount = Mathf.Lerp(initialFillAmount, 0f, elapsedTime / miniGameTime);
            RopeTime.GetComponent<Image>().fillAmount = newFillAmount;
            yield return null;
        }
        countdownState = CountdownState.TimeIsUp;
    }


    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(timeBetween);
        SceneManager.LoadScene("mainMenu");
    }
    IEnumerator StartCountdown()
    {
        CountdownComponent.SetActive(true);

        SetCountdownText("3..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("2..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("1..");
        yield return new WaitForSeconds(timeBetween);
        SetCountdownText("Go!");
        yield return new WaitForSeconds(timeBetween);

        CountdownComponent.SetActive(false);
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
