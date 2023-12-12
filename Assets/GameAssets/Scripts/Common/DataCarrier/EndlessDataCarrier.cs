using System.Collections.Generic;
using UnityEngine;

public class EndlessDataCarrier : MonoBehaviour, IDataCarrierScript
{
    // Start is called before the first frame update
    [SerializeField]
    private List<string> playerNames;
    private string currentPlayer;
    private int numbersOfGamesCompleted = -1;
    public int OverallScore;
    public List<string> GameNames;
    public float decreaseTimeBy = 0.1f;


    [SerializeField]
    private float gameLength = 10f;

    //Implement the IDataCarrier interface:
    public GameMode GetGameMode()
    {
        return GameMode.Endless;
    }
    public string NextGame(int score)
    {
        numbersOfGamesCompleted++;
        if ((numbersOfGamesCompleted - (playerNames.Count * 2)) > 0)
        {
            NextPlayer(true);
        }
        else
        {
            NextPlayer();
        }
        if ((numbersOfGamesCompleted - playerNames.Count) > 0)
        {
            decreaseLength();
        }
        OverallScore += score;

        return GameNames[UnityEngine.Random.Range(0, GameNames.Count)];
    }
    public float GetLength()
    {
        return gameLength;
    }

    /// Other function for endless mode

    public void decreaseLength()
    {
        gameLength -= gameLength * decreaseTimeBy;
    }

    public void NextPlayer(bool rand = false)
    {
        if (rand)
        {
            int randomIndex = UnityEngine.Random.Range(0, playerNames.Count);
            while (playerNames[randomIndex].Equals(currentPlayer))
            {
                randomIndex = UnityEngine.Random.Range(0, playerNames.Count);
            }
            currentPlayer = playerNames[randomIndex];
        }
        else
        {
            int Index = playerNames.IndexOf(currentPlayer);
            currentPlayer = playerNames[(Index + 1) % playerNames.Count];
        }
    }

    public string getCurrentPlayer()
    {
        return currentPlayer;
    }

    public void AddPlayer(List<string> newPlayerNames)
    {
        playerNames = newPlayerNames;
    }
    public string StartEndlessGame()
    {
        currentPlayer = playerNames[0];
        return GameNames[UnityEngine.Random.Range(0, GameNames.Count)];

    }
    void Start()
    {
        //Add all the Games in order
        GameNames = new List<string>(new string[] { "SeagullScene", "LoadCannons", "CompassGame", "DigTreasure", "RopeScene", "CrewRally", "StabPirate", "TreeTap", "LoadLogs" });
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
}
