
public interface IDataCarrierScript
{
    GameMode GetGameMode();
    string NextGame(int score);
    float GetLength();
}

public enum GameMode
{
    Vs,
    Endless,
    FreePlay
}

