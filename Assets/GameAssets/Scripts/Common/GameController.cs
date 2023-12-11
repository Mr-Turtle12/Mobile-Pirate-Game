using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private IDataCarrierScript CarrierScript;
    private CountdownController CountDownScript;
    private IMiniGamesController miniGamesController;

    public float miniGameTime;
    public GameObject[] ScoreDisplays;
    private bool[] displayScore = new bool[3] { false, false, false };

    public int progessScore = 120;


    // Start is called before the first frame update
    void Start()
    {
        //Find all the scripts from the DataCarrier and Controller
        GameObject DataCarrier = GameObject.Find("DataCarrier");
        CarrierScript = DataCarrier.GetComponent<IDataCarrierScript>();
        CountDownScript = GetComponent<CountdownController>();
        miniGamesController = GetComponent<IMiniGamesController>();
        //let all scripts know how long the game time should be
        miniGamesController.StartminiGame();
        miniGameTime = CarrierScript.GetLength();
        CountDownScript.miniGameTime = miniGameTime;
        miniGamesController.SetDuration(miniGameTime);
        switch (CarrierScript.GetGameMode())
        {
            case GameMode.Endless when CarrierScript is EndlessDataCarrier endlessCarrier:
                CountDownScript.StartGame(endlessCarrier.getCurrentPlayer());
                break;
            case GameMode.Vs when CarrierScript is VsDataCarrier vsCarrier:
                CountDownScript.StartGame(vsCarrier.getCurrentPlayer());
                break;
            case GameMode.FreePlay or GameMode.Campaign:
                CountDownScript.StartGame("");
                CountDownScript.NoTimer();
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Once the count in has finished start the mini game
        if (CountDownScript.StartMiniGame())
        {
            miniGamesController.IsRunning(true);
        }

        int score = miniGamesController.GetScore();
        if (score > 300 && !displayScore[2])
        {
            StartCoroutine(DisplayScore(2));
        }
        else if (score > 200 && !displayScore[1])
        {
            StartCoroutine(DisplayScore(1));

        }
        else if (score > 150 && !displayScore[0])
        {
            StartCoroutine(DisplayScore(0));
        }
        switch (CarrierScript.GetGameMode())
        {
            case GameMode.Endless when CarrierScript is EndlessDataCarrier endlessCarrier:
                //Endless mode can finish due to time limit/ if score reaching mini, you keep going otherwise you 
                if (score < progessScore && CountDownScript.TimerUp() && miniGamesController.GameRunning())
                {
                    CarrierScript.NextGame(score);
                    miniGamesController.IsRunning(false);
                    CountDownScript.lostGame(score);
                }
                if (score > progessScore && CountDownScript.TimerUp() && miniGamesController.GameRunning())
                {
                    string NextScene = CarrierScript.NextGame(score);
                    miniGamesController.IsRunning(false);
                    CountDownScript.endGame(NextScene, endlessCarrier.getCurrentPlayer(), score);
                }
                break;

            case GameMode.Vs when CarrierScript is VsDataCarrier vsCarrier:
                if (CountDownScript.TimerUp() && miniGamesController.GameRunning())
                {
                    string NextScene = CarrierScript.NextGame(score);

                    miniGamesController.IsRunning(false);
                    CountDownScript.endGame(NextScene, vsCarrier.getCurrentPlayer(), score);
                }
                break;

            case GameMode.FreePlay:
                if (score > progessScore && miniGamesController.GameRunning())
                {
                    miniGamesController.IsRunning(false);
                    SceneManager.LoadScene(miniGamesController.getNextScene());
                }
                break;
            case GameMode.Campaign when CarrierScript is CampaignDataCarrier campaignDataCarrier:
                if (score > progessScore && miniGamesController.GameRunning())
                {
                    miniGamesController.IsRunning(false);
                    campaignDataCarrier.unlockMiniGame(SceneManager.GetActiveScene().buildIndex + 1);
                    SceneManager.LoadScene(miniGamesController.getNextScene());
                }
                break;
        }
    }

    IEnumerator DisplayScore(int index)
    {
        displayScore[index] = true;
        ScoreDisplays[index].SetActive(true);
        yield return new WaitForSeconds(1);
        ScoreDisplays[index].SetActive(false);


    }
}
