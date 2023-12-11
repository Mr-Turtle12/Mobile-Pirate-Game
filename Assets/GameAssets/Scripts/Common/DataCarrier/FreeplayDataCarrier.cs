using JetBrains.Annotations;
using UnityEngine;

public class FreeplayDataCarrier : MonoBehaviour, IDataCarrierScript
{

    [SerializeField]
    private float gameLength = 10f;
    //Implement the IDataCarrier interface:
    public GameMode GetGameMode()
    {
        return GameMode.FreePlay;
    }
    public string NextGame(int score)
    {
        return null;
    }
    public float GetLength()
    {
        return gameLength;
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }
}
