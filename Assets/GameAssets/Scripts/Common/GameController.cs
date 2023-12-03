using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private IDataCarrierScript CarrierScript;
    private CountdownController CountDownScript;
    private IMiniGamesController miniGamesController;
    public float miniGameTime;


    // Start is called before the first frame update
    void Start()
    {
        //Find all the scripts from the DataCarrier and Controller
        GameObject DataCarrier = GameObject.Find("DataCarrier");
        CarrierScript = DataCarrier.GetComponent<IDataCarrierScript>();
        CountDownScript = GetComponent<CountdownController>();
        miniGamesController = GetComponent<IMiniGamesController>();
        //let all scripts know how long the game time should be
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
            case GameMode.FreePlay:
                CountDownScript.StartGame("");
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
        switch (CarrierScript.GetGameMode())
        {
            case GameMode.Endless when CarrierScript is EndlessDataCarrier endlessCarrier:
                //Endless mode can finish due to time limit/ if score reaching mini, you keep going otherwise you 
                if (score < -10 && CountDownScript.TimerUp() && miniGamesController.GameRunning())
                {
                    CarrierScript.NextGame(score);
                    miniGamesController.IsRunning(false);
                    CountDownScript.lostGame(score);
                }
                if (score > -10 && CountDownScript.TimerUp() && miniGamesController.GameRunning())
                {
                    string NextScene = CarrierScript.NextGame(score);
                    miniGamesController.IsRunning(false);
                    CountDownScript.endGame(NextScene, endlessCarrier.getCurrentPlayer());
                }
                break;

            case GameMode.Vs when CarrierScript is VsDataCarrier vsCarrier:
                if (CountDownScript.TimerUp() && miniGamesController.GameRunning())
                {
                    string NextScene = CarrierScript.NextGame(score);

                    miniGamesController.IsRunning(false);
                    CountDownScript.endGame(NextScene, vsCarrier.getCurrentPlayer());
                }
                break;

            case GameMode.FreePlay:
                if (score > 230 && miniGamesController.GameRunning())
                {
                    miniGamesController.IsRunning(false);
                    SceneManager.LoadScene(miniGamesController.getNextScene());
                }
                break;
        }
    }
}
