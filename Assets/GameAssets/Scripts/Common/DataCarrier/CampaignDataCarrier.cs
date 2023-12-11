using JetBrains.Annotations;
using UnityEngine;

public class CampaignDataCarrier : MonoBehaviour, IDataCarrierScript
{

    [SerializeField]
    private float gameLength = 10f;

    //Implement the IDataCarrier interface:
    public GameMode GetGameMode()
    {
        return GameMode.Campaign;
    }
    public string NextGame(int score)
    {
        return null;
    }
    public float GetLength()
    {
        return gameLength;
    }
    public int getUnlockedMiniGames()
    {
        return PlayerPrefs.GetInt("unlockedMiniGame");
    }
    public void unlockMiniGame(int index)
    {
        PlayerPrefs.SetInt("unlockedMiniGame", index);
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
