using System.Collections.Generic;
using UnityEngine;

public class VsDataCarrier : MonoBehaviour, IDataCarrierScript
{

    [SerializeField]
    private float gameLength = 10f;
    private string currentPlayer;
    private string currentGame;
    public int OverallScore;
    [SerializeField]

    private List<string> playerNames;
    public List<string> GameNames;

    //Implement the IDataCarrier interface:
    public GameMode GetGameMode()
    {
        return GameMode.Vs;
    }
    public string NextGame(int score)
    {
        int Index = (playerNames.IndexOf(currentPlayer) + 1) % playerNames.Count;
        currentPlayer = playerNames[Index];
        if (Index.Equals(0))
        {
            Index = GameNames.IndexOf(currentGame) + 1;
            currentGame = GameNames[Index];
            return currentGame;
        }
        return currentGame;
    }
    public float GetLength()
    {
        return gameLength;
    }

    public string StartVsMode()
    {
        currentPlayer = playerNames[0];
        currentGame = GameNames[0];
        return currentGame;
    }
    public string getCurrentPlayer()
    {
        return currentPlayer;
    }

    public void AddPlayer(List<string> newPlayerNames)
    {
        playerNames = newPlayerNames;
    }
    void Start()
    {
        //Add all the Games in order
        GameNames = new List<string>(new string[] { "CompassGame", "DigTreasure", "RopeScene", "LoadLogs", "EndScene" });

    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
