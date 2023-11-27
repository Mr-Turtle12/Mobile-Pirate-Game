using System.Collections.Generic;
using UnityEngine;


public class DataCarrierScript : MonoBehaviour
{
    [SerializeField]
    private List<string> playerNames;
    [SerializeField]
    private GameMode selectedGameMode;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SetGameMode(GameMode newGameMode)
    {
        selectedGameMode = newGameMode;
    }
    public GameMode GetGameMode()
    {
        return selectedGameMode;
    }

    public void SetPlayerList(List<string> newPlayerNames)
    {
        playerNames = newPlayerNames;
    }
    public List<string> GetPlayerList()
    {
        return playerNames;
    }
}

public enum GameMode
{
    Vs,
    Endless,
    FreePlay
}
