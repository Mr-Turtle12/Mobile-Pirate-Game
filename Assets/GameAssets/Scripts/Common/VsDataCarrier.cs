using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VsDataCarrier : MonoBehaviour, IDataCarrierScript
{

    [SerializeField]
    private float gameLength = 10f;
    private string currentPlayer;
    private string currentGame;
    [SerializeField]
    private Dictionary<string, int> PlayerInformation =
                   new Dictionary<string, int>();
    [SerializeField]
    public List<string> GameNames;

    //Implement the IDataCarrier interface:
    public GameMode GetGameMode()
    {
        return GameMode.Vs;
    }
    public string NextGame(int score)
    {
        PlayerInformation[currentPlayer] = PlayerInformation[currentPlayer] + score;
        int Index = (PlayerInformation.Keys.ToList().IndexOf(currentPlayer) + 1) % PlayerInformation.Count;
        currentPlayer = PlayerInformation.Keys.ToList()[Index];
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
    public int GetScoreOf(string player)
    {
        return PlayerInformation[currentPlayer];
    }

    public string StartVsMode()
    {
        currentPlayer = PlayerInformation.Keys.ToList()[0];
        currentGame = GameNames[0];
        return currentGame;
    }
    public string getCurrentPlayer()
    {
        return currentPlayer;
    }

    public void AddPlayer(List<string> newPlayerNames)
    {
        foreach (var player in newPlayerNames)
        {
            PlayerInformation.Add(player, 0);
        }
    }
    public string getPlayerOrder()
    {
        var sortedPlayers = PlayerInformation.OrderByDescending(pair => pair.Value);

        int rank = 1;
        string output = "Score:\n";
        foreach (var player in sortedPlayers)
        {
            output += $"{rank}) {player.Key} - {player.Value}\n";
            rank++;
        }
        return output;
    }
    void Start()
    {
        //Add all the Games in order
        GameNames = new List<string>(new string[] { "SeagullScene", "CompassGame", "DigTreasure", "RopeScene", "TreeTap", "LoadLogs", "EndScene" });

    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
